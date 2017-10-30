using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHandler : MonoBehaviour {

    public BaseInteractable[] puzzles;
    public Animator anim;

    public Light[] StatusLights;

	// Use this for initialization
	void Start ()
    {
		foreach(Light light in StatusLights)
        {
            light.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdatePuzzleStatus();
        CheckAllPuzzlesFinished();
	}

    void UpdatePuzzleStatus()
    {
        for(int i = 0;i<puzzles.Length;i++)
        {
            if(puzzles[i].getFinishedPuzzle())
            {
                StatusLights[i].enabled = true;
            }
        }
    }

    void CheckAllPuzzlesFinished()
    {
        int lightCount = 0;
        foreach (Light light in StatusLights)
        {
            if(light.enabled)
            {
                lightCount++;
            }
            if (lightCount >= StatusLights.Length)
            {
                OpenDoor();
                break;
            }
        }
    }

    void OpenDoor()
    {
        anim.Play("DoorAnim");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Player")
        {

        }
    }
}
