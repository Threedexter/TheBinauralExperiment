using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour {

    FPSHandler player;

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Player")
        {
            player = other.GetComponentInParent<FPSHandler>();
            player.fadeCameraOut();
        }
    }

    private void Update()
    {
        if(player != null && (player.getAlpha() >= 1))
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}
