using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterSpawner))]
public class Enemy : MonoBehaviour, IHost
{
    private List<Character> _characters = new List<Character>();
    private CharacterSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<CharacterSpawner>();
    }

    private void Start()
    {
        IEnumerable<Character> character = _spawner.Spawn(3);
        _characters.AddRange(character);
    }
}
