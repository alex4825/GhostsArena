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
    private CharactersFactory _charactersFactory;

    public void Initialize(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public IEnumerator Spawn(Action<CharacterControllerCharacter> callbackOnSpawned)
    {
        _heroCharacter = _charactersFactory.CreateMainHeroCharacter(_mainHeroConfig, transform.position);
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
