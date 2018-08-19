using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : Alive
{

    #region variables

    [Header("Player Variables")]
    [Space(8)]

    #region Player_Stats

    #region For_Shield
    public float rechargeAmount = 2; //Amount of health the shield regenarates at one step of regenerating
    public float currentShieldHealth; //Current health of the shield
    public float maxShieldHealth = 100; //Used to clamp the current shield health
    public float shieldRechargeDelay = 5;//Time to start regenerating after beign hit

    private bool isShielding = false; //Is player currently using shield?
    private bool isShieldBroken = false; //Is the Shield broken?
    #endregion

    #endregion

    #region For_Attack
    [SerializeField]
    private GameObject SwordAttackPrefab; // prefab for sword collider
    private GameObject attackCollder; //needed so that the collider is allocated to this variable
    #endregion

    #region Movement_Variables
    public float standardMoveSpeed; //The normal speed of the player (used to freeze or for some effects e.t.c)\
    private bool isAnimationLocked = false; //Is the direction of animation locked?
    #endregion

    #region Animator_Variables
    //values for animations
    private Animator animator; //Animator of the player

    private bool isPlayerMoving;//Is the player currently moving?
    private bool isPlayerAttacking;//Is the player currently attacking?
    private Vector2 lastMove;//shows the last direction that the player is was facing (current direction)

    #endregion

    float hInput; //Horizontal input
    float vInput; //Veretical Input

    #endregion

    void Start()
    {
        currentShieldHealth = maxShieldHealth;
        standardMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        Mathf.Clamp(currentShieldHealth,0,maxShieldHealth);


        if (health <= 0)
        {
            Destroy(gameObject, 0.4f);
        }

        #region Player_Shield

        if (Input.GetMouseButton(1) && !isShieldBroken)
        {
            isShielding = true;
            StartCoroutine(UseShield(shieldRechargeDelay,rechargeAmount));
        } 
        else if(Input.GetMouseButtonUp(1))
        {
            isShielding = false;
        }

        #endregion

        #region Player_Movement   

        
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        
        
        if (hInput > .5f || hInput < -.5f)
        {
            if (isPlayerAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
            {
                EndAttackAnim();
                isPlayerAttacking = false;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && !isPlayerMoving)
            {
                animator.ResetTrigger("AttackEnd");

            }
            speed = standardMoveSpeed;
            rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            isPlayerMoving = true;
            if (!isAnimationLocked)
            {
                lastMove = new Vector2(hInput, 0);
            }

        }
        else
        {

            rb.velocity = new Vector2(0, rb.velocity.y);

        }


        if (vInput > .5f || vInput < -.5f)
        {
            if (isPlayerAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
            {
                EndAttackAnim();
                isPlayerAttacking = false;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && !isPlayerMoving)
            {
                animator.ResetTrigger("AttackEnd");

            }
            speed = standardMoveSpeed;
            rb.velocity = new Vector2(rb.velocity.x, vInput * speed);
            isPlayerMoving = true;

            if (!isAnimationLocked)
            {
                lastMove = new Vector2(0, vInput);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.x == 0f && rb.velocity.y == 0f)
        {
            isPlayerMoving = false;
        }

        #endregion

        #region Player_Attack

        if (!isPlayerAttacking && Input.GetMouseButtonDown(0) && !isPlayerMoving)
        {
            isPlayerAttacking = true;
            StartAttackAnim();
        }

        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        if (isPlayerAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !animator.IsInTransition(0))
        {
            EndAttackAnim();
            isPlayerAttacking = false;
        }


        #endregion



        #region Animator_params
        animator.SetFloat("MoveX", hInput);
        animator.SetFloat("MoveY", vInput);
        animator.SetBool("IsMoving", isPlayerMoving);
        animator.SetBool("IsShielding", isShielding);
        animator.SetBool("IsShieldBroken", isShieldBroken);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        #endregion

    }

    #region Other_Methods
    IEnumerator RechargeShield(float rechargeTime)
    {
        isShielding = true;
        yield return new WaitForSeconds(rechargeTime);
        isShielding = false;
    }

    void StartAttackAnim()
    {
        speed = 0;

        animator.SetTrigger("AttackStart");

        attackCollder = Instantiate(SwordAttackPrefab, transform);
        attackCollder.GetComponent<SwordCollider>().ChangeOffset(lastMove);
    }

    void EndAttackAnim()
    {

        animator.SetTrigger("AttackEnd");
        Destroy(attackCollder);

        speed = standardMoveSpeed;

        isPlayerMoving = false;
    }
    #endregion

    #region Overrides

    private  IEnumerator UseShield(float rechargeDelay, float regenAmount)
    {
        Debug.Log("isShielding" + isShielding);

        isInvincible = true;
        isAnimationLocked = true;

        animator.SetBool("IsShielding", isShielding);
        animator.SetBool("IsShieldBroken", isShieldBroken);


        if (currentShieldHealth <= 0)
        {
            isShieldBroken = true;
        }

        yield return new WaitUntil(() => !isShielding || isShieldBroken);

        isInvincible = false;
        isAnimationLocked = false;

        animator.SetBool("IsShielding", isShielding);
        animator.SetBool("IsShieldBroken", isShieldBroken);

        Debug.Log("isShielding" + isShielding);

    }

    public override void ReceiveDamage(float damageTaken, bool isEnemy)
    {
        if (isEnemy == false)
        {
            ApplyStates();
        }
        if (isInvincible == false)
        {
            health -= damageTaken;
            StartCoroutine(Damaged());
            Debug.Log("-" + damageTaken + "hp");
        }
        else if(isShielding && !isShieldBroken)
        {
            currentShieldHealth -= damageTaken;
            


            Debug.Log("Shield is damaged by " + damageTaken + "hp");
        }
    }

    #endregion
}
