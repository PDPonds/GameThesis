using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AISoundController : MonoBehaviour, IObserver
{
    public MainObserver s_leftPunchTrigger;
    public MainObserver s_rightPunchTrigger;

    public AudioSource as_punchHitBlock;
    public AudioSource as_punchHit;
    public AudioSource as_punch;

    public List<AudioClip> ac_punchHitBlock = new List<AudioClip>();
    public List<AudioClip> ac_punchHit = new List<AudioClip>();
    public List<AudioClip> ac_punch = new List<AudioClip>();

    private void OnEnable()
    {
        s_rightPunchTrigger.AddObserver(this);
        s_leftPunchTrigger.AddObserver(this);
    }

    private void OnDisable()
    {
        s_rightPunchTrigger.RemoveObserver(this);
        s_leftPunchTrigger.RemoveObserver(this);
    }

    private void Awake()
    {
        as_punch = InitializedAudioSource(false);
        as_punchHit = InitializedAudioSource(false);
        as_punchHitBlock = InitializedAudioSource(false);
    }

    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.AIPunchHitBlock:

                PlaySound(as_punchHitBlock, ac_punchHitBlock);

                break;
            case ActionObserver.AIPunchHit:

                PlaySound(as_punchHit, ac_punchHit);

                break;
            case ActionObserver.AIPunch:

                PlaySound(as_punch, ac_punch);

                break;
            default: break;
        }
    }

    AudioSource InitializedAudioSource(bool loop)
    {
        AudioSource newSource = transform.AddComponent<AudioSource>();
        newSource.loop = loop;
        return newSource;
    }

    void PlaySound(AudioSource source, List<AudioClip> clips)
    {
        if (clips.Count > 0)
        {
            if (clips.Count > 1)
            {
                PlayRandomAudio(source, clips);
            }
            else
            {
                PlaySelectAudio(source, clips, 0);
            }
        }
    }

    void PlaySelectAudio(AudioSource source, List<AudioClip> clips, int index)
    {
        source.clip = clips[index];
        source.PlayOneShot(clips[index]);
    }

    void PlayRandomAudio(AudioSource source, List<AudioClip> clips)
    {
        int soundPlayIndex = Random.Range(0, clips.Count);
        source.clip = clips[soundPlayIndex];
        source.PlayOneShot(clips[soundPlayIndex]);
    }

}
