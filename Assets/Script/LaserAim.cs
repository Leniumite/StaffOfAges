using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [SerializeField] private GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(point);
        
        // Young
        if(Input.GetMouseButtonDown(0))
        {
            laser.SetActive(true);
        }

        // Old
        if (Input.GetMouseButtonDown(1))
        {
            laser.SetActive(true);
        }
    }
}
