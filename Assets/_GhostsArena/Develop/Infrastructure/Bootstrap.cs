using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private List<EnemiesSpawner> _enemiesSpawners;

    private void Awake()
    {
        CharacterControllerCharacter mainHeroCharacter = _mainHeroSpawner.Spawn();

        foreach (var spawner in _enemiesSpawners)
            spawner.Activate(mainHeroCharacter);
    }
}
