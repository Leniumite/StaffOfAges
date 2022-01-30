using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{

    public int playerHealth;
    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    public GameObject player;

    public void LevelEnd()
    {
        Debug.Log("level ended");
    }

}