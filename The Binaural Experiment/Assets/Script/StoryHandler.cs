using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryHandler : MonoBehaviour
{

    public BoxCollider StartTrigger;
    public FPSHandler player;
    public AudioSource voiceSource;
    public AudioClip[] clips;
    public Animator treeAnim;

    public Text subText;
    public GameObject SubBox;

    public string[] script;

    bool PlayedFirstVox;
    bool PlayedTutVox;

    [Range(1.0f, 5.0f)]
    public float voiceDelay;

    void Start()
    {
        StartCoroutine(VoiceDelay());
    }

    private void Update()
    {
        CheckForActiveVoice();
        Skip();
        Reset();
    }

    IEnumerator VoiceDelay()
    {
        yield return new WaitForSeconds(voiceDelay);
        PlayStartVoice();
    }

    IEnumerator FirstScriptTriggers()
    {
        SubBox.SetActive(true);
        subText.text = script[0];
        yield return new WaitForSeconds(5f);
        subText.text = script[1];
        yield return new WaitForSeconds(5f);
        subText.text = script[2];
        yield return new WaitForSeconds(5f);
        subText.text = script[3];
        yield return new WaitForSeconds(3f);
        subText.text = script[4];
        yield return new WaitForSeconds(3.5f);
        subText.text = script[5];
        yield return new WaitForSeconds(4f);
        SubBox.SetActive(false);
    }

    IEnumerator TutorialScriptTriggers()
    {
        SubBox.SetActive(true);
        subText.text = script[24];
        yield return new WaitForSeconds(5f);
        subText.text = script[6];
        yield return new WaitForSeconds(5f);
        subText.text = script[7];
        yield return new WaitForSeconds(3.5f);
        subText.text = script[8];
        yield return new WaitForSeconds(4f);
        subText.text = script[9];
        yield return new WaitForSeconds(2.5f);
        subText.text = script[10];
        yield return new WaitForSeconds(1f);
        subText.text = script[11];
        yield return new WaitForSeconds(3f);
        subText.text = script[12];
        yield return new WaitForSeconds(3f);
        subText.text = script[13];
        yield return new WaitForSeconds(1.5f);
        subText.text = script[14];
        yield return new WaitForSeconds(2f);
        subText.text = script[15];
        yield return new WaitForSeconds(3f);
        subText.text = script[16];
        yield return new WaitForSeconds(3f);
        subText.text = script[17];
        yield return new WaitForSeconds(3f);
        subText.text = script[18];
        yield return new WaitForSeconds(3f);
        subText.text = script[19];
        yield return new WaitForSeconds(4f);
        subText.text = script[20];
        yield return new WaitForSeconds(3.5f);
        subText.text = script[21];
        yield return new WaitForSeconds(3.5f);
        subText.text = script[22];
        yield return new WaitForSeconds(5f);
        subText.text = script[23];
        yield return new WaitForSeconds(3f);
        SubBox.SetActive(false);
    }

    void PlayStartVoice()
    {
        if (!PlayedFirstVox)
        {
            voiceSource.PlayOneShot(clips[0]);
            PlayedFirstVox = true;
            player.setCanMove(false);
            StartCoroutine(FirstScriptTriggers());
        }
    }

    public void PlayTutorialVoice()
    {
        if (!PlayedTutVox)
        {
            treeAnim.Play("TreeFall");
            voiceSource.PlayOneShot(clips[1]);
            PlayedTutVox = true;
            player.setCanMove(false);
            StartCoroutine(TutorialScriptTriggers());
        }
    }

    void CheckForActiveVoice()
    {
        if (!voiceSource.isPlaying && !player.getCanMove())
        {
            player.setCanMove(true);
        }
    }

    void Skip()
    {
        if (Input.GetButtonDown("Skip"))
        {
            voiceSource.Stop();
            SubBox.SetActive(false);
            StopAllCoroutines();
        }
    }

    void Reset()
    {
        if(Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
