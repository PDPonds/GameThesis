using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AISoundController : MonoBehaviour, IObserver
{
    public MainObserver s_aiMainObserver;
    public MainObserver s_aiPunchTrigger;

    public AudioSource as_punchHitBlock;
    public AudioSource as_punchHit;
    public AudioSource as_punch;
    public AudioSource as_eat;
    public AudioSource as_cheer;

    public List<AudioClip> ac_punchHitBlock = new List<AudioClip>();
    public List<AudioClip> ac_punchHit = new List<AudioClip>();
    public List<AudioClip> ac_punch = new List<AudioClip>();
    public List<AudioClip> ac_eat = new List<AudioClip>();
    public List<AudioClip> ac_cheer = new List<AudioClip>();

    private void OnEnable()
    {
        s_aiMainObserver.AddObserver(this);

        StateManager state = transform.GetComponent<StateManager>();
        if (state is CustomerStateManager)
        {
            s_aiPunchTrigger.AddObserver(this);
        }
    }

    private void OnDisable()
    {
        s_aiMainObserver.RemoveObserver(this);
        StateManager state = transform.GetComponent<StateManager>();
        if (state is CustomerStateManager)
        {
            s_aiPunchTrigger.RemoveObserver(this);
        }
    }

    private void Awake()
    {
        as_punch = InitializedAudioSource(false);
        as_punchHit = InitializedAudioSource(false);
        as_punchHitBlock = InitializedAudioSource(false);
        as_eat = InitializedAudioSource(true);
        as_cheer = InitializedAudioSource(true);
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

            case ActionObserver.AIEat:

                as_eat.volume = 0.05f;
                PlaySound(as_eat, ac_eat);

                break;
            case ActionObserver.AICheer:

                as_cheer.volume = 0.05f;
                PlaySound(as_cheer, ac_cheer);

                break;
            case ActionObserver.AIExitEat:

                PauseSound(as_eat);

                break;
            case ActionObserver.AIExitCheer:

                PauseSound(as_cheer);

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
            if (!source.loop)
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
            else
            {
                if (clips.Count > 1)
                {
                    if (!source.isPlaying)
                    {
                        PlayRandomAudioLoop(source, clips);
                    }
                }
                else
                {
                    if (!source.isPlaying)
                    {
                        PlaySelectAudioLoop(source, clips, 0);
                    }
                }
            }
        }
    }

    void PauseSound(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Pause();
        }
    }

    void PlaySelectAudio(AudioSource source, List<AudioClip> clips, int index)
    {
        if (!source.isPlaying)
        {
            source.clip = clips[index];
            source.PlayOneShot(clips[index]);
        }

    }

    void PlayRandomAudio(AudioSource source, List<AudioClip> clips)
    {
        int soundPlayIndex = Random.Range(0, clips.Count);
        if (!source.isPlaying)
        {
            source.clip = clips[soundPlayIndex];
            source.PlayOneShot(clips[soundPlayIndex]);
        }
    }

    void PlayRandomAudioLoop(AudioSource source, List<AudioClip> clips)
    {
        int soundPlayIndex = Random.Range(0, clips.Count);
        if (!source.isPlaying)
        {
            source.clip = clips[soundPlayIndex];
            source.Play();
        }

    }
    void PlaySelectAudioLoop(AudioSource source, List<AudioClip> clips, int index)
    {
        if (!source.isPlaying)
        {
            source.clip = clips[index];
            source.Play();
        }
    }
}
