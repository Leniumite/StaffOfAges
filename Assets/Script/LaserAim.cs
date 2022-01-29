using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Aim")]
    public Transform Body; 
    public float longueurRayon;
    [SerializeField] private GameObject laser;
    private Vector3 aimDirection;
    private Vector3 tempPos;
    private CircleCollider2D col;

    [Header("Laser")]
    public Color lifeColor;
    public Color deathColor;
    private GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector between mouse and body of the player, normalized
        Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Body.position;
        Vector3 tempDir = aimDir;
        tempDir.z = 0;

        Vector2 transform2D = new Vector2(Body.position.x + (col.radius), Body.position.y);

        RaycastHit2D hit = Physics2D.Raycast(Body.position, aimDir, longueurRayon + col.radius, ~LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            Debug.Log("test");
            enemy = hit.collider.gameObject;
            Vector3 distToEnemy = hit.point - transform2D;

            laser.transform.localScale = new Vector3(distToEnemy.magnitude, 0.5f, 0);
            laser.transform.position = Body.position + (laser.transform.localScale.x /2 + col.radius) * tempDir.normalized;
        }
        else
        {
            enemy = null;

            tempPos = tempDir.normalized * longueurRayon + Body.position;
            laser.transform.position = tempDir.normalized * ((longueurRayon/2) + col.radius) + Body.position;
            laser.transform.localScale = new Vector3((tempPos - Body.position).magnitude, 0.5f, 0);
        }


        //Aim at
        AimInput(aimDir);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
