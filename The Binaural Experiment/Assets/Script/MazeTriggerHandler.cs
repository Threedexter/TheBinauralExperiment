using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeTriggerHandler : MonoBehaviour
{
    public enum TriggerSoundType { Pos, Good, Bad }

    public BoxCollider TriggerBox;
    public TriggerSoundType soundType;

    bool hasPlayed;

    public void playSound(TriggerSoundType type)
    {
        switch(type)
        {
            case TriggerSoundType.Bad:
                Debug.Log("Hit a wrong turn");
                break;
            case TriggerSoundType.Good:
                Debug.Log("Going right way");
                break;
            case TriggerSoundType.Pos:
                Debug.Log("Hit A wall");
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ObjectiveBall" && !hasPlayed)
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
}
