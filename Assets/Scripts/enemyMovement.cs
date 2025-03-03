using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float stepForward = 1f;
    private Rigidbody rb;
    private Vector3 moveDirection = Vector3.right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection *= -1;
            transform.position += Vector3.forward * stepForward;
        }
    }
}
