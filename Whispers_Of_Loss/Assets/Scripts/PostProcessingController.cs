using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessingController : MonoBehaviour
{
    public static PostProcessingController instance;
    public VolumeProfile globalVolume;
    Vignette m_Vignette;
    ChromaticAberration m_ChromaticAberration;

    public float viganetteSpeed, viganetteMagnitude;
    [Header("Chromatic Effect")]
    public float lerpSpeed = .1f;
    ClampedFloatParameter cIntensity;

    [Header("Chromatic Effect")]
    public float satSpeed = .3f;
    ColorAdjustments m_ColorAdjustments;
    ClampedFloatParameter satIntensity;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(this);
        }
    }

    private void Start()
    {
        Vignette vgnt;
        if (globalVolume.TryGet<Vignette>(out vgnt))
        {
            m_Vignette = vgnt;
        }

        //ChromaticAberration
        ChromaticAberration ch;
        if (globalVolume.TryGet<ChromaticAberration>(out ch))
        {
            m_ChromaticAberration = ch;
        }
        cIntensity = m_ChromaticAberration.intensity;
        cIntensity.value = 0;


        //Color Adjustments
        ColorAdjustments adj;
        if (globalVolume.TryGet<ColorAdjustments>(out adj))
        {
            m_ColorAdjustments = adj;
        }
        satIntensity = m_ColorAdjustments.saturation;
        satIntensity.value = 0;
    }

    public void ViganetteBlink()
    {
        float x = Mathf.Sin(viganetteSpeed * Time.time) * viganetteMagnitude;
        m_Vignette.intensity.Override(x);
    }

    public void SetVigannete(float x)
    {
        m_Vignette.intensity.Override(x);
    }

    public void SetChromaticEffect(bool val)
    {
        StartCoroutine(ChromaticEffect(val));
    }
    IEnumerator ChromaticEffect(bool val)
    {
        float targetIntensity = val ? 1f : 0f; // Target intensity based on the boolean value
        ClampedFloatParameter intensity = m_ChromaticAberration.intensity;
        while (Mathf.Abs(intensity.value - targetIntensity) > 0.01f)
        {
            m_ChromaticAberration.intensity.value = Mathf.MoveTowards(intensity.value, targetIntensity, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }

    public void SetSaturationEffect(bool val)
    {
        StartCoroutine(SaturationEffect(val));
    }

    IEnumerator SaturationEffect(bool val)
    {
        float targetIntensity = val ? -100f : 0f; // Target intensity based on the boolean value

        while (Mathf.Abs(satIntensity.value - targetIntensity) > 0.01f)
        {
            // Gradually move towards the target intensity
            satIntensity.value = Mathf.MoveTowards(satIntensity.value, targetIntensity, Time.deltaTime * satSpeed);
            yield return null;
        }
    }
}
