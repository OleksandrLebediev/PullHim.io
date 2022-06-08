using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CharacterSpawner))]
public class Player : MonoBehaviour, IHost
{
    private StateMachine _stateMachine;
    private PlayerMovement _movement;
    private CharacterSpawner _spawner;

    private int _startAmountCharacters = 2;
    private List<Character> _characters = new List<Character>();

    private IEnumerable<Character> CharacterList => _characters;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _spawner = GetComponent<CharacterSpawner>();
    }

    public void Initialize(Joystick joystick)
    {
        AddCharacters(_startAmountCharacters);

        _stateMachine = new StateMachine();
        _stateMachine
            .AddState(new IdelStatePlayer(_stateMachine, CharacterList, joystick))
            .AddState(new MoveStatePlayer(_stateMachine, _movement, CharacterList, joystick))
            .AddState(new ShootStatePlayer(_stateMachine, CharacterList));

        _stateMachine.Initialize();

        joystick.TouchUp += OnTouchUp;
    }

    private void OnTouchUp()
    {
        _stateMachine.SwitchState<ShootStatePlayer>();
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdateLogic();
    }

    private void AddCharacters(int amount)
    {
        IEnumerable<Character> characters = _spawner.Spawn(amount);

        foreach (Character character in characters)
        {
            character.PulledOutEnemy += OnCharacterPulledOutEnemy;
            character.Died += RemoveCharacter;
            if (_stateMachine?.CurrentState is MoveStatePlayer)
                character.SetAnimation(CharacterAnimationInfo.Move, true);
        }

        _characters.AddRange(characters);
    }

    private void OnCharacterPulledOutEnemy()
    {
        AddCharacters(1);
    }

    private void RemoveCharacter(Character character)
    {
        character.PulledOutEnemy -= OnCharacterPulledOutEnemy;
        character.Died -= RemoveCharacter;
        _characters.Remove(character);
    }

}
