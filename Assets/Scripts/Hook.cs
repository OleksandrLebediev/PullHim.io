using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private IHookHitHandler _hookHitHandler;
    private IHost _host;

    public void Initialize(IHookHitHandler hookHandler, IHost host)
    {
        _hookHitHandler = hookHandler;
        _host = host;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ITarget>(out ITarget target))
        {
            if (_host == target.Host || target.IsHooked == true) return;
            _hookHitHandler.OnHooked(target);
        }
    }

}
