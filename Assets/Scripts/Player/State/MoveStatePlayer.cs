using System.Collections.Generic;
using UnityEngine;

public class MoveStatePlayer : BaseState
{
    public MoveStatePlayer(IStationStateSwitcher stateSwitcher, PlayerMovement movement,
        IEnumerable<Character> characterList, Joystick joystick) : base(stateSwitcher)
    {
        _movement = movement;
        _joystick = joystick;
        _characterList = characterList;
    }

    private readonly PlayerMovement _movement;
    private readonly IEnumerable<Character> _characterList;
    private Joystick _joystick;

    public override void Enter()
    {
        foreach (var character in _characterList)
        {
            character.SetAnimation(CharacterAnimationInfo.Move, true);
        }
    }

    public override void Exit()
    {
        foreach (var character in _characterList)
        {
            character.SetAnimation(CharacterAnimationInfo.Move, false);
        }
    }

    public override void UpdateLogic()
    {
        Vector3 direction = _joystick.Direction;
        if (direction.magnitude == 0)
            _stateSwitcher.SwitchState<IdelStatePlayer>();


        _movement.Move(direction);

        foreach (var character in _characterList)
        {
            character.Rotate(direction, _movement.SpeedRotation);
        }
    }
}
