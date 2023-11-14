using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Auto_Singleton<SoundManager>
{
    AudioSource addCoinSource;
    AudioSource removeCoinSource;
    AudioSource interactiveSource;

    AudioSource openRestaurantSource;
    AudioSource closeRestaurantSource;

    public AudioClip addCoinSound;
    public AudioClip removeCoinSound;
    public AudioClip interactiveSound;
    public AudioClip openRestaurantSound;
    public AudioClip closeRestaurantSound;

    private void Awake()
    {
        addCoinSource = InitializedAudioSource(false, false);
        removeCoinSource = InitializedAudioSource(false, false);
        interactiveSource = InitializedAudioSource(false, false);
        openRestaurantSource = InitializedAudioSource(false, false);
        closeRestaurantSource = InitializedAudioSource(false, false);
    }

    public void PlayAddCoinSound()
    {
        addCoinSource.PlayOneShot(addCoinSound);
    }

    public void PlayRemoveCoinSound()
    {
        removeCoinSource.PlayOneShot(addCoinSound);
    }

    public void PlayInteractiveSound()
    {
        interactiveSource.PlayOneShot(interactiveSound);
    }

    public void PlayOpenRestaurantSound()
    {
        openRestaurantSource.PlayOneShot(openRestaurantSound);
    }

    public void PlayCloseRestaurantSound()
    {
        closeRestaurantSource.PlayOneShot(closeRestaurantSound);
    }

    AudioSource InitializedAudioSource(bool loop, bool threeDsound)
    {
        AudioSource newSource = transform.AddComponent<AudioSource>();

        if (threeDsound) newSource.dopplerLevel = 1;
        else newSource.dopplerLevel = 0;

        newSource.loop = loop;
        return newSource;
    }

}
