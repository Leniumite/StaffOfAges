using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
    [Header("Aim")]
    public Transform Body; 
    public float longueurRayon;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject laserShooter;
    private Vector3 aimDirection;
    private Vector3 tempPos;
    private CircleCollider2D col;

    [Header("Laser")]
    public Color lifeColor;
    public Color deathColor;
    private GameObject objectTouched;
    private int coeffRotation = 1;
    public Animator animator;

    [Header("particle")]
    public ParticleSystem laserParticle;

    private int LifeClick = 0;
    private int DeathClick = 1;
    
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

        Vector2 transform2D = new Vector2(Body.position.x, Body.position.y);

        RaycastHit2D hit = Physics2D.Raycast(Body.position, aimDir, longueurRayon + col.radius, ~LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            
            Debug.DrawLine(Body.position, aimDir, Color.red);
            objectTouched = hit.collider.gameObject;
            Vector3 distToEnemy = hit.point - transform2D;

            laser.transform.localScale = new Vector3(distToEnemy.magnitude - col.radius, 0.5f, 0);
            laser.transform.position = Body.position + (laser.transform.localScale.x /2 + col.radius) * tempDir.normalized;
        }
        else
        {
            objectTouched = null;

            tempPos = tempDir.normalized * longueurRayon + Body.position;
            laser.transform.position = tempDir.normalized * ((longueurRayon/2) + col.radius) + Body.position;
            laser.transform.localScale = new Vector3((tempPos - Body.position).magnitude, 0.5f, 0);
        }

        //Aim at
        AimInput(aimDir);
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        
        laserShooter.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (Input.GetMouseButton(LifeClick) || Input.GetMouseButton(DeathClick))
        {
            animator.SetBool("isShooting", true);
            laser.SetActive(true);
            laser.GetComponent<SpriteRenderer>().color = Input.GetMouseButton(LifeClick) ? lifeColor : deathColor;
            Color laserColor = laser.GetComponent<SpriteRenderer>().color;

            var main = laserParticle.main;
            main.startColor = laserColor;

            if (objectTouched)
            {
                if (objectTouched.CompareTag("enemy"))
                {
                    if (Input.GetMouseButton(LifeClick))
                    {
                        objectTouched.GetComponent<Enemy>().GetYoung();
                    }
                    else
                    {
                        objectTouched.GetComponent<Enemy>().GetOld();
                    }
                }

                if (objectTouched.CompareTag("Tree"))
                {
                    if (Input.GetMouseButton(LifeClick))
                    {
                        objectTouched.GetComponent<GrowTree>().LifeLaserHit();
                    }
                    else
                    {
                        objectTouched.GetComponent<GrowTree>().DeathLaserHit();
                    }
                }
            }

        }
        else
        {
            laser.SetActive(false);
            animator.SetBool("isShooting", false);
        }
    }

    public void AimInput(Vector3 aimDir)
    {
        aimDirection = aimDir;
        aimDirection.Normalize();
    }
}
