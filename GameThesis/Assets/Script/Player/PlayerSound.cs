using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : Auto_Singleton<PlayerSound>, IObserver
{
    [Header("===== Main Observer =====")]
    public MainObserver s_fistCombat;
    public MainObserver s_hit;

    [Header("===== Audio Source =====")]
    public AudioSource as_playerPunchSource;
    public AudioSource as_playerHoldSource;
    public AudioSource as_playerHitSource;

    [Header("===== Audio Clip =====")]
    public List<AudioClip> ac_playerPunchClip = new List<AudioClip>();
    public List<AudioClip> ac_playerHoldClip = new List<AudioClip>();
    public List<AudioClip> ac_hitClip = new List<AudioClip>();

    private void OnEnable()
    {
        s_fistCombat.AddObserver(this);
        s_hit.AddObserver(this);
    }

    private void OnDisable()
    {
        s_fistCombat.RemoveObserver(this);
        s_hit.RemoveObserver(this);
    }
    private void Awake()
    {
        InitializedAudioSource(as_playerPunchSource, false);
        InitializedAudioSource(as_playerHoldSource, false);
        InitializedAudioSource(as_playerHitSource, false);
    }
    public void FuncToDo(ActionObserver action)
    {
        switch (action)
        {
            case ActionObserver.PlayerPunch:

                PlaySound(as_playerPunchSource, ac_playerPunchClip);

                break;
            case ActionObserver.PlayerHoldPunch:

                PlaySound(as_playerHoldSource, ac_playerHoldClip);

                break;
            case ActionObserver.PlayerAttackHit:

                PlaySound(as_playerHitSource, ac_hitClip);

                break;
            default: break;
        }
    }

    void InitializedAudioSource(AudioSource source, bool loop)
    {
        source = transform.AddComponent<AudioSource>();
        source.loop = loop;
    }

    void PlaySound(AudioSource source, List<AudioClip> clips)
    {
        if(clips.Count > 0)
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
        source.Play();
    }

    void PlayRandomAudio(AudioSource source, List<AudioClip> clips)
    {
        int soundPlayIndex = Random.Range(0, clips.Count);
        source.clip = clips[soundPlayIndex];
        source.Play();
    }

}
