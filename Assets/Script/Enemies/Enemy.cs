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
    public float resToRay; //Basically seconds to get effect from the ray
    private float tempRes;

    [Header("Attack")]
    public bool CaC;
    public float cooldown;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        tempRes = resToRay;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeStage > 4 || lifeStage < 1)
        {
            Destroy(gameObject);
        }
    }

    public void GetYoung()
    {
        if (isShieldWhite)
            return;

        tempRes -= Time.deltaTime;
        if(tempRes <= 0)
        {
            lifeStage--;
            tempRes = resToRay;
        }

        Debug.Log("young");
    }

    public void GetOld()
    {
        if (isShieldViolet)
            return;

        tempRes -= Time.deltaTime;
        if (tempRes <= 0)
        {
            lifeStage++;
            tempRes = resToRay;
        }

        Debug.Log("Old");
    }
}
