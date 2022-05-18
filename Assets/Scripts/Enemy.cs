using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    public System.Action<Enemy> ImDied;
    
    private bool _die = false;

    public bool Alive
    {
        get => !_die;
    }
    private HealthManager _healthManager = null;

    private HealthManager _hp
    {
        get => _healthManager = _healthManager ?? GetComponent<HealthManager>();
    }

    private Vector2 _direction = Vector2.left;
    private Vector2 _offsetCollider;

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
        _hp.Die += Die;
    }

    private void FixedUpdate()
    {
        if (!_die)
        {
            Detected(_direction);
            
            Vector2 newPosition = (Vector2)transform.position + _direction * _speed * Time.deltaTime;
            transform.position = newPosition;
            
            //_rb.MovePosition(_rb.position + _direction * Time.fixedDeltaTime);
        }
    }

    private void Detected(Vector2 dir)
    {
        _offsetCollider = new Vector2(0.17f, 0) * dir;

        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position + _offsetCollider, dir, 0.05f);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Player>())
            {
                hit.collider.GetComponent<HealthManager>().ChangeHp(1);
            }
            
            if (_direction == Vector2.left)
            {
                _direction = Vector2.right;
            }

            else if (_direction == Vector2.right)
            {
                _direction = Vector2.left;
            }
        }
    }

    private void Die()
    {
        _die = true;

        gameObject.GetComponent<CircleCollider2D>().radius /= 2;
        
        _anim.SetTrigger("Die");
    }

    public void OnEnd()
    {
        Destroy(gameObject, 5f);
        
        ImDied?.Invoke(this);
    }
}