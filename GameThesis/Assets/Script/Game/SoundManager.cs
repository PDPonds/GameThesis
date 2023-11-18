using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Auto_Singleton<SoundManager>
{
    AudioSource addCoinSource;
    AudioSource removeCoinSource;

    AudioSource interactiveSource;
    AudioSource cantIneractiveSource;

    AudioSource openRestaurantSource;
    AudioSource closeRestaurantSource;

    AudioSource upgradeSource;
    AudioSource summarySource;

    public AudioClip addCoinSound;
    public AudioClip removeCoinSound;
    public AudioClip interactiveSound;
    public AudioClip cantIneractiveSound;
    public AudioClip openRestaurantSound;
    public AudioClip closeRestaurantSound;
    public AudioClip upgradeSound;
    public AudioClip summarySound;



    private void Awake()
    {
        addCoinSource = InitializedAudioSource(false, false);
        removeCoinSource = InitializedAudioSource(false, false);
        interactiveSource = InitializedAudioSource(false, false);
        openRestaurantSource = InitializedAudioSource(false, false);
        closeRestaurantSource = InitializedAudioSource(false, false);
        upgradeSource = InitializedAudioSource(false, false);
        summarySource = InitializedAudioSource(false, false);
        cantIneractiveSource = InitializedAudioSource(false, false);
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
        openRestaurantSource.volume = 0.2f;
        openRestaurantSource.PlayOneShot(openRestaurantSound);
    }

    public void PlayCloseRestaurantSound()
    {
        closeRestaurantSource.PlayOneShot(closeRestaurantSound);
    }

    public void PlayUpgradeSound()
    {
        upgradeSource.PlayOneShot(upgradeSound);
    }

    public void PlaySummarySound()
    {
        summarySource.PlayOneShot(summarySound);
    }

    public void PlayCantInteractSound()
    {
        cantIneractiveSource.PlayOneShot(cantIneractiveSound);
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
