using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBacknForth : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 60f;
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * rotateSpeed, 60) - 30);
    }
}
