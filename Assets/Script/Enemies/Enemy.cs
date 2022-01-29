using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    [HideInInspector] public float moveSpeed;
    public int difficultyLevel;
    [HideInInspector] public int dmg;
    public int lifeStage;
    private Sprite sprite;
    public bool isShieldWhite;
    public bool isShieldViolet;
    public float resToRay; //Basically seconds to get effect from the ray
    private float tempRes;
    //public List<Sprite> spriteAges = new List<Sprite>();
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [Header("Attack")]
    public bool CaC;
    private float cooldown;
    private float savedCooldown;
    public GameObject projectile;
    public GameObject muzzle;

    [Header("IA")]
    public float radiusDetection; //The radius at wich the enemy detect the player
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

        //Define the first sprite
        ChooseSprite();

        //Init
        if (!CaC)
        {
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
        //Death
        if(lifeStage > 4 || lifeStage < 1)
        {
            Destroy(gameObject);
        }

        //Shield


        //"IA"
        Vector3 targetDir = target.transform.position - transform.position;
        if (targetDir.magnitude >= radiusDetection)
            return;

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
            animator.SetTrigger("Younger"); //Also change the sprite
            tempRes = resToRay;
        }
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
            animator.SetTrigger("Older"); //Also change the sprite
            tempRes = resToRay;
        }
    }

    private void Shoot(Vector3 dir)
    {
        animator.SetTrigger("Shoot");
        
        GameObject fireball = Instantiate(projectile, transform.position, transform.rotation);
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
}
