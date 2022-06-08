using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotation;

    public float SpeedMove => _speedMove;
    public float SpeedRotation => _speedRotation;

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * _speedMove * Time.deltaTime);
    }
}
