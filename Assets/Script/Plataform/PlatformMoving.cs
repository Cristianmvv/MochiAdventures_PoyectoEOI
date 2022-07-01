using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform startPoint;
    public Transform endPoint;
    [SerializeField] float waitTime;

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
            Invoke("MoveToStart", waitTime);
        }

        if (Vector3.Distance(objectToMove.transform.position, startPoint.position) < 1)
        {
            Invoke("MoveToEnd", waitTime);
        }
    }

    void MoveToStart()
    {
        moveTo = startPoint.position;
    }

    void MoveToEnd()
    {
        moveTo = endPoint.position;
    }

    
}
