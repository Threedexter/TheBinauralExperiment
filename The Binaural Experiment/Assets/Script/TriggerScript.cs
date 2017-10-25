using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    public StoryHandler storyscript;

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Player")
        {
            storyscript.PlayTutorialVoice();
        }
    }
}
