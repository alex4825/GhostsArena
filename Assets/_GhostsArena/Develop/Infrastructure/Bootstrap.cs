using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform _mainHeroSpawnPoint;
    [SerializeField] private List<EnemiesSpawner> _enemiesSpawners;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    private ControllersUpdateService _controllersUpdateService;
    private GameplayCycle _gameplayCycle;

    private void Awake()
    {
        StartCoroutine(StartProcess());
        Cursor.lockState = CursorLockMode.Confined;
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();

        _controllersUpdateService = new ControllersUpdateService();
        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();
        MainHeroConfig mainHeroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        LevelConfig levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");

        MainHeroSpawner mainHeroSpawner = new(_controllersUpdateService, controllersFactory, charactersFactory, mainHeroConfig);

        foreach (var spawner in _enemiesSpawners)
            spawner.Initialize(_controllersUpdateService, controllersFactory, charactersFactory);

        yield return new WaitForSeconds(2f);

        _loadingScreen.Hide();

        _gameplayCycle = new GameplayCycle(mainHeroSpawner, levelConfig, _confirmPopup, this, _enemiesSpawners, _mainHeroSpawnPoint);

        yield return _gameplayCycle.Launch();
    }

    private void Update()
    {
        _controllersUpdateService?.Update();
        _gameplayCycle?.Update();
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }
}
