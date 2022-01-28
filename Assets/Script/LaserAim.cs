using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Aim")]
    public Transform center; //center of baby object
    [SerializeField] private GameObject laser;
    private Vector3 aimDirection;
    private Vector3 startPosLaser;


    // Start is called before the first frame update
    void Start()
    {
        startPosLaser = laser.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector between mouse and center of the player, normalized
        Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - center.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDir);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "enemy")
            {
                Debug.Log("test");
                GameObject enemy = hit.collider.gameObject;
                Vector2 transform2D = new Vector2(transform.position.x + (transform.localScale.x /2), transform.position.y);
                Vector3 distToEnemy = hit.point - transform2D;
                
                laser.transform.localScale = distToEnemy;
                laser.transform.position = (transform2D + hit.point) / 2;
            }
        }
        else
        {
            laser.transform.localScale = new Vector3(5, 0.5f, 0);
            laser.transform.position = startPosLaser;
        }


        //Aim at
        AimInput(aimDir);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Young
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            laser.SetActive(true);
            //TODO method
        }
        else
            laser.SetActive(false);
    }

    public void AimInput(Vector3 aimDir)
    {
        aimDirection = aimDir;
        aimDirection.Normalize();
    }
}
