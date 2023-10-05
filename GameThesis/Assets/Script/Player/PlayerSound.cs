using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : Auto_Singleton<PlayerSound>, IObserver
{
    [Header("===== Main Observer =====")]
    public MainObserver s_punchTrigger;

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
        s_punchTrigger.AddObserver(this);
    }

    private void OnDisable()
    {
        s_punchTrigger.RemoveObserver(this);
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

                break;
            case ActionObserver.PlayerHoldPunch:

                break;
            case ActionObserver.PlayerAttackHit:

                break;
            default: break;
        }
    }

    void InitializedAudioSource(AudioSource source, bool loop)
    {
        source = transform.AddComponent<AudioSource>();
        source.loop = loop;
    }

    void PlayAudio(AudioSource source)
    {

    }

    void PlayRandomAudio(AudioSource source)
    {

    }

}
