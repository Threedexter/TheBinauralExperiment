using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeverDirection { Left, Right }

public struct trigger
{
    public BoxCollider box;
    public LeverDirection direction;
}

public class LeverInteractable : MonoBehaviour
{

    [Range(0.0f, 0.5f)]
    public float discrepancyDelay;

    [Range(0.5f, 1.5f)]
    public float freq;

    [Range(0f, 90f)]
    public float maxTilt;
    public GameObject lever;

    BaseInteractable baseInteract;

    public AudioSource clocksource;
    public AudioClip[] clockclips;

    public AudioSource leversource;
    public AudioClip[] leverclips;

    bool canTrigger;
    LeverDirection currentDirection;
    LeverDirection lastTriggerDir;
    int amountOfTriggersDone;
    public int amountOfTriggersToDo;
    bool hasTriggeredThisTick;

    void Start()
    {
        baseInteract = GetComponent<BaseInteractable>();
        InvokeRepeating("LeverTrigger", freq, freq);
    }

    void Update()
    {
        InputHandling();
        CheckEdges();
        CompareTriggersToDoWithDone();
    }

    public void LeverTrigger()
    {
        switch (currentDirection)
        {
            case LeverDirection.Left:
                currentDirection = LeverDirection.Right;
                clocksource.PlayOneShot(clockclips[0]);
                //Debug.Log("Right");
                break;
            case LeverDirection.Right:
                currentDirection = LeverDirection.Left;
                clocksource.PlayOneShot(clockclips[1]);
                //Debug.Log("Left");
                break;
        }
        canTrigger = true;
        hasTriggeredThisTick = false;
        StartCoroutine(Leverdelay());

    }

    IEnumerator Leverdelay()
    {
        yield return new WaitForSeconds(discrepancyDelay);
        canTrigger = false;
        if (!hasTriggeredThisTick) amountOfTriggersDone = 0;
        Debug.Log("CanTrigger=Off");
    }

    void InputHandling()
    {
        if (baseInteract.getCanInteract())
        {
            float inputVal = Input.GetAxis("Mouse X") * 0.2f;
            MoveLever(inputVal);
        }
    }

    void MoveLever(float inputVal)
    {
        if (lever.transform.localEulerAngles.x < maxTilt || lever.transform.localEulerAngles.x > (360 - maxTilt))
        {
            lever.transform.Rotate(new Vector3(inputVal, 0));
        }
        if (CheckForEdgeOfBoundsLeft())
        {
            lever.transform.localEulerAngles = new Vector3(maxTilt - 1, lever.transform.localEulerAngles.y, 0);
        }
        if (CheckForEdgeOfBoundsRight())
        {
            lever.transform.localEulerAngles = new Vector3(360 - maxTilt + 1, lever.transform.localEulerAngles.y, 0);
        }
        lever.transform.localEulerAngles = new Vector3(lever.transform.localEulerAngles.x, lever.transform.localEulerAngles.y, 0);
    }

    bool CheckForEdgeOfBoundsLeft()
    {
        if (lever.transform.localEulerAngles.x >= maxTilt && lever.transform.localEulerAngles.x < 90)
        {
            return true;
        }
        return false;
    }

    bool CheckForEdgeOfBoundsRight()
    {
        if (lever.transform.localEulerAngles.x <= (360 - maxTilt) && lever.transform.localEulerAngles.x > 270)
        {
            return true;
        }
        return false;
    }

    bool CheckForTriggerEdgeOfBoundsLeft()
    {
        if (lever.transform.localEulerAngles.x >= maxTilt - 2 && lever.transform.localEulerAngles.x < 90)
        {
            return true;
        }
        return false;
    }

    bool CheckForTriggerEdgeOfBoundsRight()
    {
        if (lever.transform.localEulerAngles.x <= (360 - maxTilt + 2) && lever.transform.localEulerAngles.x > 270)
        {
            return true;
        }
        return false;
    }

    bool playedSoundLeft;
    bool playedSoundRight;

    void CheckEdges()
    {
        if (baseInteract.getCanInteract())
        {
            if (CheckForTriggerEdgeOfBoundsLeft())
            {
                if (!playedSoundLeft && !leversource.isPlaying)
                {
                    leversource.PlayOneShot(leverclips[0]);
                    playedSoundLeft = true;
                }

                //Debug.Log("FoundLeftEdge");
                if (canTrigger && currentDirection == LeverDirection.Left && lastTriggerDir != currentDirection)
                {
                    lastTriggerDir = currentDirection;
                    Debug.Log("TriggerLeft");
                    amountOfTriggersDone++;
                    hasTriggeredThisTick = true;

                }
                if (!canTrigger && currentDirection != LeverDirection.Left)
                {
                    amountOfTriggersDone = 0;
                }
            }
            else
            {
                playedSoundLeft = false;
            }
        }
        if (CheckForTriggerEdgeOfBoundsRight())
        {
            if (!playedSoundRight && !leversource.isPlaying)
            {
                leversource.PlayOneShot(leverclips[1]);
                playedSoundRight = true;
            }
            //Debug.Log("FoundRightEdge");
            if (canTrigger && currentDirection == LeverDirection.Right && lastTriggerDir != currentDirection)
            {
                lastTriggerDir = currentDirection;
                Debug.Log("TriggerRight");
                amountOfTriggersDone++;
                hasTriggeredThisTick = true;
            }
            if (!canTrigger && currentDirection != LeverDirection.Right)
            {
                amountOfTriggersDone = 0;
            }
        }
        else
        {
            playedSoundRight = false;
        }
    }

    void CompareTriggersToDoWithDone()
    {
        if (amountOfTriggersDone >= amountOfTriggersToDo)
        {
            baseInteract.FinishPuzzle();
            amountOfTriggersDone = 0;
        }
    }
}
