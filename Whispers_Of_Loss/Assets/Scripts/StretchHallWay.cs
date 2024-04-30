using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StretchHallWay : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float newMoveSpeed;
    public CinemachineVirtualCamera virtualCamera;
    public float targetFovValue = 135f;
    public float disableHoverTime;
    public float lerpSpeed = 2f;
    float initSpeed;
    bool hasEnabled;
    void Start()
    {
        initSpeed = playerMovement.moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasEnabled) return;
        if (other.GetComponent<Player>() != null)
        {
            hasEnabled = true;
            StartCoroutine(EnableFOV(true));
            StartCoroutine(DisableFOV());
        }
    }

    IEnumerator EnableFOV(bool val)
    {
        float targetVal = val ? targetFovValue : 60; // Target intensity based on the boolean value
        while (Mathf.Abs(virtualCamera.m_Lens.FieldOfView - targetVal) > 0.01f)
        {
            // Gradually move towards the target intensity
            virtualCamera.m_Lens.FieldOfView = Mathf.MoveTowards(virtualCamera.m_Lens.FieldOfView, targetVal, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }

    IEnumerator DisableFOV()
    {
        yield return new WaitForSeconds(disableHoverTime);
        StartCoroutine(EnableFOV(false));
    }
}
