using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{
    public int trunkSize;
    public int startTrunkSize;
    public int maxTrunkSize;
    public float RayResistance;

    private Vector2 ground;

    public Grid grid;
    private float cellSize;
    
    private float Timeline;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            DeathLaserHit();
        }

        if (Input.GetKey(KeyCode.E))
        {
            LifeLaserHit();
        }
    }

    private void Start()
    {
        cellSize = grid.cellSize.x * transform.localScale.x;
        
        Timeline = trunkSize;
        ground = new Vector2( transform.position.x, transform.position.y) - Vector2.up * trunkSize  * cellSize;
        UpdateTreeSize();
    }

    private void UpdateTreeSize()
    {
        transform.position = ground + Vector2.up* trunkSize  * cellSize;
        Debug.Log(trunkSize);
    }

    public void LifeLaserHit()
    {
        ChangeLifeTime(1);
    }

    public void DeathLaserHit()
    {
        ChangeLifeTime(-1);
    }

    private void ChangeLifeTime(int val)
    {
        Timeline += Time.deltaTime *RayResistance* -val;
        Timeline = Mathf.Clamp(Timeline, 0.99f, maxTrunkSize);

        if ((int)Mathf.Floor(Timeline) != trunkSize)
        {
            trunkSize = (int)Mathf.Floor(Timeline);
            UpdateTreeSize();
        }
    }
}
