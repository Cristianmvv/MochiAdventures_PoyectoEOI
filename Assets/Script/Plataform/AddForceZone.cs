using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceZone : MonoBehaviour
{
    public Vector2 force;
    [SerializeField] bool effectEveryone;
    [SerializeField] bool dissableMochiTransform;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!effectEveryone && collision.gameObject.CompareTag("SlimeCenter"))
        {
            for (int i = 0; i < collision.transform.parent.childCount; i++)
            {
                collision.transform.parent.GetChild(i).GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
            }
            if(dissableMochiTransform) MochiManager.Instance.disableTransform = true;
        }
        if (effectEveryone)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!effectEveryone && collision.gameObject.CompareTag("SlimeCenter"))
        {
            if (dissableMochiTransform) MochiManager.Instance.disableTransform = false;
        }
    }
}
