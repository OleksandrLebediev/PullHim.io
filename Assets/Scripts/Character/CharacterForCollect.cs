using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterForCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ICollector>(out ICollector collector))
        {
            
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
