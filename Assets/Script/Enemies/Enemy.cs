using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    /*[HideInInspector]*/ public float moveSpeed;
    public int difficultyLevel;
    [HideInInspector] public int dmg;
    public int lifeStage;
    public Sprite sprite;
    public bool isShieldWhite;
    public bool isShieldViolet;
    public float resToRay; //Basically seconds to get effect from the ray
    private float tempRes;

    [Header("Attack")]
    public bool CaC;
    public float cooldown;
    private float savedCooldown;
    public GameObject projectile;

    [Header("IA")]
    public GameObject target;
    private PlayerMovement playerMovement;
    public List<GameObject> jumpPoints = new List<GameObject>();
    private bool isGrounded;
    public float checkRadius;
    public LayerMask Ground;

    public float jumpTreshold;
    public float jumpForce;
    public float jumpTimerBase;
    private float jumpTimer;
    private bool isJumping;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = target.GetComponent<PlayerMovement>();
        savedCooldown = cooldown;

        //Init
        if (!CaC)
        {
            if (lifeStage == 1)
                dmg = 0;
            else
                dmg = Fibonacci(lifeStage + difficultyLevel - 3);

            moveSpeed = playerMovement.moveSpeed * 0.4f; 
        }

        //gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        tempRes = resToRay;

        if (!TryGetComponent(out rb))
        {
            Debug.Log("no rigidbody" + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Death
        if(lifeStage > 4 || lifeStage < 1)
        {
            Destroy(gameObject);
        }

        //Shield


        //"IA"
        Vector3 targetDir = target.transform.position - transform.position;
        transform.position += targetDir.normalized * moveSpeed * Time.deltaTime;

        foreach(GameObject go in jumpPoints)
        {
            if((go.transform.position - transform.position).magnitude < jumpTreshold)
            {
                Jump();
            }
        }

        //Attack
        cooldown -= Time.deltaTime;

        if(cooldown <= 0)
        {
            if (!CaC)
                Shoot(targetDir);
            /*else
                Attack();*/
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
            ChangeLifeStageValue();
            tempRes = resToRay;
        }

        //Debug.Log("young");
    }

    public void GetOld()
    {
        if (isShieldViolet)
            return;

        tempRes -= Time.deltaTime;
        if (tempRes <= 0)
        {
            lifeStage++;
            ChangeLifeStageValue();
            tempRes = resToRay;
        }

        //Debug.Log("Old");
    }

    private void Shoot(Vector3 dir)
    {
        GameObject fireball = Instantiate(projectile, transform.position, transform.rotation, transform);
        fireball.GetComponent<FireBall>().direction = dir;
        fireball.GetComponent<FireBall>().damage = dmg;
        cooldown = savedCooldown;
    }

    private void Jump()
    {
        isJumping = true;
        jumpTimer = jumpTimerBase;
        rb.velocity = Vector2.up * jumpForce;

        if (isJumping)
        {
            if (jumpTimer > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        isJumping = false;
    }

    private int Fibonacci(int n)
    {
        if (n <= 1)
            return 1;
        else
            return Fibonacci(n-1) + Fibonacci(n-2);
    }

    private void ChangeLifeStageValue()
    {
        if (!CaC)
        {
            if (lifeStage == 1)
                dmg = 0;
            else
                dmg = Fibonacci(lifeStage + difficultyLevel - 3);
        }
    }
}
