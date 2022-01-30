using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawnerAlways : MonoBehaviour
{
    public List<Enemy> possibleEnemies = new List<Enemy>();
    public Vector2 RandomDelayBetween  = new Vector2(5,10);

    public int maxToSpawn=1;

    private float delay;

    private List<Enemy> activeEnemies = new List<Enemy>();

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
        if (delay <= 0 )
        {   
            Spawn();
            delay = Random.Range(RandomDelayBetween.x, RandomDelayBetween.y);
        }
    }

    private void CheckEnemieActive()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if(!activeEnemies[i]) activeEnemies.RemoveAt(i);
        }
    }
    
    private void Spawn()
    {
        CheckEnemieActive();
        if(activeEnemies.Count >= maxToSpawn) return;
        activeEnemies.Add(Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count - 1)], transform.position, Quaternion.identity));
    }

}
