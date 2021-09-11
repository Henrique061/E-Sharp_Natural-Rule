using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;

    [SerializeField]PlayableDirector timeline;
    double targetProgress;
    double progress = 0;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        targetProgress = timeline.duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < 1)
        {
            progress = timeline.time;
            slider.value = (float)(progress / targetProgress);
        }
    }
}
