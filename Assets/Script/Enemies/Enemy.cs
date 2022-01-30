using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    [HideInInspector] public float moveSpeed;
    public int difficultyLevel;
    [HideInInspector] public int dmg;
    [HideInInspector] public int lifeStage;
    private Sprite sprite;
    public bool isShieldWhite;
    public bool isShieldViolet;
    [HideInInspector] public float resToRay; //Basically seconds to get effect from the ray
    private float tempRes;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Attack")]
    public float radiusDetection;
    public bool CaC;
    public float cooldown;
    private float savedCooldown;
    public GameObject projectile;
    public GameObject muzzle;
    public BoxCollider2D attackZone;

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
        target = GameManager.Instance.player;
        playerMovement = target.GetComponent<PlayerMovement>();
        savedCooldown = cooldown;

        //Init
        lifeStage = Random.Range(1, 5);

        if (!CaC)
        {
            ChooseSprite();
            if (lifeStage == 1)
                dmg = 0;
            else
                dmg = Fibonacci(lifeStage + difficultyLevel - 3);

            moveSpeed = playerMovement.moveSpeed * 0.4f; 
        }
        else
        {
            dmg = 1;


            
        }

        cooldown = Random.Range(3, 5);
        savedCooldown = cooldown;

        tempRes = resToRay;

        if (!TryGetComponent(out rb))
        {
            Debug.Log("no rigidbody" + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AdaptColliderSize();

        //Death
        if(lifeStage > 4 || lifeStage < 1)
            Destroy(gameObject);

        //Shield


        //"IA"
        Vector3 targetDir = target.transform.position - transform.position;

        if(targetDir.x < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (targetDir.x > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        if (targetDir.magnitude >= radiusDetection)
            return;

        transform.position += targetDir.normalized * moveSpeed * Time.deltaTime;

        //Jump (if needed)
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
            animator.SetTrigger("Shoot");
            if (!CaC)
                Shoot(targetDir);
            else
                Attack();
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
            animator.SetTrigger("Younger");
            tempRes = resToRay;
        };
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
            animator.SetTrigger("Older");
            tempRes = resToRay;
        }
    }

    private void Shoot(Vector3 dir)
    {
        GameObject fireball = Instantiate(projectile, transform.position, transform.rotation, transform);
        fireball.GetComponent<FireBall>().direction = dir;
        fireball.GetComponent<FireBall>().damage = dmg;
        cooldown = savedCooldown;
    }

    private void Attack()
    {
        if (!attackZone.IsTouching(target.GetComponent<CircleCollider2D>()))
            return;
        
        target.GetComponent<Player>().getHit(dmg);
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

    private void ChooseSprite()
    {
        switch(lifeStage)
        {
            case 1:
                animator.SetTrigger("1");
                break;
            case 2:
                animator.SetTrigger("2");
                break;
            case 3:
                animator.SetTrigger("3");
                break;
            case 4:
                animator.SetTrigger("4");
                break;
            default:break;
        }
    }

    private void AdaptColliderSize()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();

        switch (lifeStage)
        {
            case 1:
                capsule.size = new Vector2(0.08f, 0.16f);
                break;
            case 2:
                capsule.size = new Vector2(0.14f, 0.25f);
                break;
            case 3:
                capsule.size = new Vector2(0.16f, 0.32f);
                break;
            case 4:
                capsule.size = new Vector2(0.16f, 0.32f);
                break;
            default: break;
        }
    }
}
