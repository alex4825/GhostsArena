using UnityEngine;

public class DirectionalRotator
{
    private Transform _transform;
    private float _rotationSpeed;
    private Vector3 _currentDirection;

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void SetDirection(Vector3 direction) => _currentDirection = direction;

    public void Update()
    {
        if (_currentDirection.magnitude < 0.05f)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(_currentDirection.normalized);

        float step = _rotationSpeed * Time.deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }
}
