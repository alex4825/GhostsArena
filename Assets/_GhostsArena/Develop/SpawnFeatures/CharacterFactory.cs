using UnityEngine;

public class CharacterFactory<TCharacter> where TCharacter : Character
{
    private TCharacter _characterPrefab;
    private Transform _spawnPoint;

    public CharacterFactory(TCharacter characterPrefab, Transform spawnPoint)
    {
        _characterPrefab = characterPrefab;
        _spawnPoint = spawnPoint;
    }

    public TCharacter Spawn()
    {
        TCharacter character = Object.Instantiate(_characterPrefab, _spawnPoint);
        return character;
    }

    public TCharacter SpawnInRandom(float radius)
    {
        float maxColliderRadius = _characterPrefab.GetComponent<Collider>().bounds.extents.magnitude;
        int maxAttempts = 10;
        bool hasIntersections;
        int attempt = 0;
        Vector3 offset;

        do
        {
            attempt++;
            offset = RandomPointer.GetRandomPointInRadius(radius);
            hasIntersections = Physics.CheckSphere(_spawnPoint.position + offset, maxColliderRadius);
        }
        while (hasIntersections || attempt < maxAttempts);

        TCharacter character = Object.Instantiate(_characterPrefab, _spawnPoint.position + offset, Quaternion.identity);

        return character;
    }
}
