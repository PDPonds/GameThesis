using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Radio : MonoBehaviour , IInteracable
{
    public List<AudioClip> playlist = new List<AudioClip>();
    public AudioClip interactSound;

    private AudioSource audioSource;
    private bool isRadioOn = true;

    private int currentSong;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isRadioOn)
        {
            if(!audioSource.isPlaying)
            {
                PlaySong();
            }
        }
        else
        {
            
        }

    }


    private void PlaySong()
    {
        audioSource.clip = playlist[currentSong];
        audioSource.Play();
    }

    private void NextSong()
    {
        currentSong++;
        if(currentSong >= playlist.Count) { currentSong = 0; }
        PlaySong();
    }

    public void Interaction()
    {
        isRadioOn = !isRadioOn;
        

        if (isRadioOn)
        {
            audioSource.PlayOneShot(interactSound);
            NextSong();
        }
        else
        {
            audioSource.Stop();
            audioSource.PlayOneShot(interactSound);
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
