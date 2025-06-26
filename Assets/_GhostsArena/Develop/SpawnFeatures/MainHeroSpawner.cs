using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class MainHeroSpawner
{
    private CharacterControllerCharacter _heroCharacter;
    private CinemachineVirtualCamera _mainHeroFollowCamera;

    private MainHeroConfig _mainHeroConfig;
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public MainHeroSpawner(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        MainHeroConfig mainHeroConfig)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _mainHeroConfig = mainHeroConfig;
    }

    public IEnumerator Spawn(Action<CharacterControllerCharacter> callbackOnSpawned, Vector3 spawnPosition)
    {
        _heroCharacter = _charactersFactory.CreateMainHeroCharacter(_mainHeroConfig, spawnPosition);

        if (_mainHeroFollowCamera == null)
            InitializeCamera();
        else
            ChooseCameraTarget(_heroCharacter.transform);

        yield return new WaitForSeconds(_heroCharacter.ShowDuration);

        Controller heroController = _controllersFactory.CreateMainHeroController(_heroCharacter);

        _controllersUpdateService.Add(_heroCharacter, heroController);

        callbackOnSpawned?.Invoke(_heroCharacter);
    }

    private void InitializeCamera()
    {
        CinemachineVirtualCamera mainHeroFollowCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/FollowCamera");
        _mainHeroFollowCamera = Object.Instantiate(mainHeroFollowCameraPrefab);
        ChooseCameraTarget(_heroCharacter.transform);

        _heroCharacter.Dead += OnHeroCharacterDead;
    }

    private void OnHeroCharacterDead(IKillable character)
    {
        ChooseCameraTarget(null);

        character.Dead -= OnHeroCharacterDead;
    }

    private void ChooseCameraTarget(Transform target)
    {
        _mainHeroFollowCamera.Follow = target;
        _mainHeroFollowCamera.LookAt = target;
    }
}
