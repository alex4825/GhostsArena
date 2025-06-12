using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCameraRotator : MonoBehaviour
{
    private CinemachineOrbitalTransposer _orbitalTransposer;
    private float _initialSpeed;

    private void Awake()
    {
        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        _initialSpeed = _orbitalTransposer.m_XAxis.m_MaxSpeed;
        _orbitalTransposer.m_XAxis.m_MaxSpeed = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            _orbitalTransposer.m_XAxis.m_MaxSpeed = _initialSpeed;
        else
            _orbitalTransposer.m_XAxis.m_MaxSpeed = 0f;
    }
}
