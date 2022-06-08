using UnityEngine;
using UnityEngine.Events;

public class CharacterCollector : MonoBehaviour
{
    public event UnityAction CharactedCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterForCollect>(out CharacterForCollect character))
        {
            character.Remove();
            CharactedCollected.Invoke();
        }
    }
}
