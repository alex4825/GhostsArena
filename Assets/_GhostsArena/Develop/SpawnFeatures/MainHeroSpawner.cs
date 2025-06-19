using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class MainHeroSpawner : MonoBehaviour
{
    [SerializeField] private MainHeroConfig _mainHeroConfig;
    [SerializeField] private Transform _heroSpawnPoint;

    private CharacterControllerCharacter _heroCharacter;
    private Controller _heroController;
    private CinemachineVirtualCamera _mainHeroFollowCamera;

    private void Update()
    {
        if (_heroController != null)
        {
            _heroController.IsEnabled = _heroCharacter.IsAlive;
            _heroController?.Update();
        }
    }

    public IEnumerator Spawn(Action<CharacterControllerCharacter> callbackOnSpawned)
    {
        _heroCharacter = Instantiate(_mainHeroConfig.Prefab as CharacterControllerCharacter, transform.position, Quaternion.identity);
        _heroCharacter.Initialize(_mainHeroConfig);
        InitializeCamera();

        yield return new WaitForSeconds(_heroCharacter.ShowDuration);

        _heroController = new CompositeController(
            new DirectionalCharacterWASDController(_heroCharacter),
            new ShooterController(_heroCharacter));

        _heroController.IsEnabled = true;

        callbackOnSpawned?.Invoke(_heroCharacter);
    }

    private void InitializeCamera()
    {
        _mainHeroFollowCamera = Instantiate(_mainHeroConfig.FollowCameraPrefab);
        _mainHeroFollowCamera.Follow = _heroCharacter.transform;
        _mainHeroFollowCamera.LookAt = _heroCharacter.transform;
    }
}
