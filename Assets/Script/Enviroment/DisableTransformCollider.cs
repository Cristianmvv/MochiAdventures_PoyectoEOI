using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTransformCollider : MonoBehaviour
{
    [SerializeField] bool disableMovement;
    [SerializeField] bool disableTransform;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (disableMovement)
            MochiManager.Instance.disableMovement = true;

        if (disableTransform)
            MochiManager.Instance.disableTransform = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MochiManager.Instance.disableTransform = false;
        MochiManager.Instance.disableMovement = false;
    }

}
