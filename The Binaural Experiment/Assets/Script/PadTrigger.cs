using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTrigger : MonoBehaviour {

    Light light;
    PadInteractable padInteract;
    [Range(1f, 9f)]
    public int index;
    AudioSource noteSource;

    bool hasTriggered;

    // Use this for initialization
    void Start ()
    {
        light = GetComponentInChildren<Light>();
        padInteract = GetComponentInParent<PadInteractable>();
        noteSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Initialize()
    {
        light.enabled = true;
        PlayAudio();
        StartCoroutine(lightOff());
    }

    IEnumerator lightOff()
    {
        yield return new WaitForSeconds(1f);
        light.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.tag == "Player" && !padInteract.getFinished() && padInteract.getPuzzleRunning())
        {
            Debug.Log("Pad " + index + " is hit");
            if (!hasTriggered)
            {
                padInteract.triggerPad(index);
                hasTriggered = true;
            }
            PlayAudio();
            light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasTriggered = false;
    }

    void PlayAudio()
    {
        if (!noteSource.isPlaying)
        {
            noteSource.Play();
        }
    }

    public void TurnOffLight()
    {
        light.enabled = false;
    }
}
