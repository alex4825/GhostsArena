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
    private GameMode _gameMode;

    private void Awake()
    {
        StartCoroutine(StartProcess());
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

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        CharacterControllerCharacter mainHeroCharacter = new();

        yield return StartCoroutine(mainHeroSpawner.Spawn(hero => mainHeroCharacter = hero, _mainHeroSpawnPoint.position));

        _gameMode = new(levelConfig, mainHeroCharacter, _enemiesSpawners);

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        _gameMode.Start();

    }

    private void Update()
    {
        _controllersUpdateService?.Update();
        _gameMode?.Update();
    }
}
