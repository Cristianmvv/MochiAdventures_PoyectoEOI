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

        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += Input.GetAxis("Mouse ScrollWheel");
        if (GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 7)
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 7;
        if (GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 15)
            GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 15;

    }


}
