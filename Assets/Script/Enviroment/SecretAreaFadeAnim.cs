using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAreaFadeAnim : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            print("FadeOut");
            anim.Play("FadeOut");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            print("FadeIn");
            anim.Play("FadeIn");
        }
    }
}
