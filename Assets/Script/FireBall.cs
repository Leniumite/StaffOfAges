using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FireBall : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage = 1;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        direction.Normalize();
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            col.gameObject.GetComponent<Player>().getHit(damage);
        }
    }
}
