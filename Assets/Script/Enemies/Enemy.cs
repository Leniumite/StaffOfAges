using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    public float moveSpeed;
    public int dmg;
    public int lifeStage;
    public Sprite sprite;
    public bool isShieldWhite;
    public bool isShieldViolet;
    public float resToRay; //Basically seconds to get effect from the ray
    private float tempRes;

    [Header("Attack")]
    public bool CaC;
    public float cooldown;
    public GameObject projectile;

    [Header("IA")]
    public GameObject target;
    public List<GameObject> jumpPoints = new List<GameObject>();
    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask Ground;

    public float jumpForce;
    public float jumpTimerBase;
    private float jumpTimer;
    private bool isJumping;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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
            if((go.transform.position - transform.position).magnitude < 0.8)
            {
                Jump();
            }
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
            tempRes = resToRay;
        }

        Debug.Log("young");
    }

    public void GetOld()
    {
        if (isShieldViolet)
            return;

        tempRes -= Time.deltaTime;
        if (tempRes <= 0)
        {
            lifeStage++;
            tempRes = resToRay;
        }

        Debug.Log("Old");
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
}