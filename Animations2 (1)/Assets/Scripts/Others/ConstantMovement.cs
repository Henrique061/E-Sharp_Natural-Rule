using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ConstantMovement : MonoBehaviour
{
    public GameObject mask;
    [HideInInspector]public float expandDifference = -0.002f;

    public Vector2 PositionAxis = new Vector2(0, 0);

    public Vector3 RotationAxis = new Vector3(0, 0, 0);
     
    public Vector3 ScaleAxis = new Vector3(0, 0,0);

    [HideInInspector] public float speed = 6.9f;

    [Header("Progress Bar Only")]
    [SerializeField] Transform max;
    [SerializeField] bool position, rotation, scale;
    Vector3 distanceP, distanceS;
    Quaternion distanceR;
    [SerializeField] TimelineClip timeline;

    private void Awake()
    {
        if (mask != null) {
            if (ScaleAxis.x != 0 || ScaleAxis.y != 0)
            {
                expandDifference = mask.GetComponent<ConstantMovement>().expandDifference;
                ScaleAxis = mask.GetComponent<ConstantMovement>().ScaleAxis * expandDifference;
            }
        }

        if (max != null) {
            distanceP = max.position - transform.position;
            distanceS = max.localScale - transform.localScale;
            distanceR = max.rotation * Quaternion.Inverse(transform.rotation);
        }
        if (timeline != null) {
            distanceP = new Vector3(distanceP.x/ (float)timeline.duration, distanceP.y / (float)timeline.duration);
            distanceS = new Vector3(distanceS.x / (float)timeline.duration, distanceS.y / (float)timeline.duration);
        }
    }
    void Update()
    {
        if (max == null)
            noMax();
        else if (max != null && timeline != null)
            Max();
        else Debug.Log("can't find timeline");
            
    }

    void noMax() {
        if (PositionAxis.x != 0 || PositionAxis.y != 0)
        {
            transform.Translate(PositionAxis * Time.deltaTime, Space.World);
        }

        if (RotationAxis.x != 0 || RotationAxis.y != 0 || RotationAxis.z != 0)
        {
            transform.Rotate(RotationAxis * Time.deltaTime, Space.Self);
        }

        if (ScaleAxis.x != 0 || ScaleAxis.y != 0)
        {
            transform.localScale += ScaleAxis * Time.deltaTime * speed;
        }
    }

    void Max() {
        if (position)
        {
            if(transform.position.x < max.position.x || transform.position.y < max.position.y)
            transform.Translate(distanceP * Time.deltaTime, Space.World);
        }

        if (rotation)
        {
            transform.Rotate(RotationAxis * Time.deltaTime, Space.Self);
        }

        if (scale)
        {
             if (transform.localScale.x < max.localScale.x || transform.localScale.y < max.localScale.y)
               transform.localScale += distanceS * Time.deltaTime;
        }
    }
}
