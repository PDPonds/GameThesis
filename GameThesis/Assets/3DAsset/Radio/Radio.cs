using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Radio : MonoBehaviour , IInteracable
{
    public List<AudioClip> restaurantPlaylist = new List<AudioClip>();
    public List<AudioClip> combatPlaylist = new List<AudioClip>();
    public AudioClip interactSound;

    public AudioSource restaurantAudioSource;
    public AudioSource combatAudioSource;
    private bool isRadioOn = true;

    private int currentSong;

    void Start()
    {
        restaurantAudioSource = GetComponent<AudioSource>();
        combatAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isRadioOn)
        {
            if(!restaurantAudioSource.isPlaying)
            {
                NextSong();
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) { StartCombatSong(); }
        if(Input.GetKeyDown(KeyCode.RightArrow)) { StopCombatSong(); }
    }

    private void StartCombatSong()
    {
        combatAudioSource.clip = combatPlaylist[UnityEngine.Random.Range(0, combatPlaylist.Count)];
        restaurantAudioSource.Pause();
        combatAudioSource.Play();
    }

    private void StopCombatSong()
    {
        restaurantAudioSource.Play();
        combatAudioSource.Pause();
    }


    private void PlaySong()
    {
        restaurantAudioSource.clip = restaurantPlaylist[currentSong];
        restaurantAudioSource.Play();
    }

    private void NextSong()
    {
        currentSong++;
        if(currentSong >= restaurantPlaylist.Count) { currentSong = 0; }
        PlaySong();
    }

    public void Interaction()
    {
        isRadioOn = !isRadioOn;
        

        if (isRadioOn)
        {
            restaurantAudioSource.PlayOneShot(interactSound);
            NextSong();
        }
        else
        {
            restaurantAudioSource.Stop();
            restaurantAudioSource.PlayOneShot(interactSound);
        }
    }

    public string InteractionText()
    {
        if(isRadioOn)
        {
            return $"[E] to close radio";
        }
        else
        {
            return $"[E] to open radio";
        }
        
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
