using Cinemachine;
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
        _heroController.IsEnabled = _heroCharacter.IsAlive;
        _heroController?.Update();
    }

    public CharacterControllerCharacter Spawn()
    {
        _heroCharacter = Instantiate(_mainHeroConfig.Prefab as CharacterControllerCharacter, transform.position, Quaternion.identity);
        _heroCharacter.Initialize(_mainHeroConfig);

        _heroController = new CompositeController(
            new DirectionalCharacterWASDController(_heroCharacter),
            new ShooterController(_heroCharacter));

        _heroController.IsEnabled = true;

        InitializeCamera();

        return _heroCharacter;
    }

    private void InitializeCamera()
    {
        _mainHeroFollowCamera = Instantiate(_mainHeroConfig.FollowCameraPrefab);
        _mainHeroFollowCamera.Follow = _heroCharacter.transform;
        _mainHeroFollowCamera.LookAt = _heroCharacter.transform;
    }
}
