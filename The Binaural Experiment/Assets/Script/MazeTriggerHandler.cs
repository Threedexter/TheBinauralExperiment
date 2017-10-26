using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeTriggerHandler : MonoBehaviour
{
    #region Globals
    public enum TriggerSoundType { Pos, Good, Bad, End }

    public BoxCollider TriggerBox;
    public TriggerSoundType soundType;
    public AudioSource interactSource;
    AudioClip[] triggerClips;

    BaseInteractable baseInteract;

    private void Start()
    {
        baseInteract = GetComponentInParent<BaseInteractable>();
    }

    bool hasPlayed;
    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjectiveBall" && !hasPlayed)
        {
            playSound(this.soundType);
            hasPlayed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ObjectiveBall" && hasPlayed)
        {
            hasPlayed = false;
        }

    }
    #endregion

    #region Audio
    public void SetAudioClips(AudioClip[] clips)
    {
        triggerClips = clips;
    }

    public void playSound(TriggerSoundType type)
    {
        interactSource.PlayOneShot(triggerClips[(int)type]);
        if (type == TriggerSoundType.End)
        {
            baseInteract.FinishPuzzle();
        }
    }
    #endregion
}
