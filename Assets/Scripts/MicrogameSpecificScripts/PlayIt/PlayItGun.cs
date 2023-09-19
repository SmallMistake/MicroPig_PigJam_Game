using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayItGun : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private UnityEvent onShoot;

    [SerializeField]
    private UnityEvent onOutOfAmmo;

    private float cooldown = 0.3f;
    private float cooldownRemaining = 0f;

    [SerializeField]
    MicrogameManager microgameManager;

    private void Update()
    {
        this.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f), Vector3.back);
        if (Input.GetMouseButtonDown(0) && this.cooldownRemaining <= 0f)
        {
            if (!microgameManager.GetGameOverStatus())
            {
                this.Fire();
            }
            else
            {
                onOutOfAmmo?.Invoke();
            }
        }
        this.cooldownRemaining -= GameManager.DifficultyDeltaTime;

    }

    void Fire()
    {
        var bullet = Instantiate(this.bulletPrefab, this.muzzle.position, this.transform.rotation);
        this.cooldownRemaining = this.cooldown;
        bullet.SetActive(true);
        onShoot?.Invoke();
     
        
    }
}
