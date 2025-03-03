using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float playerSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float playerMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(playerMove * playerSpeed, rb.velocity.y, 0);
    }
}
