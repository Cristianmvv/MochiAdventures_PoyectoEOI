using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceZone : MonoBehaviour
{
    public Vector2 force;
    [SerializeField] bool effectEveryone;
    [SerializeField] bool dissableMochiTransform;
    [SerializeField] bool forceMochiSlime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MochiManager.Instance.IsSphere && forceMochiSlime)
            MochiManager.Instance.InstantiateMochiSlime();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SlimePerimeter"))
            MochiManager.Instance.inForceZone = true;

        if (!effectEveryone && collision.gameObject.CompareTag("SlimeCenter"))
        {
            for (int i = 0; i < collision.transform.parent.childCount; i++)
            {
                collision.transform.parent.GetChild(i).GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
                MochiManager.Instance.inForceZone = true;
            }
            if (!MochiManager.Instance.IsSphere && dissableMochiTransform) MochiManager.Instance.disableTransform = true;
        }
        if (effectEveryone)
        {
            if (!(collision.CompareTag("Pickups/Fruit") || collision.CompareTag("Pickups/FruitX5")))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
                if (!MochiManager.Instance.IsSphere && dissableMochiTransform) MochiManager.Instance.disableTransform = true;
            }    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if  (collision.gameObject) /*(collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))*/
        {
            MochiManager.Instance.disableTransform = false;
            MochiManager.Instance.inForceZone = false;

        }

    }

    //private void OnParticleTrigger()
    //{
    //    ParticleSystem particle = GetComponent<ParticleSystem>();

    //    particle.getT
    //}

}
