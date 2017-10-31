using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUIScript : MonoBehaviour {

    public Button StartButton;
    public Button CreditsButton;
    public Button ExitButton;

    public Canvas CreditsCanvas;

	void Start ()
    {
        StartButton.onClick.AddListener(StartGame);
        CreditsButton.onClick.AddListener(ShowCredits);
        ExitButton.onClick.AddListener(ExitGame);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }


    void ExitGame()
    {
        Application.Quit();
    }

    void ShowCredits()
    {
        if (CreditsCanvas != null)
        {
            CreditsCanvas.GetComponent<Canvas>().enabled = true;
            this.GetComponent<Canvas>().enabled = false;
        }
    }

}
