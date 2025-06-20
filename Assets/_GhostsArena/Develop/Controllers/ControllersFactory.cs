public class ControllersFactory
{
    public AgentEnemyAgroController CreateAgentEnemyAgroController(AgentEnemyCharacter character, IKillable target)
    {
        return new AgentEnemyAgroController(character, target);
    }

    public AgentRandomPatrolController CreateAgentRandomPatrolController(AgentCharacter character, float patrolRadius)
    {
        return new AgentRandomPatrolController(character, patrolRadius);
    }

    public DirectionalCharacterWASDController CreateDirectionalCharacterWASDController(CharacterControllerCharacter character)
    {
        return new DirectionalCharacterWASDController(character);
    }

    public ShooterController CreateShooterController(IShootable shootable)
    {
        return new ShooterController(shootable);
    }

    public SwitcherController CreateEnemyController(AgentEnemyCharacter enemy, IKillable target, float patrolRadius)
    {
        return new SwitcherController(
            CreateAgentEnemyAgroController(enemy, target),
            CreateAgentRandomPatrolController(enemy, patrolRadius));
    }

    public CompositeController CreateMainHeroController(CharacterControllerCharacter heroCharacter)
    {
        return new CompositeController(
            CreateDirectionalCharacterWASDController(heroCharacter),
            CreateShooterController(heroCharacter));   
    }
}
