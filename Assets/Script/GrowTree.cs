using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowTree : MonoBehaviour
{
    public Transform treeBase;
    public Transform treeTop;

    private static float TreeTopHeightExtend = 0.25f;

    public int width;
    public int height;
    public int maxHeight;

    [SerializeField] private float LifeTime;

    public void OnValidate()
    {
        UpdateTreeSize();
    }

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
        LifeTime = height;
    }

    private void UpdateTreeSize()
    {
        height = Mathf.Clamp(height, 0, maxHeight);
        
        treeBase.transform.localPosition = Vector3.up * height / 2;
        treeBase.transform.localScale = new Vector3(1, height, 1);

        treeTop.transform.localPosition = Vector3.up * (TreeTopHeightExtend + height);
        treeTop.localScale = new Vector3(width, 1, 1);
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
        LifeTime += Time.deltaTime * val;
        LifeTime = Mathf.Clamp(LifeTime, 0.99f, maxHeight);

        if ((int)Mathf.Floor(LifeTime) != height)
        {
            height = (int)Mathf.Floor(LifeTime);
            UpdateTreeSize();
        }
    }
}
