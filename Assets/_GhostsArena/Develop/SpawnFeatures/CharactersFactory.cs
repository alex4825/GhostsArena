using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class CharactersFactory
{
    private CinemachineVirtualCamera _mainHeroFollowCamera;

    public CharacterControllerCharacter CreateMainHeroCharacter(MainHeroConfig config, Vector3 spawnPosition)
    {
        CharacterControllerCharacter character = Object.Instantiate(config.Prefab as CharacterControllerCharacter, spawnPosition, Quaternion.identity);

        CharacterControllerMover mover = new(character.GetComponent<CharacterController>()); ;
        CharacterControllerJumper jumper = new(character.GetComponent<CharacterController>(), character, config.JumpHeight, character);
        Shooter shooter = new(character, config.BulletPrefab, config.DistantAttack, config.BulletLifetime, config.ShootForce);

        character.Initialize(config, mover, jumper, shooter);

        InitializeCamera(character);

        return character;
    }

    public AgentEnemyCharacter CreateEnemyCharacter(AgentEnemyConfig config, Vector3 spawnPosition)
    {
        AgentEnemyCharacter character = Object.Instantiate(config.Prefab as AgentEnemyCharacter, spawnPosition, Quaternion.identity);

        AgentMover mover = new AgentMover(character.GetComponent<NavMeshAgent>(), config.WalkingSpeed);
        AgentJumper jumper = new AgentJumper(config.JumpSpeed, character.GetComponent<NavMeshAgent>(), character, config.JumpCurve);

        character.Initialize(config, mover, jumper);

        return character;
    }

    private void InitializeCamera(CharacterControllerCharacter character)
    {
        CinemachineVirtualCamera mainHeroFollowCameraPrefab = Resources.Load<CinemachineVirtualCamera>("Prefabs/FollowCamera");
        _mainHeroFollowCamera = Object.Instantiate(mainHeroFollowCameraPrefab);
        _mainHeroFollowCamera.Follow = character.transform;
        _mainHeroFollowCamera.LookAt = character.transform;

        character.Dead += OnHeroCharacterDead;
    }
    private void OnHeroCharacterDead(IKillable character, float arg2)
    {
        _mainHeroFollowCamera.Follow = null;
        _mainHeroFollowCamera.LookAt = null;

        character.Dead -= OnHeroCharacterDead;
    }
}
