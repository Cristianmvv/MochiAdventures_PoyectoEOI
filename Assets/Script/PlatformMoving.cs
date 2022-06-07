using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform startPoint;
    public Transform endPoint;

    public float speed;

    private Vector3 moveTo;

    void Start()
    {
        moveTo = endPoint.position;
    }
    void Update()
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, moveTo, speed * Time.deltaTime);

        if (Vector3.Distance(objectToMove.transform.position, endPoint.position) < 1)
        {
            moveTo = startPoint.position;
        }

        if (Vector3.Distance(objectToMove.transform.position, startPoint.position) < 1)
        {
            moveTo = endPoint.position;
        }
    }
}
