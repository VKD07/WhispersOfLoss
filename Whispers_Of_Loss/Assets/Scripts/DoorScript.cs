using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
    public float closeValue;
    public float openValue;
    public float doorSpeed;
    public bool sfx;
    public bool closed;
    public AudioSource sfxSource;
    public UnityEvent OnDoorClosed;

    public void SetOpenOrCloseDoor(bool close)
    {
        StartCoroutine(OpenOrCloseDoor(close));
    }

    IEnumerator OpenOrCloseDoor(bool close)
    {
        float targetAngle = close ? closeValue : openValue; // Target angle based on the boolean value

        while (Quaternion.Angle(transform.localRotation, Quaternion.Euler(0, targetAngle, 0)) > 0.01f)
        {
            // Gradually rotate towards the target angle
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * doorSpeed);
            yield return null;
        }

        if (!closed && sfx)
        {
            closed = true;
            sfxSource.Play();
            OnDoorClosed.Invoke();
        }
    }
}
