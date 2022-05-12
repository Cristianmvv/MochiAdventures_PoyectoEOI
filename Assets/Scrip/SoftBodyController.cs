using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodyController : MonoBehaviour
{
    [SerializeField] int speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        rb.velocity = Vector2.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    }


}
