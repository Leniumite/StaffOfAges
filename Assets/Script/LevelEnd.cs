using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEnd : MonoBehaviour
{
    public UnityEvent OnGameEnd;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnGameEnd.Invoke();
        }
    }
}
