using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character characterTemplate;

    public List<Character> Spawn(int amount)
    {
        List<Character> _characters = new List<Character>();

        for (int i = 0; i < amount; i++)
        {
            Vector3 newPososition = GetPosition();
            Character character = Instantiate(characterTemplate, newPososition, 
                Quaternion.identity, transform);
            _characters.Add(character);
        }
        return _characters;
    }

    public Vector3 GetPosition()
    {
        Vector3 randomRadius = Random.insideUnitSphere * 0.1f;
        Vector3 newPosition = transform.position + randomRadius;
        newPosition.y = 0;
        return newPosition;
    }
}
