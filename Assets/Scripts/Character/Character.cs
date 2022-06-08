using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, ITarget, IShooter
{
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private Harpoon _harpoon;
    private IHost _host;

    private bool _isHooked;
    private bool _isShooting;

    public IHost Host => _host;
    public bool IsHooked => _isHooked;
    public bool IsShooting => _isShooting;

    public event UnityAction PulledOutEnemy;
    public event UnityAction<Character> Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _host = GetComponentInParent<IHost>();
        _harpoon = GetComponentInChildren<Harpoon>();
    }

    public void Rotate(Vector3 direction, float speedRotation)
    {
        Vector3 direct = Vector3.RotateTowards(transform.forward, direction, speedRotation, 0.0f);
        transform.rotation = Quaternion.LookRotation(direct);
    }

    public void SetAnimation(string name, bool value)
    {
        _animator.SetBool(name, value);
    }

    public void Shoot()
    {
        if (_isShooting == true || _isHooked == true) return;

        _isShooting = true;
        _harpoon.Shoot();
    }

    public void StopShoot()
    {
        _isShooting = false;
    }    

    public void OnHooked()
    {
        _animator.enabled = false;
        _capsuleCollider.isTrigger = true;
        _isHooked = true;
    }

    public void OnPulledOutEnemy()
    {
        PulledOutEnemy?.Invoke();
    }    

    public void SetPosition(Vector3 position)
    {
        position.y = 0;
        transform.position = position;
    }

    public void Remove()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
