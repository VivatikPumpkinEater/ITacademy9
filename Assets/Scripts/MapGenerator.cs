using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _ground = null;

    [SerializeField] private float _offsetCell = 0.33f;

    [SerializeField] private Vector2 _size;

    [SerializeField] private Transform _startGenerate = null;

    private void Start()
    {
        Vector2 currentPosition = _startGenerate.position;
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                var ground = Instantiate(_ground, currentPosition, Quaternion.identity);
                ground.transform.SetParent(this.transform);

                if (y == _size.y - 1)
                {
                    ground.gameObject.AddComponent<BoxCollider2D>();
                }
                
                currentPosition += new Vector2(0, _offsetCell);
            }

            currentPosition = new Vector2(currentPosition.x + _offsetCell, _startGenerate.position.y);
        }
    }
}
