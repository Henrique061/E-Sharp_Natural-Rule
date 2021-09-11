using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraChanges : MonoBehaviour
{
    [SerializeField] PostProcessProfile profile;

    [Header("Bloom")]
    [Range(0f, 15f)]
    [SerializeField] float Bloom = 1;

    [Header("Chromatic Aberration")]
    [Range(0f, 1f)]
    [SerializeField] float ChromaticAberration = 0;

    [Header("Color Grading")]
    [Range(-100, 100)]
    [SerializeField] int Saturation = 10;
    [Range(-6,6)]
    [SerializeField] float Brightness = 0;
    [Range(-100, 100)]
    [SerializeField] int Contrast = 18;

    Bloom b;
    ChromaticAberration ca;
    ColorGrading cg;

    void Awake()
    {
        b = profile.GetSetting<Bloom>();
        ca = profile.GetSetting<ChromaticAberration>();
        cg = profile.GetSetting<ColorGrading>();
    }

    void Update()
    {
        b.intensity.Override(Bloom);

        ca.intensity.Override(ChromaticAberration);

        cg.postExposure.Override(Brightness);
        cg.saturation.Override(Saturation);
        cg.contrast.Override(Contrast);
    }
}
