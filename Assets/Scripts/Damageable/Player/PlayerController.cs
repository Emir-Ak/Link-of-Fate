using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PlayerController : Damageable
{

    #region variables

    [Header("Player Variables")]
    [Space(8)]
    #region Player_Stats
    public int swordDamage = 20;

    #endregion

    [SerializeField]
    private GameObject SwordAttackPrefab;

    private GameObject attackCollder;

    #region Movement_Variables
    //Movement values
    public float standardMoveSpeed;
    #endregion

    #region Animator_Variables
    //values for animations
    private Animator animator;

    private bool isPlayerMoving;
    private bool isPlayerAttacking;
    private Vector2 lastMove;

    #endregion

    float hInput;
    float vInput;

    #endregion
    // Use this for initialization
    void Start()
    {

        standardMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(gameObject, 0.4f);
        }

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
            lastMove = new Vector2(hInput, 0);

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
            lastMove = new Vector2(0, vInput);

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

        if (!isPlayerAttacking && Input.GetKeyDown(KeyCode.Z) && !isPlayerMoving)
        {
            Debug.Log("Starting Animation");
            isPlayerAttacking = true;
            StartAttackAnim();
        }

        //animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        if (isPlayerAttacking && animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !animator.IsInTransition(0))
        {
            Debug.Log("Ending Animation");
            EndAttackAnim();
            isPlayerAttacking = false;
        }


        #endregion

        #region Animator_params
        animator.SetFloat("MoveX", hInput);
        animator.SetFloat("MoveY", vInput);
        animator.SetBool("IsMoving", isPlayerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        #endregion

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

}
