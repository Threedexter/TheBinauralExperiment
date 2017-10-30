using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryHandler : MonoBehaviour
{

    public BoxCollider StartTrigger;
    public FPSHandler player;
    public AudioSource voiceSource;
    public AudioClip[] clips;
    public Animator treeAnim;

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

    void PlayStartVoice()
    {
        if (!PlayedFirstVox)
        {
            voiceSource.PlayOneShot(clips[0]);
            PlayedFirstVox = true;
            player.setCanMove(false);
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
        }
    }

    void CheckForActiveVoice()
    {
        if (!voiceSource.isPlaying)
        {
            player.setCanMove(true);
        }
    }

    void Skip()
    {
        if (Input.GetButtonDown("Skip"))
        {
            voiceSource.Stop();
        }
    }

    void Reset()
    {
        if(Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
