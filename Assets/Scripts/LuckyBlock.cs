using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBlock : MonoBehaviour
{
    [SerializeField] private GameObject _coin = null;

    private bool _onActive = false;
    private float _offsetY = 0.33f;
    private float _timer = 0.01f;
    private Coroutine _coroutine = null;

    public bool OnActive
    {
        get => _onActive;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
    }

    private IEnumerator MaybeJump()
    {
        _onActive = true;
        
        float currentOffset = _offsetY / 10;

        for (int i = 0; i < 10; i++)
        {
            transform.position += new Vector3(0, currentOffset);

            yield return new WaitForSecondsRealtime(_timer);
        }

        Instantiate(_coin, transform.position + new Vector3(0,_offsetY), Quaternion.identity);

        for (int i = 0; i < 10; i++)
        {
            transform.position -= new Vector3(0, currentOffset);

            yield return new WaitForSecondsRealtime(_timer);
        }

        StopCoroutine(_coroutine);

        _onActive = false;
    }

    public void StartRoutine()
    {
        _coroutine = StartCoroutine(MaybeJump());
    }
}
