using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Aim")]
    public Transform center; //center of baby object
    public float longueurRayon;
    [SerializeField] private GameObject laser;
    private Vector3 aimDirection;
    private Vector3 tempPos;

    [Header("Laser")]
    public Color lifeColor;
    public Color deathColor;
    private GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Vector between mouse and center of the player, normalized
        Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - center.position;
        aimDir = aimDir.normalized;

        Vector2 transform2D = new Vector2(transform.position.x + (transform.localScale.x / 2), transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(center.position, aimDir, longueurRayon + center.localScale.x);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "enemy")
            {
                //Debug.Log("test");
                enemy = hit.collider.gameObject;
                Vector3 distToEnemy = hit.point - transform2D;

                laser.transform.localScale = new Vector3(distToEnemy.magnitude, 0.5f, 0);
                laser.transform.position = (transform2D + hit.point) / 2;
            }
        }
        else
        {
            enemy = null;
            Vector3 tempDir = aimDir;
            tempDir.z = 0;
            tempPos = tempDir.normalized * longueurRayon + center.position;
            laser.transform.position = tempDir.normalized * (longueurRayon + center.localScale.x / 2) + center.position;
            laser.transform.localScale = new Vector3((tempPos - center.position).magnitude * 2, 0.5f, 0);
        }


        //Aim at
        AimInput(aimDir);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        center.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            laser.SetActive(true);
            laser.GetComponent<SpriteRenderer>().color = Input.GetMouseButton(0) ? lifeColor : deathColor;
            Color laserColor = laser.GetComponent<SpriteRenderer>().color;

            //Young
            if (laserColor == lifeColor && enemy != null)
                enemy.GetComponent<Enemy>().GetYoung();
            //Old
            else if(laserColor == deathColor && enemy != null)
                enemy.GetComponent<Enemy>().GetOld();    
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
