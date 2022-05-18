using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Enemy _enemy = null;
    [SerializeField] private int _maxCount = 2;
    [SerializeField] private float _rangeTimeSpawn = 0f;

    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(GenerateEnemy());
    }

    private IEnumerator GenerateEnemy()
    {
        while (_enemies.Count <= _maxCount)
        {
            yield return new WaitForSecondsRealtime(_rangeTimeSpawn);
            
            Spawn(_enemy);
        }
    }

    private void Spawn(Enemy enemy)
    {
        enemy = Instantiate(enemy, transform.position, quaternion.identity);
        enemy.transform.SetParent(this.transform);
        
        _enemies.Add(enemy);

        enemy.ImDied += RemoveAtList;
        
        Vector2 dir = Vector2.left + Vector2.up;
        
        enemy.GetComponent<Rigidbody2D>().AddForce(dir * 5, ForceMode2D.Impulse);
    }

    private void RemoveAtList(Enemy dieEnemy)
    {
        _enemies.Remove(dieEnemy);
        
        StartCoroutine(GenerateEnemy());
    }
}
