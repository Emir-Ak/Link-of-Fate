﻿using UnityEngine.UI;
using UnityEngine;

public class PlayerController : Alive
{
    #region variables

    [Header("Player Variables")]
    [Space(8)]

    [SerializeField]
    private Stat barHealth;

    [SerializeField]
    private Text barValueText;

    #region Components
    [SerializeField]
    internal PlayerShieldComponent playerShieldComponent;//Component that has the functions for the shield :3
    [SerializeField]
    private PlayerAttackComponent playerAttackComponent;//Component that has the functions for the Attack :3
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController; //Script that controls the animations of the player and it's animator

    #endregion


    #region Movement_Variables

    private bool _isPlayerMoving;
    [HideInInspector]
    public float standardMoveSpeed; //The normal speed of the player (used to freeze or for some effects e.t.c)\


    #endregion Movement_Variables
    private float hInput; //Horizontal input
    private float vInput; //Vertical Input

    #region Properties
    public bool IsPlayerMoving { get { return this._isPlayerMoving; } set { this._isPlayerMoving = value; } }
    #endregion


    #endregion variables

    private void Start()
    {
        #region GetComponent
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        playerAttackComponent = GetComponent<PlayerAttackComponent>();
        rb = GetComponent<Rigidbody2D>();
        #endregion

        standardHealth = health;
        standardMoveSpeed = speed;

        barHealth.Initialize();
        barHealth.CurrentVal = health;
        barValueText.text = health.ToString();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (playerShieldComponent.IsUsingShield == true)
            speed = standardMoveSpeed/2;
        else
            speed = standardMoveSpeed;

        if (health <= 0)
        {
            Destroy(gameObject, 0.4f);
        }

        #region Player_Attack


        if (Input.GetMouseButtonDown(0) && playerShieldComponent.IsUsingShield == false)
        {
            playerAttackComponent.IsAttackButtonPressed = true;
        }
        else if (Input.GetMouseButtonUp(0) || playerShieldComponent.IsUsingShield == true)
        {
            playerAttackComponent.IsAttackButtonPressed = false;
        }

        #endregion Player_Attack

        #region Player_Shield


        if (Input.GetMouseButtonDown(1) && playerShieldComponent.IsShieldButtonPressed == false)
        {
            
            playerShieldComponent.IsShieldButtonPressed = true;
           
        }
        if (Input.GetMouseButtonUp(1))
        {
            
            playerShieldComponent.IsShieldButtonPressed = false;
        }

        if(health < standardHealth/2 - (standardHealth/10) && barValueText.color != Color.red)
        {
            barValueText.color = Color.red;
        }
        else if(health >= standardHealth/2 - (standardHealth/10) && barValueText.color != Color.white)
        {
            barValueText.color = Color.white;
        }

        #endregion Player_Shield

        #region Player_Movement

        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (hInput > .5f || hInput < -.5f)
        {

            rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
            _isPlayerMoving = true;
            if (!playerAnimatorController.IsAnimationLocked)
            {
                playerAnimatorController.LastMove = new Vector2(hInput, 0);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (vInput > .5f || vInput < -.5f)
        {


            rb.velocity = new Vector2(rb.velocity.x, vInput * speed);
            _isPlayerMoving = true;

            if (!playerAnimatorController.IsAnimationLocked)
            {
                playerAnimatorController.LastMove = new Vector2(0, vInput);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (rb.velocity.x == 0f && rb.velocity.y == 0f)
        {
            _isPlayerMoving = false;

        }
        #endregion Player_Movement

        if(isRegenerating == true)
        {
            barHealth.CurrentVal = health;
            barValueText.text = health.ToString();
        }


        playerAnimatorController.IsPlayerMoving = _isPlayerMoving;

    }

    /// <summary>
    /// takes damage into the player if doesn't collide with shield (raycast from position)
    /// </summary>
    /// <param name="damageTaken">damage amount</param>
    /// <param name="positionOfAttack"> where from the attack is taking place (your(enemy)) position)</param>
    public void ReceiveDamage(float damageTaken,Vector3 positionOfAttack)
    {
        if (playerShieldComponent.CheckIfDefended(positionOfAttack))
        {
            playerShieldComponent.ReceiveDamage(damageTaken);
        }
        else
        {
            base.ReceiveDamage(damageTaken);
            barHealth.CurrentVal = health;
            barValueText.text = health.ToString();
        }

    }


}