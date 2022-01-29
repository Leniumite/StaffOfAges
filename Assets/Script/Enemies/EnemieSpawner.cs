using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemieSpawner : MonoBehaviour
{
    public List<Enemy> possibleEnemies = new List<Enemy>();
    public Vector2 RandomDelayBetween  = new Vector2(5,10);

    public int maxToSpawn=1;

    public float maxDistanceActive;
    
    private float delay;

    private void Start()
    {
        delay = Random.Range(RandomDelayBetween.x, RandomDelayBetween.y);
        if (possibleEnemies.Count == 0)
        {
            Debug.Log("no enemie in list "+name);
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(maxToSpawn<=0) gameObject.SetActive(false);
        
        delay -= Time.deltaTime;
        if (delay <= 0 && CheckDistancePlayer())
        {   
            Spawn();
            delay = Random.Range(RandomDelayBetween.x, RandomDelayBetween.y);
            maxToSpawn--;
        }
    }

    private void Spawn()
    {
        Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count - 1)], transform.position, Quaternion.identity);
    }

    private bool CheckDistancePlayer()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen) return false;

        bool tooFar = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) > maxDistanceActive;

        return !tooFar;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxDistanceActive * 2, maxDistanceActive * 2, 1));
    }
}
