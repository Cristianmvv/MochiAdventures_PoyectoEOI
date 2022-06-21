using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
/// <summary>
/// WIP CODE
/// el Sprite Shape tiene un fallo que no permite modificar directamente el color usando la libreria de UnityEngine.U2D
/// por lo que se haria modificando el propio material
/// </summary>
public class SecretAreaFade : MonoBehaviour
{
    [SerializeField] Material material;
    [Range(0f, 1f)]
    [SerializeField] float fadeQuantity;
    [SerializeField] float fadeTime;
    public Color color;

    private void Update()
    {
        material.color = color;
        if (Input.GetMouseButtonDown(0)) color = Color.cyan;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            
            StopCoroutine(FadeIn());

            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            StopCoroutine(FadeOut());

            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        while (color.a > 0)
        {
            print("FadeIn" + color.a);
            color.a -= fadeQuantity;
            if (color.a < 0) color.a = 0;
            yield return new WaitForSeconds(fadeTime);
        }
    }

    IEnumerator FadeOut()
    {
        while (color.a < 1)
        {
            print("FadeOut" + color.a);
            color.a += fadeQuantity;
            if (color.a > 1) color.a = 1;
            yield return new WaitForSeconds(fadeTime);
        }
    }

}
