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
        Vector3 offset = RandomPointer.GetRandomPointInRadius(radius);

        TCharacter character = Object.Instantiate(_characterPrefab, _spawnPoint.position + offset, Quaternion.identity);

        return character;
    }
}
