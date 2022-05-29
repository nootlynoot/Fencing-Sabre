using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[Header("Inputs")]
	public Player playerInput;

	[Header("Components")]
    public Stamina stamina;
    public LevelManager levelManager;
    playerMovement playerMvmnt;
    public AudioClip swordSound,parrysound;
    public GameObject moveSound, dashSound;
    public AudioSource audioSource;
    public Transform otherPlayer;
	[HideInInspector]
	public Animator anim;
	public GameObject parryCol1,parryCol2;

	[Header("Variables")]
	public int changePriority;
	public bool priority;
	public int player;
    float atkStart = 0f;
    public float atkCooldown = 0.5f;
	public bool WarmUp;
    //[HideInInspector]
    public int atkNum;
    public bool headpry, chestpry, sidepry;
	// Start is called before the first frame update
	void Start()
    {
       // gameObject.tag = "neutral";
        playerMvmnt = GetComponentInParent<playerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Inputs()
    {
        
        if (playerMvmnt.dashUsed)
        {
			
			if (anim.GetBool("Crossovered") == false && anim.GetBool("Flunged") == true)
			{
				if(!WarmUp)
				{
                    //Flunge Head
                    if (Input.GetButtonDown(playerInput.headAtk))
                    {
                        gameObject.tag = "headatk";
                        atkNum = 18;
                    }
                    //Flunge Side
                    else if (Input.GetButtonDown(playerInput.leftAtk))
                    {
                        gameObject.tag = "sideatk";
                        atkNum = 17;
                    }
                    //Flunge Chest
                    else if (Input.GetButtonDown(playerInput.rightAtk))
                    {
                        gameObject.tag = "chestatk";
                        atkNum = 16;
                    }
				}
				else if (WarmUp)
				{
					//Flunge Head
					if (Input.GetButtonDown(playerInput.headAtk))
					{
                        gameObject.tag = "headatk";
                        atkNum = 18;
					}
					//Flunge Side
					else if (Input.GetButtonDown(playerInput.leftAtk))
					{
                        gameObject.tag = "sideatk";
                        atkNum = 17;
					}
					//Flunge Chest
					else if (Input.GetButtonDown(playerInput.rightAtk))
                    {
                        gameObject.tag = "chestatk";
                        atkNum = 16;
					}
				}
			}
		}

        //Stepbackward head
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x < 0 && !playerMvmnt.dodgeUsed )
            {
                 
                if (Input.GetButtonDown(playerInput.headAtk))
                {
                    gameObject.tag = "headatk";
                    atkNum = 15;
                    atkStart = playerMvmnt.time + atkCooldown;
				}
            }
        }
        //Stepbackward side
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x < 0 && !playerMvmnt.dodgeUsed)
            {
                 
                if (Input.GetButtonDown(playerInput.leftAtk))
                {
                    gameObject.tag = "sideatk";
                    atkNum = 14;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }
        //Stepbackward chest
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x < 0 && !playerMvmnt.dodgeUsed)
            {
                 
                if (Input.GetButtonDown(playerInput.rightAtk))
                {
                    gameObject.tag = "chestatk";
                    atkNum = 13;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }

        //Stepforward head
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x > 0 && !playerMvmnt.dashUsed)
            {
                 
                if (Input.GetButtonDown(playerInput.headAtk))
                {
                    gameObject.tag = "headatk";
                    atkNum = 12;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }
        //Stepforward side
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x > 0 && !playerMvmnt.dashUsed)
            {
                 
                if (Input.GetButtonDown(playerInput.leftAtk))
                {
                    gameObject.tag = "sideatk";
                    atkNum = 11;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }
        //Stepforward chest
        if (playerMvmnt.time > atkStart)
        {
            if (playerMvmnt.x > 0 && !playerMvmnt.dashUsed)
            {
                 
                if (Input.GetButtonDown(playerInput.rightAtk))
                {
                    gameObject.tag = "chestatk";
                    atkNum = 10;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }

        //Lunge Head
        if (playerMvmnt.time > atkStart)
        {
            if (Input.GetButton(playerInput.fastStep))
            {
                 
                if (Input.GetButtonDown(playerInput.headAtk))
                {
                    gameObject.tag = "headatk";
                    atkNum = 9;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }
        //Lunge Side
        if (playerMvmnt.time > atkStart)
        {
            if (Input.GetButton(playerInput.fastStep))
            {
                 
                if (Input.GetButtonDown(playerInput.leftAtk))
                {
                    gameObject.tag = "sideatk";
                    atkNum = 8;
                    atkStart = playerMvmnt.time + atkCooldown;
                }
            }
        }
        //Lunge Chest
        if (playerMvmnt.time > atkStart)
        {
			if (Input.GetButton(playerInput.fastStep))
			{
                 
                if (Input.GetButtonDown(playerInput.rightAtk))
                {
                    gameObject.tag = "chestatk";
                    atkNum = 7;
					atkStart = playerMvmnt.time + atkCooldown;
				}
            }
        }

        //Crossover Head Attack
        if (playerMvmnt.dodgeUsed)
		{
             
            if (Input.GetButtonDown(playerInput.headAtk) && anim.GetBool("Crossovered") == true && anim.GetBool("Flunged") == false)
			{
                gameObject.tag = "headatk";
                anim.SetTrigger("CrossoverHeadAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
            }
        }
        //Crossover Side Attack
        if (playerMvmnt.dodgeUsed)
        {
            if (Input.GetButtonDown(playerInput.leftAtk) && anim.GetBool("Crossovered") == true && anim.GetBool("Flunged") == false)
            {
                gameObject.tag = "sideatk";
                anim.SetTrigger("CrossoverSideAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
            }
        }
        //Crossover Chest Attack
        if (playerMvmnt.dodgeUsed)
        {
            if (Input.GetButtonDown(playerInput.rightAtk) && anim.GetBool("Crossovered") == true && anim.GetBool("Flunged") == false)
            {
                gameObject.tag = "chestatk";
                anim.SetTrigger("CrossoverChestAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
            }
        }

        //Head Attack
        if (playerMvmnt.time > atkStart)
		{
            if (Input.GetButtonDown(playerInput.headAtk) && anim.GetBool("Crossovered") == false && anim.GetBool("Flunged") == false)
            {
                gameObject.tag = "headatk";
                atkNum = 3;
                atkStart = playerMvmnt.time + atkCooldown;
            }
        }
        //Side Attack
        if(playerMvmnt.time > atkStart)
		{
            if (Input.GetButtonDown(playerInput.leftAtk) && anim.GetBool("Crossovered") == false && anim.GetBool("Flunged") == false)
            {
                gameObject.tag = "sideatk";
                atkNum = 2;
                atkStart = playerMvmnt.time + atkCooldown;
            }
        }
        //Chest Attack
        if(playerMvmnt.time > atkStart)
		{
            if (Input.GetButtonDown(playerInput.rightAtk) && anim.GetBool("Crossovered")==false && anim.GetBool("Flunged")==false)
            {
                gameObject.tag = "chestatk";
                atkNum = 1;
                atkStart = playerMvmnt.time + atkCooldown;
            }
        }

        //Head Parry
        if (Input.GetButtonDown(playerInput.headParry))
        {
            headpry = true;
			parryCol1.SetActive(true);
			parryCol2.SetActive(true);
			anim.SetTrigger("HeadPry");
            anim.SetBool("HeadPrry", true);
            audioSource.PlayOneShot(parrysound, 1f);
            StartCoroutine(stamina.DecreaseStamina(20f));
        }
        else if (Input.GetButtonUp(playerInput.headParry))
        {
            headpry = false;
            anim.SetBool("HeadPrry", false);
			parryCol1.SetActive(false);
			parryCol2.SetActive(false);

		}
        //Side Parry
        if (Input.GetButtonDown(playerInput.leftParry))
        {
            sidepry = true;
            parryCol1.SetActive(true);
			parryCol2.SetActive(true);
			anim.SetTrigger("SidePry");
            anim.SetBool("SidePrry", true);
            audioSource.PlayOneShot(parrysound, 1f);
            StartCoroutine(stamina.DecreaseStamina(20f));
        }
        else if (Input.GetButtonUp(playerInput.leftParry))
        {
            sidepry = false;
            anim.SetBool("SidePrry", false);
			parryCol1.SetActive(false);
			parryCol2.SetActive(false);

		}
        //Chest Parry
        if (Input.GetButtonDown(playerInput.rightParry))
        {
            chestpry = true;
            parryCol1.SetActive(true);
			parryCol2.SetActive(true);
			anim.SetTrigger("ChestPry");
            anim.SetBool("ChestPrry", true);
            audioSource.PlayOneShot(parrysound, 1f);
            StartCoroutine(stamina.DecreaseStamina(20f));
        }
        else if (Input.GetButtonUp(playerInput.rightParry))
        {
            chestpry = false;
            anim.SetBool("ChestPrry", false);
			parryCol1.SetActive(false);
			parryCol2.SetActive(false);

		}
    }

    void atks()
	{
		switch (atkNum)
		{
            case 0:
                return;

            case 1:
                //chest atk
                anim.SetTrigger("SideAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 2:
                //side atk
                anim.SetTrigger("ChestAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 3:
                //head atk
                anim.SetTrigger("HeadAtk");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 7:
                //lunge chest
                anim.SetTrigger("LungeChest");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 8:
                //lunge side
                anim.SetTrigger("LungeSide");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 9:
                //lunge head
                anim.SetTrigger("LungeHead");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 10:
                //stpfwd chest
                anim.SetTrigger("ForwardChest");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 11:
                //stpfwd side
                anim.SetTrigger("ForwardSide");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 12:
                //stpfwd head
                anim.SetTrigger("ForwardHead");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 13:
                //stpbck chest
                anim.SetTrigger("BackwardChest");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 14:
                //stpbck side
                anim.SetTrigger("BackwardSide");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 15:
                //stpbck head
                anim.SetTrigger("BackwardHead");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 16:
                //flunge chest
                playerMvmnt.state = playerMovement.State.flunging;
                anim.SetTrigger("FlungeChest");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 17:
                //flunge side
                playerMvmnt.state = playerMovement.State.flunging;
                anim.SetTrigger("FlungeSide");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
            case 18:
                //flunge head
                playerMvmnt.state = playerMovement.State.flunging;
                anim.SetTrigger("FlungeHead");
                audioSource.PlayOneShot(swordSound, 1f);
                StartCoroutine(whenAtk());
                StartCoroutine(stamina.DecreaseStamina(20f));
                 
                atkNum = 0;
                break;
        }
	}

    // Update is called once per frame
    void Update()
    {
        //print(gameObject.tag);
        Inputs();
        atks();
    }

    IEnumerator whenAtk()
	{
        yield return new WaitForSeconds(0.5f);
		if(!WarmUp)
		{
			if (player == 2 && !levelManager.RHit && priority)
			{
				levelManager.priority = changePriority;
			}
			if (player == 1 && !levelManager.LHit && priority)
			{
				levelManager.priority = changePriority;
			}
		}
		
    }
}