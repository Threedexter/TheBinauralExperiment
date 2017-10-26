using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeInteractable : MonoBehaviour
{
    #region Globals
    public GameObject dial;

    SafePuzzleLogic.CombinationStep[] Combinations;

    SafePuzzleLogic puzzlelogic;
    BaseInteractable baseInteract;
    SafePuzzleLogic.TurnDirection dir;
    int StepIndex = 0;
    float angle;

    bool canInteract;
    bool PlayedClick = false;

    public AudioSource screechSource;
    public AudioSource wheelSource;
    public AudioSource ClickSource;

    public AudioClip[] clips;
    #endregion

    #region UnityEngine
    void Start()
    {
        puzzlelogic = GetComponent<SafePuzzleLogic>();
        baseInteract = GetComponent<BaseInteractable>();
        Combinations = new SafePuzzleLogic.CombinationStep[3];
        FetchCombination();
    }

    void FetchCombination()
    {
        if (puzzlelogic == null)
        {
            return;
        }
        int rndVal = (int)Random.Range(0, 9);
        Combinations = puzzlelogic.ReturnCombination(rndVal);

    }

    void Update()
    {
        Interact();
        CheckCombination();
        CheckForResetCombination();
    }
    #endregion

    #region LogicCode
    public void Interact()
    {
        float inputVal = Input.GetAxis("Mouse X") * 0.5f;
        if (baseInteract.getCanInteract() && baseInteract.getWithinRange())
        {
            dial.transform.eulerAngles += (new Vector3(0, 0, inputVal));
            angle = dial.transform.rotation.eulerAngles.z;
            if (inputVal > 0.5)
            {
                dir = SafePuzzleLogic.TurnDirection.Right;
            }
            else if (inputVal < -0.5)
            {
                dir = SafePuzzleLogic.TurnDirection.Left;
            }
        }
    }

    public bool getWithinRange()
    {
        return baseInteract.getWithinRange();
    }

    void CheckForResetCombination()
    {
        if(!getWithinRange() && !baseInteract.getFinishedPuzzle() && StepIndex > 0)
        {
            ClickSource.PlayOneShot(clips[1]);
            StepIndex = 0;
        }
    }
    #endregion

    #region combinationHandling

    void CheckCombination()
    {
        float inputAngle = Input.GetAxis("Mouse X") * 0.5f;
        playSoundDirection(dir, Combinations[StepIndex].direction, inputAngle);
        playSoundWrongWay(dir, Combinations[StepIndex].direction, Combinations[StepIndex].tumblerAngle, angle, 1f, inputAngle);
        if (compareDirection(dir, Combinations[StepIndex].direction) && compareAroundValue(Combinations[StepIndex].tumblerAngle, angle, 1f) && Mathf.Abs(inputAngle) < 2)
        {
            if (!PlayedClick && baseInteract.getCanInteract()) ClickSource.PlayOneShot(clips[0]);
            PlayedClick = true;
            if (StepIndex == 2)
            {
                baseInteract.setCanInteract(false);
                baseInteract.FinishPuzzle();
                StepIndex = 0;
            }
            StepIndex++;
        }
        else
        {
            PlayedClick = false;
        }
    }

    bool compareAroundValue(float orig, float other, float error)
    {
        if (orig + error > other && orig - error < other)
        {
            return true;
        }
        return false;
    }

    bool compareDirection(SafePuzzleLogic.TurnDirection cur, SafePuzzleLogic.TurnDirection goal)
    {
        if (cur == goal)
        {
            return true;
        }
        return false;
    }

    void playSoundDirection(SafePuzzleLogic.TurnDirection cur, SafePuzzleLogic.TurnDirection goal, float inputAngle)
    {
        if (baseInteract.getCanInteract())
        {
            if (cur != goal && Mathf.Abs(inputAngle) > 0.5)
            {
                if (!screechSource.isPlaying)
                {
                    screechSource.Play();
                }
            }
            if (cur != goal && Mathf.Abs(inputAngle) < 0.5)
            {
                if (screechSource.isPlaying)
                {
                    screechSource.Stop();
                }
            }

            if (cur == goal && Mathf.Abs(inputAngle) > 0.5)
            {
                if (!wheelSource.isPlaying)
                {
                    wheelSource.Play();
                }
            }
            if (cur == goal && Mathf.Abs(inputAngle) < 0.5)
            {
                if (wheelSource.isPlaying)
                {
                    wheelSource.Stop();
                }
            }
        }
        else
        {
            if (wheelSource.isPlaying)
            {
                wheelSource.Stop();
            }
            if (screechSource.isPlaying)
            {
                screechSource.Stop();
            }
        }

    }

    void playSoundWrongWay(SafePuzzleLogic.TurnDirection cur, SafePuzzleLogic.TurnDirection goal, float orig, float other, float error, float inputAngle)
    {
        if ((!compareDirection(cur, goal) && compareAroundValue(orig, other, error)) || (inputAngle > 2 && compareAroundValue(orig, other, error)))
        {
            if (baseInteract.getCanInteract())
            {
                ClickSource.PlayOneShot(clips[1]);
            }
        }
    }

    #endregion
}
