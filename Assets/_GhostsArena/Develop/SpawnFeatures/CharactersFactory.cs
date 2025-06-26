using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class CharactersFactory
{ 
    public CharacterControllerCharacter CreateMainHeroCharacter(MainHeroConfig config, Vector3 spawnPosition)
    {
        CharacterControllerCharacter character = Object.Instantiate(config.Prefab as CharacterControllerCharacter, spawnPosition, Quaternion.identity);

        CharacterControllerMover mover = new(character.GetComponent<CharacterController>()); ;
        CharacterControllerJumper jumper = new(character.GetComponent<CharacterController>(), character, config.JumpHeight, character);
        Shooter shooter = new(character, config.BulletPrefab, config.DistantAttack, config.BulletLifetime, config.ShootForce);

        character.Initialize(config, mover, jumper, shooter);

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
}
