using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float minShootInterval = 0.5f;
    public float maxShootInterval = 2.0f;
    public float bulletSpeed = 10f;

    private void Start()
    {
        ScheduleNextShot();
    }

    private void ScheduleNextShot()
    {
        float shootDelay = Random.Range(minShootInterval, maxShootInterval);
        Invoke("Shoot", shootDelay);
    }

    private void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }


            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

            BulletBehavior bulletScript = bullet.AddComponent<BulletBehavior>();
            bulletScript.SetShooter(gameObject);
        }

        ScheduleNextShot();
    }
}

public class BulletBehavior : MonoBehaviour
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
            BlockHealth blockHealth = collision.gameObject.GetComponent<BlockHealth>();
            if (blockHealth != null)
            {
                blockHealth.TakeDamage();
            }
            Debug.Log("Shield hit");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
