using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour , IInteracable
{
    public List<AudioClip> playlist = new List<AudioClip>();
    public AudioClip openSound;
    public AudioClip closeSound;

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
            audioSource.Stop();
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
