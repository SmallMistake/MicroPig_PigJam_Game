using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayItBullet : MonoBehaviour
{
    public float Speed, Lifespan;
    [SerializeField]
    new private Rigidbody2D rigidbody;

    private void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * this.Speed * GameManager.FixedDeltaTime, Space.Self);
        this.Lifespan -= GameManager.FixedDeltaTime;
        if (this.Lifespan <= 0f) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
