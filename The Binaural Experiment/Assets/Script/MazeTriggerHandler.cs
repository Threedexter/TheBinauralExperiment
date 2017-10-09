using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeTriggerHandler : MonoBehaviour
{
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

    public void playSound(TriggerSoundType type)
    {
        interactSource.PlayOneShot(triggerClips[(int)type]);
        if(type == TriggerSoundType.End)
        {
            baseInteract.FinishPuzzle();
        }
        //switch (type)
        //{
        //    case TriggerSoundType.Bad:
        //        interactSource.PlayOnce(trigger);
        //        Debug.Log("Hit a wrong turn");
        //        break;
        //    case TriggerSoundType.Good:
        //        Debug.Log("Going right way");
        //        break;
        //    case TriggerSoundType.Pos:
        //        Debug.Log("Hit A wall");
        //        break;
        //    case TriggerSoundType.End:
        //        baseInteract.FinishPuzzle();
        //        Debug.Log("Hit the end");
        //        break;
        //    case TriggerSoundType.Start:
        //        break;
        //}
    }

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

    public void SetAudioClips(AudioClip[] clips)
    {
        triggerClips = clips;
    }
}
