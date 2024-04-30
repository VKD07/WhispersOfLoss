using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxTriggers : MonoBehaviour
{
    public AudioClip[] whispersToPlay;
    public float whispersVolume = .4f;
    public float whispersTimeInterval = 1;

    [Header("Player Movement")]
    public bool disablePlayerMovement = true;
    public float disableTime = 5f;

    [Header("Chromatic Effect")]
    public bool enableChromatic;
    public float chromaticTime = 5f;
    public bool playBellRing;

    [Header("Saturation Effect")]
    public bool enableSaturation;
    public float saturationTime = 5f;

    [Header("Environment Change")]
    public bool enableEnvironmentChange;
    public float timeToNormal = 2f;
    bool hasTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) { return; }
        if (other.GetComponent<Player>() != null)
        {
            hasTriggered = true;
            StartCoroutine(PlayWhispers());
        }
    }

    IEnumerator PlayWhispers()
    {
        if (enableEnvironmentChange)
        {
            EnvironmentMaterialChanger.instance?.ChangeMaterialsTexture(true);
            StartCoroutine(ChangeMaterialToNormal());
        }
        if (disablePlayerMovement)
        {
            PlayerMovement.instance?.DisablePlayerMovement(true);
            StartCoroutine(EnablePlayerMovement());
        }

        if (enableSaturation)
        {
            PostProcessingController.instance?.SetSaturationEffect(true);
            StartCoroutine(DisableSaturation());
        }
        if (enableChromatic)
        {
            PostProcessingController.instance?.SetChromaticEffect(true);
            StartCoroutine(DisableChromatic());
        }
        if (playBellRing)
        {
            AudioManager.instance?.PlaySound("BellRing");
        }
        for (int i = 0; i < whispersToPlay.Length; i++)
        {
            AudioManager.instance?.PlayAudioClip(whispersToPlay[i], whispersVolume);
            yield return new WaitForSeconds(whispersTimeInterval);
        }
    }


    IEnumerator EnablePlayerMovement()
    {
        yield return new WaitForSeconds(disableTime);
        PlayerMovement.instance?.DisablePlayerMovement(false);
    }

    IEnumerator DisableChromatic()
    {
        yield return new WaitForSeconds(chromaticTime);
        PostProcessingController.instance?.SetChromaticEffect(false);
    }

    IEnumerator DisableSaturation()
    {
        yield return new WaitForSeconds(saturationTime);
        PostProcessingController.instance?.SetSaturationEffect(false);
    }

    IEnumerator ChangeMaterialToNormal()
    {
        yield return new WaitForSeconds(timeToNormal);
        EnvironmentMaterialChanger.instance?.ChangeMaterialsTexture(false);
    }
}
