using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceForceArea : MonoBehaviour
{
    public float amountOfForce;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            Vector3 directionVector = new Vector3(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y, 0).normalized;
            collision.attachedRigidbody.AddForce(directionVector * amountOfForce);
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position, 1);
            //DrawArrow.ForGizmo(transform.position, rotatedVector);
        }
    }
}
