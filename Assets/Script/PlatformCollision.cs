using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bone") || collision.gameObject.CompareTag("MochiSphere"))
            MochiManager.Instance.transform.GetChild(0).SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("bone") || collision.gameObject.CompareTag("MochiSphere"))
            MochiManager.Instance.transform.GetChild(0).SetParent(null);
    }
}
