using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    // Start is called before the first frame update
    public static ParticleSystem playvfx(ParticleSystem vfx, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        var temp = Instantiate(vfx, position, rotation);
        if (parent != null)
        {
            temp.transform.SetParent(parent);
        }
        temp.Play();
        return temp;
    }
}
