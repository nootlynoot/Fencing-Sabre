using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
	public Player player;
	public playerMovement playerMovement;
	public PlayerController playerController;
	[HideInInspector]
	public Animator anim;
	public Image[] greenFill, redFill;
	public float regenAmount;

	public GameObject staminaVFX;

	[HideInInspector]
	public float stamina = 101f, stamina2 = 101f;

	public bool infiniteActive;

	private void Start()
	{
		stamina = 101f;
		stamina2 = 101f;
	}

	// Update is called once per frame
	void Update()
    {
		//stamina = 1000000000000f;
		//stamina2 = 1000000000000f;

		greenFill[0].fillAmount = stamina / 101;
		redFill[0].fillAmount = stamina2 / 101;
		greenFill[1].fillAmount = stamina / 101;
		redFill[1].fillAmount = stamina2 / 101;

		float rest = Input.GetAxis(player.stamina);
		float x = Input.GetAxis(player.horizontal);

		if (rest<0 && x==0)
		{
			//print("resting");
			// change priority when reseting
			if(playerController.priority)
			{
				playerMovement.ChangePriority();
			}
			anim.SetTrigger("Rest");
			anim.SetBool("Resting",true);
			
			staminaVFX.SetActive(true);
			// while resting, disable movement
			stamina += regenAmount;
			stamina2 += regenAmount;
			playerMovement.enabled = false;
			playerController.enabled = false;
		}
		else if(stamina <=0)
		{
			// if no stamina, disable movement
			playerMovement.enabled = false;
			anim.Play("Idle");
			gameObject.tag = "neutral";
			playerController.headpry = false;
			playerController.sidepry = false;
			playerController.chestpry = false;
			playerController.enabled = false;
		}
		else
		{
			// other than that enable movement
			anim.SetBool("Resting", false);
			anim.SetTrigger("backToIdle");
			staminaVFX.SetActive(false);
			playerMovement.enabled = true;
			playerController.enabled = true;
		}

		// stamina clamp at 0 to 100 when infinite is not active
		if(!infiniteActive)
		{
			stamina = Mathf.Clamp(stamina, 0f, 101f);
			stamina2 = Mathf.Clamp(stamina2, 0f, 101f);
		}
		else if(infiniteActive)
		{
			stamina = 1000000000000f;
			stamina2 = 1000000000000f;
		}		
	}

	public void ResetStamina()
	{
		stamina = 101f;
		stamina2 = 101f;
		infiniteActive = false;
	}
	

	public IEnumerator DecreaseStamina(float energyUsed)
	{ 
		stamina -= energyUsed;
		yield return new WaitForSeconds(0.5f);
		stamina2 -= energyUsed;
	}
}
