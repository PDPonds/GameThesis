using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Radio : MonoBehaviour, IInteracable
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

    int count = 0;

    void Update()
    {
        if (isRadioOn)
        {
            if (!restaurantAudioSource.isPlaying)
            {
                NextSong();
            }
        }

        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (PlayerManager.Instance.b_inFighting)
            {
                if (count == 0)
                {
                    StartCombatSong();
                    count++;
                }
            }
            else
            {
                if (count != 0)
                {
                    StopCombatSong();
                    count = 0;
                }
            }
        }
        
    }

    private void StartCombatSong()
    {
        if (restaurantAudioSource.isPlaying) restaurantAudioSource.Pause();
        if (!combatAudioSource.isPlaying)
        {
            combatAudioSource.clip = combatPlaylist[UnityEngine.Random.Range(0, combatPlaylist.Count)];
            combatAudioSource.Play();
        }

    }

    private void StopCombatSong()
    {
        if (!restaurantAudioSource.isPlaying) restaurantAudioSource.Play();
        if (combatAudioSource.isPlaying) combatAudioSource.Pause();
    }


    private void PlaySong()
    {
        restaurantAudioSource.clip = restaurantPlaylist[currentSong];
        restaurantAudioSource.Play();
    }

    private void NextSong()
    {
        currentSong++;
        if (currentSong >= restaurantPlaylist.Count) { currentSong = 0; }
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
        if (isRadioOn)
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
