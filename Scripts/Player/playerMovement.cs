using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
	public bool invertControls;

	[Header("References")]
	public Player player;
	public CharacterController fencer;
	[HideInInspector]
	public Animator anim;
	public Image[] stats;
	public Stamina stamina;
	public PlayerController playerController;
	public LevelManager lvlManager;

	[Header("stats")]
	public int changePriority;
	public float oneStepSpeed;
	public float defenceSpeed;
	public float attackSpeed;
	public float baseStat = 1f;

	[HideInInspector]
	// for other scripts to ref if it moved
	public float x;

	[Header("Dodging")]
	public float dodgeSpeed, dashSpeed;
	public float dodgeCooldown, dashCooldown;
	float resetSpeed, resetSpeed2;
	float lastMoveTime, lastMoveTime2;
	public float dodgeTreshhold, dashTreshhold;
	bool dodge = false;
	bool dash = false;
	public bool dodgeUsed;
	public bool dashUsed;
	public float dir;
	public GameObject nearMissCol;

	// movement states
	public enum State
	{
		normal, dodging, flunging
	}
	public State state;

	public bool focus;
	[HideInInspector]
	public float deltaTime;
	[HideInInspector]
	public float time;

	private void Start()
	{
		resetSpeed = dodgeSpeed;
		resetSpeed2 = dashSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		switch(state)
		{
			case State.normal:
				Movement();
				dodgeManager();
				FlungeManager();
			break;

			case State.dodging:
				Dodging();
			break;

			case State.flunging:
				Flunging();
			break;
		}

		// when focused, this fencer will be unscaled by time
		if(!focus)
		{
			deltaTime = Time.deltaTime;
			time = Time.time;
			anim.updateMode = AnimatorUpdateMode.Normal;
		}
		else if (focus)
		{
			deltaTime = Time.unscaledDeltaTime;
			time = Time.unscaledTime;
			anim.updateMode = AnimatorUpdateMode.UnscaledTime;
		}
	}

	public void ChangePriority()
	{
		lvlManager.priority = changePriority;
	}

	public void Dodging()
	{
		transform.position += new Vector3(dir, 0) * dodgeSpeed * deltaTime;
		// to make dodge come to a stop
		dodgeSpeed -= dodgeSpeed * 10f * deltaTime;
		if(dodgeSpeed<10f)
		{
			state = State.normal;
		}
	}
	public void Flunging()
	{
		playerController.dashSound.SetActive(true);
		transform.position += new Vector3(dir, 0) * dashSpeed * deltaTime;
		// to make dodge come to a stop
		dashSpeed -= dashSpeed * 5f * deltaTime;
		if (dashSpeed < 5f)
		{
			playerController.dashSound.SetActive(false);
			state = State.normal;
		}
		
	}

	public void FlungeManager()
	{
		if (dashUsed)
		{
			anim.SetBool("Flunged", true);
			dashSpeed = resetSpeed2;
			dashCooldown += deltaTime;
			if (dashCooldown >= 1.5f)
			{
				anim.SetBool("Flunged", false);
				dashUsed = false;
				dashCooldown = 0f;
			}
		}

		if (x > 0)
		{
            if (!playerController.WarmUp)
            {
                if (lvlManager.canLunge())
                {
					if (dash)
					{
						if (invertControls)
						{
							dir = -1;
						}
						else if (!invertControls)
						{
							dir = 1;
						}
						// find the last time player move minus the previus attempt time
						float timeSinceLastMove2 = time - lastMoveTime2;
						if (timeSinceLastMove2 <= dashTreshhold)
						{
							// if within the threshhold, ready to flunge
							dashUsed = true;
						}
						// capture the time
						lastMoveTime2 = time;
						//so that it happens one time
						dash = false;
					}
				}
			}
			if (playerController.WarmUp)
			{
				if (dash)
				{
					if (invertControls)
					{
						dir = -1;
					}
					else if (!invertControls)
					{
						dir = 1;
					}
					// find the last time player move minus the previus attempt time
					float timeSinceLastMove2 = time - lastMoveTime2;
					if (timeSinceLastMove2 <= dashTreshhold)
					{
						// if within the threshhold, ready to flunge
						dashUsed = true;
					}
					// capture the time
					lastMoveTime2 = time;
					//so that it happens one time
					dash = false;
				}
			}
		}

		else if (x == 0)
		{
			// reset the dash
			if (!dashUsed)
			{
				dash = true;
			}
		}
	}

	public void dodgeManager()
	{
		// so that players don't spam dodge
		if (dodgeUsed)
		{
			anim.SetBool("Crossovered", true);
			nearMissCol.SetActive(true);
			dodgeSpeed = resetSpeed;
			dodgeCooldown += deltaTime;
			if (dodgeCooldown >= 1.5f)
			{
				anim.SetBool("Crossovered", false);
				nearMissCol.SetActive(false);
				dodgeUsed = false;
				dodgeCooldown = 0f;
			}
		}

		if (x < 0)
		{
			if (dodge)
			{
				if (invertControls)
				{
					dir = 1;
				}
				else if(!invertControls)
				{
					dir = -1;
				}
				
				// find the last time player move minus the previus attempt time
				float timeSinceLastMove = time - lastMoveTime;
				if (timeSinceLastMove <= dodgeTreshhold)
				{
					anim.SetTrigger("Crossover");
					playerController.dashSound.SetActive(true);
					// if within the threshhold, player dodge back
					dodgeUsed = true;
					state = State.dodging;
				}
				// capture the time
				lastMoveTime = time;
				//so that it happens one time
				dodge = false;
			}
		}
		else if (x == 0)
		{
			// reset the dodge
			if (!dodgeUsed)
			{
				dodge = true;
				playerController.dashSound.SetActive(false);
			}
		}
	}

	public void Movement()
	{
		oneStepSpeed = baseStat + stats[0].fillAmount;
		attackSpeed = baseStat + stats[1].fillAmount;
		defenceSpeed = baseStat + stats[2].fillAmount;
		
		anim.SetFloat("DefenceSpeed", defenceSpeed);
		anim.SetFloat("AttackSpeed", attackSpeed);

		// get axis from sciptable object
		switch(invertControls)
		{
			case false:
				x = Input.GetAxis(player.horizontal);
			break;

			case true:
				x = -Input.GetAxis(player.horizontal);
			break;
		}

		anim.SetFloat("X", x);
		if (x > 0)
		{
			playerController.moveSound.SetActive(true);
			if (Input.GetButton(player.fastStep))
			{
				StartCoroutine(stamina.DecreaseStamina(0.1f));
				// make onestep faster
				oneStepSpeed += 1;
			}
		}
		else if (x < 0)
		{
			playerController.moveSound.SetActive(true);
			if (Input.GetButton(player.fastStep))
			{
				StartCoroutine(stamina.DecreaseStamina(0.1f));
				// make onestep faster
				oneStepSpeed += 1;
			}
			// change priority if attacker is moving back
			if (playerController.priority && !lvlManager.LHit && !lvlManager.RHit)
			{
				ChangePriority();
			}
		}
		else if( x == 0)
		{
			playerController.moveSound.SetActive(false);
			//anim.SetTrigger("backToIdle");
		}
		anim.SetFloat("FootWorkSpeed", oneStepSpeed);
		fencer.Move(transform.forward * x * oneStepSpeed * deltaTime);
	}
}
