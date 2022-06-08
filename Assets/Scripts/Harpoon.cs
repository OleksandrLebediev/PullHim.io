using System.Collections;
using UnityEngine;

public class Harpoon : MonoBehaviour, IHookHitHandler
{
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private float _maxHookDistance;

    private LineRenderer line;
    private Character _character;
    private ITarget _targetOnHook;
    private IHost _host;

    private Hook _currentHook;
    private float _distance;
    private float _speed = 5;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        _host = GetComponentInParent<IHost>();
        _character = GetComponentInParent<Character>();
    }

    public void Shoot()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        _currentHook = Instantiate(_hook, _shootPosition.position, _shootPosition.rotation);
        _currentHook.Initialize(this, _host);

        while (_currentHook != null)
        {
            line.SetPosition(0, _currentHook.transform.position);
            line.SetPosition(1, _shootPosition.position);

            _currentHook.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _distance = Vector3.Distance(_currentHook.transform.position, _shootPosition.position);

            if (_distance >= _maxHookDistance)
            {
                yield return ReturnHookCoroutine();
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator ReturnHookCoroutine()
    {
        while (_currentHook != null)
        {
            line.SetPosition(0, _currentHook.transform.position);
            line.SetPosition(1, _shootPosition.position);

            _currentHook.transform.position = Vector3.MoveTowards(_currentHook.transform.position,
                _shootPosition.position, Time.deltaTime * _speed);
            _targetOnHook?.SetPosition(_currentHook.transform.position);
            _distance = Vector3.Distance(_currentHook.transform.position, _shootPosition.position);

            if (_distance <= 0.01f)
            {
                if(_targetOnHook != null)
                {
                    _character.OnPulledOutEnemy();
                    _targetOnHook.Remove();
                    _targetOnHook = null;
                }
                _character.StopShoot();
                Destroy(_currentHook.gameObject);
                yield break;
            }
            yield return null;
        }
    }

    public void OnHooked(ITarget target)
    {
        StopAllCoroutines();
        _targetOnHook = target;
        _targetOnHook.OnHooked();
        StartCoroutine(ReturnHookCoroutine());
    }
}
