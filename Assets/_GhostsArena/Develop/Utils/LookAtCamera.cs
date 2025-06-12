using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 lookPosition = Camera.main.transform.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);
    }
}
