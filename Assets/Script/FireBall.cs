using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    [HideInInspector] public int damage;
    [HideInInspector] public Vector3 direction;

    private void Start()
    {
        direction.Normalize();

        transform.localScale = new Vector3(0.25f * damage, 0.25f * damage, 1);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().getHit(damage);
            Destroy(gameObject);
        }
    }
}
