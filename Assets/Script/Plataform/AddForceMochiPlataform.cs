using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceMochiPlataform : MonoBehaviour
{
    public Vector2 force;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SlimePerimeter"))
        {
            print("colisionando");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        }
    }
}
