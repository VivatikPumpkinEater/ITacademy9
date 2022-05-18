using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = -9.8f;

    [SerializeField] private HealthManager _health = null;
    
    [SerializeField] private AnimationCurve _curve;

    private bool _alive = true;
    
    private MaybeWeapon _maybeWeapon = null;

    private MaybeWeapon _downDetected
    {
        get => _maybeWeapon = _maybeWeapon ?? GetComponentInChildren<MaybeWeapon>();
    }
    
    private Vector2 _moveVelocity;
    
    private Animator _animator = null;
    private Animator _anim
    {
        get => _animator = _animator ?? GetComponent<Animator>();
    }

    private Rigidbody2D _rigidbody2D = null;

    private Rigidbody2D _rb
    {
        get => _rigidbody2D = _rigidbody2D ?? GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _health.Die += Die;
    }

    private void Update()
    {
        if (_alive)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _downDetected.OnJump)
        {
            Debug.Log("Space");
            _rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

        if (horizontal > 0 && transform.rotation.y != 0)
        {
            Flip(true);
        }
        if (horizontal < 0 && transform.rotation.y != 180)
        {
            Flip(false);
        }

        _anim.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector2 newPosition = (Vector2)transform.position + ((Vector2.right * horizontal) * _speed * Time.deltaTime);

        transform.position = newPosition;
        //_rb.MovePosition(newPosition);
    }

    private void JumpAnimation()
    {
        while (!_downDetected.OnJump)
        {
            _anim.SetTrigger("Jump");
            
            if (_downDetected.OnJump)
            {
                _anim.SetTrigger("JumpEnd");
            }
        }
    }
    
    private void FixedUpdate()
    {
        //_rb.MovePosition(_rb.position + _moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip(bool flip)
    {
        transform.rotation = flip ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0,180,0);
    }

    private void Die()
    {
        _alive = false;
        
        _anim.SetTrigger("Die");
    }
}
