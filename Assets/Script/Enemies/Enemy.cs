using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    public int dmg;
    public int lifeStage;
    public Sprite sprite;
    public bool isShieldWhite;
    public bool isShieldViolet;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetYoung()
    {
        if (isShieldWhite)
            return;

        Debug.Log("young");
    }

    public void GetOld()
    {
        if (isShieldViolet)
            return;

        Debug.Log("Old");
    }
}
