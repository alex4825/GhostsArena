using UnityEngine;

public static class RandomPointer
{
    public static Vector3 GetRandomPointInRadius(float maxRadius)
    {
        float randomAngle = Random.Range(0, 360f);
        float randomDirectionLength = Random.Range(0, maxRadius);
        Vector3 randomDirectionNormalized = (Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward).normalized;

        return randomDirectionNormalized * randomDirectionLength;
    }
}
