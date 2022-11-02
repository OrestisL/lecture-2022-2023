using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float range;
    [SerializeField] private float frequency;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.right * range * Mathf.Sin(Time.time * frequency);
    }
}
