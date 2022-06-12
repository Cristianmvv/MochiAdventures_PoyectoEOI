using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLaunch : MonoBehaviour
{
    [SerializeField] float launchForce;
    [SerializeField] float launchDelay;
    [SerializeField] float turnDelay;
    bool startTimer;
    public float launchTimer;
    Animator anim;

    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.up * launchForce, Color.red);
        if (startTimer) launchTimer += Time.deltaTime;  //  He intentado que cuente en el "OnStay" pero cuenta mal el tiempo, es como si el "OnStay" funcionara diferente a nivel de frames

        if (launchTimer >= turnDelay)
        {
            anim.Play("TurnCannon");
            print("Animacion");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MochiManager.Instance.InstantiateMochiSphere();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MochiManager.Instance.disableTransform = true;

        if (!startTimer && collision.gameObject.CompareTag("MochiSphere"))
        {
            startTimer = true;
        }

        if (launchTimer >= launchDelay)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * launchForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (startTimer && collision.gameObject.CompareTag("MochiSphere"))
        {
            startTimer = false;
            launchTimer = 0;
        }

        MochiManager.Instance.disableTransform = false;
    }
}
