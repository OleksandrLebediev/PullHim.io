using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Player _player;

    private void Start()
    {
       _player.Initialize(_joystick);
    }
}
