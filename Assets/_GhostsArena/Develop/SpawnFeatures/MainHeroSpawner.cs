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

        yield return new WaitForSeconds(_heroCharacter.ShowDuration);

        Controller heroController = _controllersFactory.CreateMainHeroController(_heroCharacter);

        _controllersUpdateService.Add(_heroCharacter, heroController);

        callbackOnSpawned?.Invoke(_heroCharacter);
    }

}
