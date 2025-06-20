using System.Collections.Generic;

public class ControllersUpdateService
{
    private Dictionary<Character, Controller> _charactersToControllers = new();

    public void Add(Character character, Controller controller)
    {
        _charactersToControllers.Add(character, controller);

        character.Dead += OnCharacterDead;
    }

    public void Update()
    {
        foreach (var characterToController in _charactersToControllers)
        {
            characterToController.Value.IsEnabled = characterToController.Key.IsAlive;
            characterToController.Value?.Update();
        }
    }

    private void OnCharacterDead(IKillable character, float deadDuration)
    {
        _charactersToControllers[character as Character].IsEnabled = false;
        _charactersToControllers.Remove(character as Character);

        character.Dead -= OnCharacterDead;
    }
}
