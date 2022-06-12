using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTransformCollider : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        MochiManager.Instance.disableTransform = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MochiManager.Instance.disableTransform = false;
    }
}
