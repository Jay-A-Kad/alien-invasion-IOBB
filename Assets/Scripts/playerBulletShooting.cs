using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;
    public AudioSource shootAudio;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (shootAudio != null)
        {
            shootAudio.Play();
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;

        }

        PlayerBulletBehavior bulletScript = bullet.AddComponent<PlayerBulletBehavior>();
        bulletScript.SetShooter(gameObject);
    }
}

public class PlayerBulletBehavior : MonoBehaviour
{
    private GameObject shooter;

    public void SetShooter(GameObject shooterObject)
    {
        shooter = shooterObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Player bullet ignored sheild!!");
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();
            }
            Debug.Log("enemy hit");
            Destroy(gameObject);
        }
    }
}
