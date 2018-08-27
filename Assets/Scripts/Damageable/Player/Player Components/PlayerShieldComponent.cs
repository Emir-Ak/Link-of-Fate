using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShieldComponent : Damageable
{

    #region Variables

    #region Components
    [SerializeField]
    private Animator barValueAnimator; 
    [SerializeField]
    private Stat barShieldHealth;
    [SerializeField]
    public Text barValueText;

    [SerializeField]
    private PlayerAttackComponent playerAttackComponent;//Component that has the functions for the Attack :3
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController; //Script that controls the animations of the player and it's animaton
    [SerializeField]
    private PlayerController playerController; //Player Controller .-. (I want a cute waifu girlfriend D: (like inori or 02 ;/)) ...WELP WE ALL WANT SOMETHING UNREACHABLE SOMETIMES... Dreaming isn`t bad though :\
    #endregion

    private float maxShieldHealth; //Used to clamp the current shield health

    public float currentShieldHealth; //Current health of the shield

    public float shieldRechargeDelay = 5;//Time to start regenerating after beign hit
    public float regenAmount = 2; //Amount of health the shield regenarates at one step of regenerating


    private bool hasShieldRegenerated = true; //Has shield finished  regenerating?
    private bool isShieldRegenerating = false; //Is shield regenerating right now?

    [SerializeField]
    private bool _isUsingShield = false; //Is player currently using shield?
    private bool _isShieldBroken = false; //Is the Shield broken?
    [SerializeField]
    private bool _isShieldButtonPressed = false; //Is the button for Shield pressed

    #endregion

    #region Properties
    public bool IsUsingShield { get { return this._isUsingShield; } set { this._isUsingShield = value; } }
    public bool IsShieldBroken { get { return this._isShieldBroken; } set { this._isShieldBroken = value; } }
    //public bool isshieldregenerated { get { return this._isShieldRegenerated; } set { this._isShieldRegenerated = value; } }
    //public bool isshieldregenerating { get { return this._isShieldRegenerating; } set { this._isShieldRegenerating = value; } }
    public bool IsShieldButtonPressed { get { return this._isShieldButtonPressed; } set { this._isShieldButtonPressed = value; } }
    #endregion


    // Use this for initialization
    void Start()
    {
        #region GetComponent
        playerController = GetComponent<PlayerController>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        playerAttackComponent = GetComponent<PlayerAttackComponent>();

        #endregion
        maxShieldHealth = health;
        currentShieldHealth = maxShieldHealth;

        barShieldHealth.Initialize();
        barShieldHealth.CurrentVal = health;
        barValueText.text = health.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        currentShieldHealth = Mathf.Clamp(currentShieldHealth, 0, maxShieldHealth);

        if (!isShieldRegenerating && currentShieldHealth <= maxShieldHealth)
        {
            StartCoroutine(RegenerateShield(shieldRechargeDelay, regenAmount));
            isShieldRegenerating = true;
        }


        if (_isShieldButtonPressed && !_isShieldBroken && hasShieldRegenerated)
        {

            _isUsingShield = true;
            StartCoroutine(UseShield());
        }
        else if (!_isShieldButtonPressed)
        {
            _isUsingShield = false;
        }




        #region Animator_Controller_Values_Refresh
        playerAnimatorController.IsUsingShield = _isUsingShield;
        playerAnimatorController.IsShieldBroken = _isShieldBroken;
        #endregion

    }

    private IEnumerator UseShield()
    {
        playerController.isInvincible = true;
        playerAnimatorController.IsAnimationLocked = true;

        _isUsingShield = true;


        if (currentShieldHealth <= 0)
        {
          
  IsShieldBroken = true;
            playerController.Initialize();

        }

        yield return new WaitUntil(() => !_isUsingShield || _isShieldBroken);

        playerController.isInvincible = false;
        playerAnimatorController.IsAnimationLocked = false;
        _isUsingShield = false;
    }

    private IEnumerator RegenerateShield(float rechargeDelay, float regenAmount)
    {

        yield return new WaitUntil(() => IsUsingShield == false);
        yield return new WaitForSeconds(rechargeDelay);

        bool hasFinishedRegenerating = false;
        while (!hasFinishedRegenerating && IsUsingShield == false)
        {
            if (currentShieldHealth >= maxShieldHealth)
            {
                hasFinishedRegenerating = true;
                IsShieldBroken = false;
                hasShieldRegenerated = true;
                //Debug.Log("Finished regenerating shield");
                break;

            }

            currentShieldHealth += regenAmount;
            barShieldHealth.CurrentVal = (int)currentShieldHealth;

            if(IsShieldBroken == false)
            {
                barValueText.text = currentShieldHealth.ToString();
            }

            if (currentShieldHealth >= regenAmount && barValueAnimator.GetBool("isShieldBroken") == true)
            {
                barValueAnimator.SetBool("isShieldBroken", false);
            }

            yield return new WaitForSeconds(0.2f);
              
        }

        isShieldRegenerating = false;

        barValueText.text = currentShieldHealth.ToString();
    }


    public override void ReceiveDamage(float damageTaken)
    {
        if (_isUsingShield && !_isShieldBroken)
        {
            if (currentShieldHealth > damageTaken)
            {
                currentShieldHealth -= damageTaken;
                barShieldHealth.CurrentVal = currentShieldHealth;
                barValueText.text = currentShieldHealth.ToString();

                playerController.Initialize();
                string instantiatedText = "-" + damageTaken.ToString();
                playerController.textInst.InstantiateText(instantiatedText, transform.position, new Color32(122, 147, 255, 255));
            }
            else
            {
                playerController.Initialize();
                if (currentShieldHealth < damageTaken)
                {
                   float playerDamage = damageTaken - currentShieldHealth;
                   playerController.ReceiveDamage(playerDamage);

                    string instantiatedText = "-" + (playerDamage).ToString();
                   playerController.textInst.InstantiateText(instantiatedText, transform.position, new Color32(125, 20, 130, 255));
                }

                currentShieldHealth = 0;
                IsShieldBroken = true;
                barValueText.text = "BROKEN";
                barValueAnimator.SetBool("isShieldBroken", true);
                barShieldHealth.CurrentVal = currentShieldHealth;
            }

        }
    }
}
