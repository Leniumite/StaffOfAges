using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    public void LevelEnd()
    {
        Debug.Log("level ended");
    }
    
}
