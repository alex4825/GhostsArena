using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private MainHeroSpawner _mainHeroSpawner;
    [SerializeField] private List<EnemiesSpawner> _enemiesSpawners;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private ConfirmPopup _confirmPopup;

    private ControllersUpdateService _controllersUpdateService;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        _loadingScreen.Show();

        _controllersUpdateService = new ControllersUpdateService();

        _mainHeroSpawner.Initialize(_controllersUpdateService);

        foreach (var spawner in _enemiesSpawners)
            spawner.Initialize(_controllersUpdateService);

        yield return new WaitForSeconds(2f);

        _loadingScreen.Hide();

        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        CharacterControllerCharacter mainHeroCharacter = new();

        yield return StartCoroutine(_mainHeroSpawner.Spawn(hero => mainHeroCharacter = hero));

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        _confirmPopup.Hide();

        foreach (var spawner in _enemiesSpawners)
            spawner.Activate(mainHeroCharacter);
    }

    private void Update()
    {
        _controllersUpdateService?.Update();
    }
}
