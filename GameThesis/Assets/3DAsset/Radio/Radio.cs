using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour , IInteracable
{
    public List<AudioClip> playlist = new List<AudioClip>();
    public AudioClip interactSound;

    private AudioSource audioSource;
    private bool isRadioOn = true;

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
        audioSource.clip = playlist[Random.Range(0, playlist.Count)];
        audioSource.Play();
    }

    public void Interaction()
    {
        isRadioOn = !isRadioOn;
        

        if (isRadioOn)
        {
            audioSource.PlayOneShot(interactSound);
            PlaySong();
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
