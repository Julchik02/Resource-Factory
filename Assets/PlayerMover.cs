using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    MobileController _joystick;
    [SerializeField] float _moveSpeed = 1f;
    private Vector3 _moveVector;
    private void Start()
    {
        _joystick = GameObject.FindWithTag("Joystic").GetComponent<MobileController>();
    }

    private void FixedUpdate()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal() * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical() * _moveSpeed * Time.deltaTime;
        transform.Translate(_moveVector);
    }
}
