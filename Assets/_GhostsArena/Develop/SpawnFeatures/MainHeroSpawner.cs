using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class MainHeroSpawner : MonoBehaviour
{
    [SerializeField] private MainHeroConfig _mainHeroConfig;
    [SerializeField] private Transform _heroSpawnPoint;

    private CharacterControllerCharacter _heroCharacter;
    private CinemachineVirtualCamera _mainHeroFollowCamera;

    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;

    public void Initialize(ControllersUpdateService controllersUpdateService, ControllersFactory controllersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
    }

    public IEnumerator Spawn(Action<CharacterControllerCharacter> callbackOnSpawned)
    {
        _heroCharacter = Instantiate(_mainHeroConfig.Prefab as CharacterControllerCharacter, transform.position, Quaternion.identity);
        _heroCharacter.Initialize(_mainHeroConfig);
        InitializeCamera();

        yield return new WaitForSeconds(_heroCharacter.ShowDuration);

        Controller heroController = _controllersFactory.CreateMainHeroController(_heroCharacter);

        _controllersUpdateService.Add(_heroCharacter, heroController);

        callbackOnSpawned?.Invoke(_heroCharacter);
    }

    private void InitializeCamera()
    {
        _mainHeroFollowCamera = Instantiate(_mainHeroConfig.FollowCameraPrefab);
        _mainHeroFollowCamera.Follow = _heroCharacter.transform;
        _mainHeroFollowCamera.LookAt = _heroCharacter.transform;

        _heroCharacter.Dead += OnHeroCharacterDead;
    }

    private void OnHeroCharacterDead(IKillable arg1, float arg2)
    {
        _mainHeroFollowCamera.Follow = null;
        _mainHeroFollowCamera.LookAt = null;
    }
}
