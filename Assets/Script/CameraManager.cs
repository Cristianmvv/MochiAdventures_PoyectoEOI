using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    private CinemachineVirtualCamera cinemachine;
    [SerializeField] GameObject mochiManager;


    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();

    }

    private void Update()
    {
        if (MochiManager.Instance.IsSphere) cinemachine.Follow = mochiManager.transform.GetChild(0);
        else cinemachine.Follow = MochiManager.Instance.slimeCenter.transform;
    }


}
