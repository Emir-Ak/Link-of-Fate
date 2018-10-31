using UnityEngine.UI;
using UnityEngine;
using System;

public static class PlayerControlKeys
{

    public static int AttackKey = (int)KeyCode.Mouse0;
    public static int ShieldKey = (int)KeyCode.Mouse1;
    public static int LeftKey = (int)KeyCode.A;
    public static int RightKey = (int)KeyCode.D;
    public static int UpKey = (int)KeyCode.W;
    public static int DownKey = (int)KeyCode.S;
    public static int IneractionKey = (int)KeyCode.X;
    public static int DialogueKey = (int)KeyCode.Space;
    public static int ItemUseKey = (int)KeyCode.F;

    public static int MenuKey = (int)KeyCode.Escape;
}

public class PlayerController : Alive
{
    #region variables
    public static GameObject player;

    [Header("Player Variables")]
    [Space(8)]

    [SerializeField]
    private Stat barHealth;

    [SerializeField]
    private Text barValueText;

    #region Components
    [SerializeField] internal PlayerShieldComponent playerShieldComponent;//Component that has the functions for the shield :3
    [SerializeField] private PlayerAttackComponent playerAttackComponent;//Component that has the functions for the Attack :3
    [SerializeField]  private PlayerAnimatorController playerAnimatorController; //Script that controls the animations of the player and it's animator
    #endregion


    #region Movement_Variables

    private bool _isPlayerMoving;
    [HideInInspector]
    public float standardMoveSpeed; //The normal speed of the player (used to freeze or for some effects e.t.c)\


    #endregion Movement_Variables

    int rightKey, leftKey, upKey, downKey;

    private float hInput; //Horizontal input
    private float vInput; //Vertical Input

    [HideInInspector]
    public bool shouldFreeze = false;

    #region Properties
    public bool IsPlayerMoving { get { return this._isPlayerMoving; } set { this._isPlayerMoving = value; } }
    #endregion


    #endregion variables

    private void Awake()
    {
        if (player == null)
        {
            DontDestroyOnLoad(gameObject);
            player = gameObject;
        }
        else if (player != gameObject)
        {
            Debug.Log("destroyed" + gameObject);
            Destroy(gameObject);
        }
    }

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

        if (health <= 0)
        {
           
            health = 0;
            barValueText.text = "DEAD";
            gameObject.SetActive (false);
        }

        #region Player_Attack


        if (!shouldFreeze)
        {
            if (Input.GetKeyDown((KeyCode)PlayerControlKeys.AttackKey) && playerShieldComponent.IsUsingShield == false)
            {
                playerAttackComponent.IsAttackButtonPressed = true;
            }
            else if (Input.GetKeyUp((KeyCode)PlayerControlKeys.AttackKey) || playerShieldComponent.IsUsingShield == true)
            {
                playerAttackComponent.IsAttackButtonPressed = false;
            }
            else if (playerShieldComponent.IsShieldButtonPressed == false && (Input.GetKey((KeyCode)PlayerControlKeys.AttackKey)))
            {
                playerAttackComponent.IsAttackButtonPressed = true;
            }

            #endregion Player_Attack

            #region Player_Shield


            if (Input.GetKeyDown((KeyCode)PlayerControlKeys.ShieldKey) && playerShieldComponent.IsShieldButtonPressed == false)
            {
                speed = 0;
                playerShieldComponent.IsShieldButtonPressed = true;

            }
            else if (Input.GetKeyUp((KeyCode)PlayerControlKeys.ShieldKey))
            {
                speed = standardMoveSpeed;
                playerShieldComponent.IsShieldButtonPressed = false;
            }


            if (health < standardHealth / 2 - (standardHealth / 10) && barValueText.color != Color.red)
            {
                barValueText.color = Color.red;
            }
            else if (health >= standardHealth / 2 - (standardHealth / 10) && barValueText.color != Color.white)
            {
                barValueText.color = Color.white;
            }
        }

        #endregion Player_Shield

        #region Player_Movement

        #region Taking_Input
            rightKey = Convert.ToInt32(Input.GetKey((KeyCode)PlayerControlKeys.RightKey));
            leftKey = Convert.ToInt32(Input.GetKey((KeyCode)PlayerControlKeys.LeftKey));
            upKey = Convert.ToInt32(Input.GetKey((KeyCode)PlayerControlKeys.UpKey));
            downKey = Convert.ToInt32(Input.GetKey((KeyCode)PlayerControlKeys.DownKey));

            hInput = rightKey - leftKey;
            vInput = upKey - downKey;
            #endregion

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

        if (isRegenerating == true)
        {
            barHealth.CurrentVal = health;
            barValueText.text = ((int)health).ToString();
        }

        if (shouldFreeze)
        {
            rb.velocity = Vector2.zero;
        }
            playerAnimatorController.IsPlayerMoving = _isPlayerMoving;

    }

    /// <summary>
    /// takes damage into the player if doesn't collide with shield (raycast from position)
    /// </summary>
    /// <param name="damageTaken">damage amount</param>
    /// <param name="positionOfAttack"> where from the attack is taking place (your(enemy)) position)</param>
    public void ReceiveDamage(float damageTaken, Vector3 positionOfAttack)
    {
        if (!shouldFreeze) {
            if (playerShieldComponent.CheckIfDefended(positionOfAttack) && playerShieldComponent.IsShieldButtonPressed == true)
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


}