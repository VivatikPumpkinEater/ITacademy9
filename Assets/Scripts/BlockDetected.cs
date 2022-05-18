using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetected : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        LuckyBlock luckyBlock = col.GetComponent<LuckyBlock>();
        
        if (luckyBlock && !luckyBlock.OnActive)
        {
            col.GetComponent<LuckyBlock>().StartRoutine();

            Debug.Log("Pew");
        }
    }
}
