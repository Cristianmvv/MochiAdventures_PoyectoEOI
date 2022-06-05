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

        if(objectToMove.transform.position == endPoint.position)
        {
            moveTo = startPoint.position;
        }

        if (objectToMove.transform.position == startPoint.position)
        {
            moveTo = endPoint.position;
        }
    }
}
