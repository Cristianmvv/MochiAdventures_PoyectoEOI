using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMochiTransform : MonoBehaviour
{
    public bool activate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("bone0"))
            MochiManager.Instance.disableTransform = activate;
    }
}
