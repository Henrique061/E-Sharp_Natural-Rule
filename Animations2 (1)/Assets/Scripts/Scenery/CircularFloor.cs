using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var poli = GetComponent<PolygonCollider2D>();

        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();

        edge.points = poli.points;
        edge.edgeRadius = 0.05f;

        Destroy(poli);
    }
}
