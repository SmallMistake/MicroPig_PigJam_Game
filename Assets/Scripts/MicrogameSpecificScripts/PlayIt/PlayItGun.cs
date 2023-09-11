using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayItGun : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject bulletPrefab;
    
    private float cooldown = 0.3f;
    private float cooldownRemaining = 0f;

    private void Update()
    {
        this.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f), Vector3.back);
        if (Input.GetMouseButtonDown(0) && this.cooldownRemaining <= 0f)
        {
            this.Fire();
        }
        this.cooldownRemaining -= GameManager.DeltaTime;

    }

    void Fire()
    {
        var bullet = Instantiate(this.bulletPrefab, this.muzzle.position, this.transform.rotation);
        this.cooldownRemaining = this.cooldown;
        bullet.SetActive(true);
     
        
    }
}
