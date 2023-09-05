using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalForceArea : MonoBehaviour
{
    public float forceRotation = 0;
    public float amountOfForce;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            collision.attachedRigidbody.AddForce(Vector3.up * amountOfForce);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            //Gizmos.matrix = rotationMatrix;
            Gizmos.color = Color.cyan;
            var rotatedVector = Quaternion.AngleAxis(forceRotation, Vector3.up) * Vector3.up;

            DrawArrow.ForGizmo(transform.position, rotatedVector);
            //Gizmos.DrawArrow(transform.position, new Vector2(transform.localScale.x, transform.localScale.y));
        }
    }
}
