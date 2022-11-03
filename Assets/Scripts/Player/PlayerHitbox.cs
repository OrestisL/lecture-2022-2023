using UnityEngine;

public class PlayerHitbox : MonoBehaviour 
{
    public AudioClip hitClip;
    private SphereCollider col;
    private float forceAmount;

    public void ChangeSize(float r, float force)
    {
        if (!col)
            col = GetComponent<SphereCollider>();

        col.radius = r;
        forceAmount = force;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.TryGetComponent(out Rigidbody body);
        if (body != null)
        {
            body.AddForce(col.ClosestPointOnBounds(other.transform.position) * forceAmount, ForceMode.VelocityChange);
            Audio.Instance.PlaySFX(hitClip);
        }
    }
}
