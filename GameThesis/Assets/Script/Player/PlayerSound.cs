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

    [Header("===== Audio Source =====")]
    public AudioSource as_playerPunchSource;
    public AudioSource as_playerHoldSource;
    public AudioSource as_playerHitSource;


    [Header("===== Audio Clip =====")]
    public List<AudioClip> ac_playerPunchClip = new List<AudioClip>();
    public List<AudioClip> ac_playerHeavyPunchClip = new List<AudioClip>();
    public List<AudioClip> ac_hitClip = new List<AudioClip>();


    private void OnEnable()
    {
        s_fistCombat.AddObserver(this);
        s_rightHit.AddObserver(this);
        s_leftHit.AddObserver(this);
    }

    private void OnDisable()
    {
        s_fistCombat.RemoveObserver(this);
        s_rightHit.RemoveObserver(this);
        s_leftHit.RemoveObserver(this);
    }
    private void Awake()
    {
        as_playerPunchSource = InitializedAudioSource(false);
        as_playerHoldSource = InitializedAudioSource(false);
        as_playerHitSource = InitializedAudioSource(false);
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
