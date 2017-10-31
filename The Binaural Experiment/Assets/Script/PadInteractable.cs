using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadInteractable : MonoBehaviour
{

    BaseInteractable baseInteract;
    public GameObject[] pads;
    AudioSource source;


    bool PuzzleIsRunning;
    bool PuzzleHasStarted;

    int startEnum;
    int currentPadIndex;
    int[] padSequence = { 1, 4, 5, 2, 3, 6, 9, 8, 7 };

    // Use this for initialization
    void Start()
    {
        baseInteract = GetComponent<BaseInteractable>();
        source = GetComponent<AudioSource>();
        startEnum = 0;
        currentPadIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PuzzleHasStarted && !PuzzleIsRunning && !baseInteract.getFinishedPuzzle())
        {
            CheckForStart();
        }
    }

    void CheckForStart()
    {
        if (baseInteract.getCanInteract())
        {
            PuzzleHasStarted = true;
            InitializePuzzle();
        }
    }

    void InitializePuzzle()
    {
        StartCoroutine(holdPad());
    }

    IEnumerator holdPad()
    {
        if (startEnum < pads.Length)
        {
            pads[startEnum].GetComponent<PadTrigger>().Initialize();
            yield return new WaitForSeconds(1.0f);
            startEnum++;
            StartCoroutine(holdPad());
        }
        if(startEnum >= pads.Length)
        {
            StartMusic();
            PuzzleIsRunning = true;
        }
        StopCoroutine(holdPad());
    }

    void StartMusic()
    {
        if(!source.isPlaying)
        source.Play();
    }


    void StopMusic()
    {
        if(source.isPlaying)
        source.Stop();
    }

    public void triggerPad(int padIndex)
    {
        if(PuzzleIsRunning && !baseInteract.getFinishedPuzzle())
        {
            if (padSequence[currentPadIndex] == padIndex)
            {
                currentPadIndex++;
                if (currentPadIndex == padSequence.Length)
                {
                    baseInteract.FinishPuzzle();
                    StopMusic();
                }
                return;
            }
            if(padSequence[currentPadIndex] != padIndex)
            {
                foreach (GameObject pad in pads)
                {
                    pad.GetComponent<PadTrigger>().TurnOffLight();
                }
            }
        }
    }

    public bool getPuzzleRunning()
    {
        return PuzzleIsRunning;
    }

    public bool getFinished()
    {
        return baseInteract.getFinishedPuzzle();
    }
}
