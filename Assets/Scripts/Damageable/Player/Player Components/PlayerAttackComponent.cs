using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour {

    #region Variables

    #region Components
    
    [SerializeField]
    private PlayerAnimatorController animatorController;
    [SerializeField]
    public PlayerController playerController;
    #endregion

    #region For_Attack

    [SerializeField]
    private GameObject SwordAttackPrefab; // prefab for sword collider

    private GameObject attackCollder; //needed so that the collider is allocated to this variable

    private bool isPlayerAttacking;//Is the player currently attacking?

    private bool _isAttackButtonPressed = false; //Is the button for attack pressed


    public bool IsAttackButtonPressed { get { return this._isAttackButtonPressed; } set { this._isAttackButtonPressed = value; } }

    #endregion For_Attack

    #endregion

    private void Start()
    {
        
        animatorController = GetComponent<PlayerAnimatorController>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        
        if (!isPlayerAttacking && IsAttackButtonPressed && !animatorController.IsPlayerMoving)
        {
            Debug.Log("Started");
            isPlayerAttacking = true;
            StartAttack();
        }



        //This is what I call stage debigging :3. I already know how to fix the 1 hit bug. But I though you won't like it so I leave it up to you.
        //PS. If you ask me on Discord  I will help.
        if (isPlayerAttacking)
        {
            Debug.Log("Player is attacking");
            if (animatorController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("1 sec passed");
                if (!animatorController.animator.IsInTransition(0))
                {
                    Debug.Log("No transition occuring");

                    if (animatorController.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
                    {
                        Debug.Log("PlayerAttack animation is running");
                        Debug.Log("End");
                        EndAttack();
                        isPlayerAttacking = false;
                    }
                }
            }
        }
        else if (!animatorController.animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && !playerController.IsPlayerMoving)
        {
            EndAttack();
        }



        #region Animator_Controller_Values_Refresh
        animatorController.IsPlayerAttacking = isPlayerAttacking;
        //animatorController.IsShieldBroken = _isShieldBroken;
        #endregion

    }

    private void StartAttack()
    {

        playerController.speed = 0;

        animatorController.StartAttackAnimation();


        attackCollder = Instantiate(SwordAttackPrefab, transform);
        attackCollder.GetComponent<SwordCollider>().ChangeOffset(animatorController.LastMove);
    }

    private void EndAttack()
    {
        Destroy(attackCollder);
        playerController.speed = playerController.standardMoveSpeed;

        animatorController.EndAttackAnimation();
        animatorController.IsPlayerMoving = false;
    }
}
