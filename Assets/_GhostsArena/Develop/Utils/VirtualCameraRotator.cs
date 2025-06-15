using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class VirtualCameraRotator : MonoBehaviour
{
    private const int RightMouseButton = 1;

    private CinemachineOrbitalTransposer _orbitalTransposer;
    private float _initialSpeed;

    private void Awake()
    {
        var virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        _initialSpeed = _orbitalTransposer.m_XAxis.m_MaxSpeed;
        _orbitalTransposer.m_XAxis.m_MaxSpeed = 0;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(RightMouseButton))
            _orbitalTransposer.m_XAxis.m_MaxSpeed = _initialSpeed;
        else
            _orbitalTransposer.m_XAxis.m_MaxSpeed = 0f;
    }
}
