using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public int playerHealth;

    AudioSource source;

    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    private void Start()
    {
        TryGetComponent(out source);
        source.Play();
    }

    public GameObject player;

    public void LevelEnd()
    {
        Debug.Log("level ended");
    }

}