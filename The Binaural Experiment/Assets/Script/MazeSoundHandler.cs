using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSoundHandler : MonoBehaviour
{

    public GameObject ball;
    public GameObject player;

    public AudioSource source;

    public MazeTriggerHandler[] handlers;

    public AudioClip[] TriggerAudioClips;

    void Start()
    {
        SetParamForHandlers(handlers);
    }

    void Update()
    {
        float vel = getObjectVelocitytoVolume(ball);
        float vol = VolumeDifference(player.transform.position, ball.transform.position);
        vol = (vel * 10) * vol;
        float pan = PanningValue(player.transform.position, ball.transform.position);

        SetPanAndVolume(pan, vol);
    }

    float PanningValue(Vector3 OriginPos, Vector3 ballPos)
    {
        float angle = OriginPos.z - ballPos.z;
        angle = angle.Remap(-0.2f, 0.2f, 1f, -1f);
        return angle;

    }

    float VolumeDifference(Vector3 Origin, Vector3 other)
    {
        float dist = Origin.x - other.x;
        float orig = dist;
        dist = dist.Remap(0.45f, 0.0f, 0.05f, 0.2f);
        return dist;
    }

    void SetPanAndVolume(float angle, float distance)
    {
        source.panStereo = angle;
        source.volume = distance;
    }

    float getObjectVelocitytoVolume(GameObject obj)
    {
        Vector3 vel = obj.GetComponent<Rigidbody>().velocity;
        return vel.magnitude;
    }

    void SetParamForHandlers(MazeTriggerHandler[] triggerHandlers)
    {
        int i;
        for (i = 0; i < triggerHandlers.Length; i++)
        {
            float vol = VolumeDifference(player.transform.position, triggerHandlers[i].transform.position);
            float pan = PanningValue(player.transform.position, triggerHandlers[i].transform.position);
            triggerHandlers[i].interactSource.panStereo = pan;
            triggerHandlers[i].interactSource.volume = vol;
            triggerHandlers[i].SetAudioClips(TriggerAudioClips);
        }
    }
}
