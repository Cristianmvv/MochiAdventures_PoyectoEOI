using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTransformCollider : MonoBehaviour
{
    [SerializeField] bool disableMovement;
    [SerializeField] bool disableTransform;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            if (disableMovement)
                MochiManager.Instance.disableMovement = true;

            if (disableTransform)
                MochiManager.Instance.disableTransform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            MochiManager.Instance.disableTransform = false;
            MochiManager.Instance.disableMovement = false;
        }
    }
}
