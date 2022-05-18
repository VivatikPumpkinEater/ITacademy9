using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralMap : MonoBehaviour
{
    [SerializeField] private int _height = 0, _width = 0, _minHeight = 0, _maxHeight = 0;
    [SerializeField] private int _repeat;

    [SerializeField] private GameObject _ground = null, _luckyBlock = null, _brickBlock = null;
    [SerializeField] private Enemy _enemy = null;
    
    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        int repeatValue = 0;
        
        for (int x = 0; x < _width; x++)
        {
            for (int i = 0; i < _repeat; i++)
            {
                GeneratePlatform(x);
            }
        }
    }

    private void GeneratePlatform(int x)
    {
        

        for (int y = 0; y < _height; y++)
        {
            Spawn(_ground, x, y);
        }
    }

    private void Spawn(GameObject spawn, int width, int height)
    {
        spawn = Instantiate(spawn, new Vector2(width, height), Quaternion.identity);
        spawn.transform.SetParent(this.transform);
    }
}
