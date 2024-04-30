using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingHopeScript : MonoBehaviour
{
    public GameObject livingRoomBarrier;
    public GameObject blackImage;
    public AudioSource screamingSource;
    public AudioSource runSource;
    public GameObject pills;

    public float volumeChangeSpeed = .1f;
    public AudioSource [] fireSounds;


    public void EnableGuidingScene()
    {
        runSource.Pause();
        StartCoroutine(GuidingScene());
        StartCoroutine(FireVolumeHighLow(false));
    }
    IEnumerator GuidingScene()
    {
        livingRoomBarrier.SetActive(false);
        blackImage.SetActive(true);
        AudioManager.instance.PlaySound("ItsOkay");
        screamingSource.volume = 0.09f;

        yield return new WaitForSeconds(15f);
        screamingSource.volume = 0.1f;
        StartCoroutine(FireVolumeHighLow(true));
        blackImage.SetActive(false);
        pills.SetActive(true);
    }

    IEnumerator FireVolumeHighLow(bool increaseVolume)
    {
        float startVolume = increaseVolume ? 0.037f : 0.115f;
        float targetVolume = increaseVolume ? 0.115f : 0.037f;
        float currentVolume;

        // Get the current volume of all fire sounds
        foreach (var sound in fireSounds)
        {
            if (sound != null)
            {
                sound.volume = startVolume;
            }
        }

        while (Mathf.Abs(fireSounds[0].volume - targetVolume) > 0.01f)
        {
            currentVolume = Mathf.MoveTowards(fireSounds[0].volume, targetVolume, Time.deltaTime * volumeChangeSpeed);

            // Apply the current volume to all fire sounds
            foreach (var sound in fireSounds)
            {
                if (sound != null)
                {
                    sound.volume = currentVolume;
                }
            }

            yield return null;
        }
    }
}
