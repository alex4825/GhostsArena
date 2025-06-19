using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private List<EnemiesSpawner> _enemiesSpawners;
    [SerializeField] private LoadingScreen _loadingScreen;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();

        yield return new WaitForSeconds(2f);

        _loadingScreen.Hide();

        CharacterControllerCharacter mainHeroCharacter;

        yield return StartCoroutine(_mainHeroSpawner.Spawn(hero =>
        {
            mainHeroCharacter = hero;

            foreach (var spawner in _enemiesSpawners)
                spawner.Activate(mainHeroCharacter);
        }));
    }
}
