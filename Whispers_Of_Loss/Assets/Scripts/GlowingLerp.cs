using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingLerp : MonoBehaviour
{
    public Material material;
    public float lerpSpeed = 1.0f;
    private Color targetColor;
    private bool isLerping = true;

    void Start()
    {
        if (material == null)
        {
            material = GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        if (isLerping)
        {
            // Calculate the target color based on time
            targetColor = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time * lerpSpeed, 1.0f));

            // Set the emissive color of the material
            material.SetColor("_EmissiveColor", targetColor);
        }
    }
}
