using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour
{
    public Rigidbody body;
    public float force;
    private int collisionCount;
    [SerializeField] private Collider sphereCollider;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<Collider>();
        Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
        Debug.Log(transform.rotation.eulerAngles);
        Debug.Log(transform.localRotation.eulerAngles);
        Debug.Log(transform.lossyScale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        body.AddForce(collision.gameObject.transform.up * (force / collisionCount), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Debug.Log(other.ClosestPointOnBounds(transform.position));
    }
}
