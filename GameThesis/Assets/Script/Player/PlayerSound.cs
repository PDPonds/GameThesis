using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : Auto_Singleton<PlayerSound>, IObserver
{
    [Header("===== Main Observer =====")]
    public MainObserver s_fistCombat;
    public MainObserver s_rightHit;
    public MainObserver s_leftHit;
    public MainObserver s_movement;

    [Header("===== Audio Source =====")]
    public AudioSource as_playerPunchSource;
    public AudioSource as_playerHoldSource;
    public AudioSource as_playerHitSource;
    public AudioSource as_playerWalk;

    [Header("===== Audio Clip =====")]
    public List<AudioClip> ac_playerPunchClip = new List<AudioClip>();
    public List<AudioClip> ac_playerHeavyPunchClip = new List<AudioClip>();
    public List<AudioClip> ac_hitClip = new List<AudioClip>();
    public AudioClip ac_walkDirt;
    public AudioClip ac_walkWood;

    private void OnEnable()
    {
        s_fistCombat.AddObserver(this);
        s_rightHit.AddObserver(this);
        s_leftHit.AddObserver(this);
        s_movement.AddObserver(this);
    }

    private void OnDisable()
    {
        s_fistCombat.RemoveObserver(this);
        s_rightHit.RemoveObserver(this);
        s_leftHit.RemoveObserver(this);
        s_movement.RemoveObserver(this);
    }
    private void Awake()
    {
        as_playerPunchSource = InitializedAudioSource(false);
        as_playerHoldSource = InitializedAudioSource(false);
        as_playerHitSource = InitializedAudioSource(false);
        as_playerWalk = InitializedAudioSource(true);
    }
    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerLeftSoftPunch:

                PlaySound(as_playerPunchSource, ac_playerPunchClip);

                break;
            case ActionObserver.PlayerRightSoftPunch:

                PlaySound(as_playerPunchSource, ac_playerPunchClip);

                break;
            case ActionObserver.PlayerHeavyPunch:

                PlaySound(as_playerHoldSource, ac_playerHeavyPunchClip);

                break;
            case ActionObserver.PlayerAttackRightHit:

                PlaySound(as_playerHitSource, ac_hitClip);

                break;
            case ActionObserver.PlayerAttackLeftHit:

                PlaySound(as_playerHitSource, ac_hitClip);

                break;
            case ActionObserver.PlayerWalkInRestaurant:

                as_playerWalk.volume = 0.1f;
                PlaySound(as_playerWalk, ac_walkWood);

                break;
            case ActionObserver.PlayerWalkOutRestaurant:

                as_playerWalk.volume = 0.1f;
                PlaySound(as_playerWalk, ac_walkDirt);

                break;
            case ActionObserver.PlayerStopWalk:

                PauseAudio(as_playerWalk);

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
        if (source.loop == false)
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
        else
        {
            if (clips.Count > 0)
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

    void PlaySound(AudioSource source, AudioClip clips)
    {
        if (!source.isPlaying)
        {
            PlayAudioLoop(source, clips);
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

    void PlaySelectAudioLoop(AudioSource source, List<AudioClip> clips, int index)
    {
        source.clip = clips[index];
        source.Play();
    }

    void PlayRandomAudioLoop(AudioSource source, List<AudioClip> clips)
    {
        int soundPlayIndex = Random.Range(0, clips.Count);
        source.clip = clips[soundPlayIndex];
        source.Play();
    }

    void PauseAudio(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Pause();
        }
    }

    void PlayAudioLoop(AudioSource source, AudioClip clips)
    {
        source.clip = clips;
        source.Play();
    }
}
