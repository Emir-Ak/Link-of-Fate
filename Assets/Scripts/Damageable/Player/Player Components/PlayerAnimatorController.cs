using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

    #region Variables

    #region Components
    [SerializeField]
    private PlayerShieldComponent playerShieldComponent;//Component that has the functions for the shield :3
    [SerializeField]
    private PlayerAttackComponent playerAttackComponent;//Component that has the functions for the Attack :3
    #endregion


    #region Animator_Variables
    public Animator animator; //Animator of the player

    private bool _isUsingShield = false; //Is player currently using shield?
    private bool _isShieldBroken = false; //Is the Shield broken?
    private bool _isAnimationLocked = false; //Is the direction of animation locked?
    private bool _isPlayerMoving;//Is the player currently moving?
    private bool _isPlayerAttacking;//Is the player currently attacking?
    private Vector2 _lastMove;//shows the last direction that the player was facing (current direction)
    #endregion

    #region Properties
    public bool IsUsingShield { get { return this._isUsingShield; } set { this._isUsingShield = value; } }
    public bool IsShieldBroken { get { return this._isShieldBroken; } set { this._isShieldBroken = value; } }
    public bool IsAnimationLocked { get { return this._isAnimationLocked; } set { this._isAnimationLocked = value; } }
    public bool IsPlayerMoving { get { return this._isPlayerMoving; } set { this._isPlayerMoving = value; } }
    public bool IsPlayerAttacking { get { return this._isPlayerAttacking; } set { this._isPlayerAttacking = value; } }
    public Vector2 LastMove { get { return this._lastMove; } set { this._lastMove = value; } }
    #endregion

    #endregion

    private void Start()
    {
        #region GetComponent
        animator = GetComponent<Animator>();
        playerShieldComponent = GetComponent<PlayerShieldComponent>();
        playerAttackComponent = GetComponent<PlayerAttackComponent>();
        #endregion
    }

    private void Update()
    {
        #region Animator_params
        animator.SetBool("IsMoving", _isPlayerMoving);
        animator.SetBool("IsShielding", _isUsingShield);
        animator.SetBool("IsShieldBroken", _isShieldBroken);
        animator.SetFloat("LastMoveX", _lastMove.x);
        animator.SetFloat("LastMoveY", _lastMove.y);
        #endregion Animator_params
    }

    public void StartAttackAnimation()
    {
        animator.SetTrigger("AttackStart");
    }

    public void EndAttackAnimation()
    {
        animator.SetTrigger("AttackEnd");
    }
}
