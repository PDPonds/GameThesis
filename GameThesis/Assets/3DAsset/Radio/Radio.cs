using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
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

        if(Input.GetKeyUp(KeyCode.Space)) { PlaySong(); }
    }


    private void PlaySong()
    {
        audioSource.clip = playlist[Random.Range(0, playlist.Count)];
        audioSource.Play();
    }
}
