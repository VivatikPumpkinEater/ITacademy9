using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaybeWeapon : MonoBehaviour
{
    private int _damage = 1;

    private Rigidbody2D _rigidbody2D = null;

    private Rigidbody2D _rbPlayer
    {
        get => _rigidbody2D = _rigidbody2D ?? GetComponentInParent<Rigidbody2D>();
    }

    [SerializeField] private bool _onJump = false;

    public bool OnJump
    {
        get => _onJump;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        _onJump = true;

        Enemy enemy = col.GetComponent<Enemy>();
        
        //if (col.GetComponent<Enemy>() && col.GetComponent<Enemy>().Alive)
        if(enemy && enemy.Alive)
        {
            _rbPlayer.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            
            col.GetComponent<HealthManager>().ChangeHp(_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _onJump = false;
    }
}
