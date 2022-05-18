using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _health = 1;

    public System.Action Die;

    private bool _alive = true;

    public void ChangeHp(int damage)
    {
        _health -= damage;

        if (_health >= 0 && _alive)
        {
            _alive = false;
            
            Die?.Invoke();
        }
    }
}
