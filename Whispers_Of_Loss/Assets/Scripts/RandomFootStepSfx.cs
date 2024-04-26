using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFootStepSfx : MonoBehaviour
{
    public string[] footstepsSfxNames;
    int randomIndex;
    public void PlayRandomFootStep()
    {
        randomIndex = Random.Range(0, footstepsSfxNames.Length);
        AudioManager.instance?.PlaySound(footstepsSfxNames[randomIndex]);
    }
}
