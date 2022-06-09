using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceMochi : MonoBehaviour
{
    public Vector3 force;
    public float timeToTransform;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter"))
        {
            print("colisionando");
            collision.transform.parent.GetChild(8).GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter"))
        {
            MochiManager.Instance.canTransform = false;
            CancelInvoke();
            Invoke("ActivateTransformMode", timeToTransform);
        }
            
    }
    public void ActivateTransformMode()
    {
        MochiManager.Instance.canTransform = true;
    }
}
