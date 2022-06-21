using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAreaFadeAnim : MonoBehaviour
{
    Animator anim;
    bool hasEntered;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    hasEntered = !hasEntered;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!hasEntered && collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
    //    {
    //        print("FadeOut");
    //        anim.Play("FadeOut");
    //    }
    //    if (hasEntered && collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
    //    {
    //        if (Input.GetKey(KeyCode.W)) return;
    //        print("FadeIn");
    //        anim.Play("FadeIn");
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.W)) return;
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            print("FadeOut");
            anim.Play("FadeOut");
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (Input.GetKeyDown(KeyCode.W)) return;
    //    if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
    //    {
    //        print("FadeIn");
    //        anim.Play("FadeIn");
    //    }
    //}
}
