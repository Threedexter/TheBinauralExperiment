using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour {

    public Button BackButton;
    public Canvas MainCanvas;

	void Start ()
    {
        BackButton.onClick.AddListener(ShowMain);
	}

    void ShowMain()
    {
        if(MainCanvas != null)
        {
            MainCanvas.GetComponent<Canvas>().enabled = true;
            this.GetComponent<Canvas>().enabled = false;
        }
    }
}
