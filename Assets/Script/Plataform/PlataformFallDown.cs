using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformFallDown : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name.Contains("bone") || collision.gameObject.CompareTag("MochiSphere")) && collision.transform.position.y > transform.position.y)
            Invoke("DesactiveKinematic", 3);
    }

    void DesactiveKinematic()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().enabled = false;
    }

}
