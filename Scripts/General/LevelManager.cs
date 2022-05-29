using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public bool testingMode;
	int LScore = 0;
	int RScore = 0;
	public float minDistance;
	public Text LScoreText, RScoreText;

	public Image LPriority;
	public Image RPriority;
	public Image LPriority2;
	public Image RPriority2;
	public Sprite attack, defend;
	public GameObject LPlayer, RPlayer;
	public PlayerController LController, RController;
	public playerMovement LMovement, RMovement;
	public Stamina LStamina, RStamina;
	public Transform playerL, playerR, LStart, RStart;
	public int priority;
	public GameObject LeftLight, RightLight;
	public PerkManager LPerk, RPerk;
	onHitVFX[] hitVfx;
	float distancebetween;

	float startTimer = 4f;
	public GameObject countdownText;
	bool start;
	bool halfTime;
	bool stop;

	public bool LHit;
	public bool RHit;

	Animator LAnim, RAnim;

	public PauseMenu pauseScript;

	[Header("Victory")]
	public int victoryScore;
	public CameraManager camManager;
	public CameraZoom camZoom;
	public Transform LVictoryZoom, RVictoryZoom;
	public GameObject distanceBar;
	public GameObject[] playerConditions;
	public GameObject lReplayButton, rReplayButton;
	public bool victory;

	[Header("Halftime")]
	public int halftimeScore;
	public Animator halftimeAnim;
	public StatSelect statsL, statsR;
	public StatSetter statSetter;

	public HitBox sword1, sword2, sword3, sword4;

	SoundSystem soundSystem;

	private void Start()
	{
		LAnim = LMovement.anim;
		RAnim = RMovement.anim;
		soundSystem = FindObjectOfType<SoundSystem>();
		hitVfx = FindObjectsOfType<onHitVFX>();
	}

	void Update()
    {
		#region start and pause
		// timer will start after short countdown
		if (!start && !pauseScript.paused && !stop && !victory)
		{
			LController.headpry = false;
			LController.sidepry = false;
			LController.chestpry = false;
			RController.headpry = false;
			RController.sidepry = false;
			RController.chestpry = false;
			LAnim.Play("Idle");
			RAnim.Play("Idle");
			startTimer -= Time.deltaTime;
			countdownText.SetActive(true);
			LHit = false;
			RHit = false;
			DontMove();
			//when timer end, reset position and stamina
			if (startTimer <= 0f)
			{
				countdownText.SetActive(false);
				StartFighting();
				startTimer = 4f;
				start = true;
			}
		}
		else if(pauseScript.paused)
		{
			start = false;
			startTimer = 4f;
			countdownText.SetActive(false);
			DisableOrEnableScripts(false);
		}
		#endregion

		#region Priority
		// check only during no priority state
		if (priority==0 && start)
		{
			// check who moves first
			float moveFirst = LMovement.x - RMovement.x;
			//who has higher movespeed
			float higherMoveSpeed = LMovement.oneStepSpeed - RMovement.oneStepSpeed;

			// if p1 move first
			if (moveFirst > 0)
			{
				priority = 1;
			}
			// if p2 move first
			else if (moveFirst < 0)
			{
				priority = 2;
			}
			// if they move at the same time
			else if (moveFirst == 0 && LMovement.x > 0 && RMovement.x > 0)
			{
				// p1 has higher speed
				if (higherMoveSpeed > 0)
				{
					priority = 1;
				}
				// p2 has higher speed
				else if (higherMoveSpeed < 0)
				{
					priority = 2;
				}
			}
		}		
		
		// priority conditions and changes
		switch(priority)
		{
			// no priority state
			case 0:
				LPriority.gameObject.SetActive(false);
				RPriority.gameObject.SetActive(false);
				LPriority2.gameObject.SetActive(false);
				RPriority2.gameObject.SetActive(false);
			break;
			// when player 1 has priorty
			case 1:
				LPriority.gameObject.SetActive(true);
				RPriority.gameObject.SetActive(true);
				LPriority.sprite = attack;
				RPriority.sprite = defend;
				LPriority2.gameObject.SetActive(true);
				RPriority2.gameObject.SetActive(true);
				LPriority2.sprite = attack;
				RPriority2.sprite = defend;
				LController.priority = true;
				RController.priority = false;
			break;
			// when player 2 has priorty
			case 2:
				LPriority.gameObject.SetActive(true);
				RPriority.gameObject.SetActive(true);
				LPriority.sprite = defend;
				RPriority.sprite = attack;
				LPriority2.gameObject.SetActive(true);
				RPriority2.gameObject.SetActive(true);
				LPriority2.sprite = defend;
				RPriority2.sprite = attack;
				RController.priority = true;
				LController.priority = false;
			break;
		}
		#endregion

		#region Scoring 
		HalfTimeEvent();
		if (LScore == victoryScore)
		{
			//left victory
			// change audio mixer snapshot
			if (!testingMode)
			{
				soundSystem.playerWin = victory;
			}
			PlayerVictory(LVictoryZoom, "Lvictory", LAnim, lReplayButton, LMovement.player);
			victory = true;
			countdownText.SetActive(false);
		}
		else if (RScore == victoryScore)
		{
			//right victory
			// change audio mixer snapshot
			if (!testingMode)
			{
				soundSystem.playerWin = victory;
			}
			PlayerVictory(RVictoryZoom, "Rvictory", RAnim, rReplayButton, LMovement.player);
			victory = true;
			countdownText.SetActive(false);
		}

		LScoreText.text = LScore.ToString("0");
		RScoreText.text = RScore.ToString("0");
		#endregion

		distancebetween = Vector3.Distance(playerL.position, playerR.position);
		//print(distancebetween);
	}
	public bool canLunge()
	{
		if (distancebetween < minDistance)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	// reset stamina and position
	public IEnumerator ResetPlayers()
	{
		StartCoroutine(HitChecks());
		yield return new WaitForSecondsRealtime(1f);
		for (int i = 0; i < hitVfx.Length; i++)
		{
			hitVfx[i].headvfxOn = false;
			hitVfx[i].sidevfxOn = false;
			hitVfx[i].chestvfxOn = false;
		}
		// stop all perks
		LPerk.StopAllCoroutines();
		RPerk.StopAllCoroutines();
		LMovement.focus = false;
		RMovement.focus = false;
		Time.timeScale = 1f;
		sword1.hit = false;
		sword2.hit = false;
		sword3.hit = false;
		sword4.hit = false;
		playerL.transform.position = LStart.transform.position;
		playerR.transform.position = RStart.transform.position;
		LStamina.ResetStamina();
		RStamina.ResetStamina();	
		//reset stats
		statSetter.LStatSet();
		statSetter.RStatSet();
		LeftLight.SetActive(false);
		RightLight.SetActive(false);
		LAnim.SetFloat("X", 0f);
		RAnim.SetFloat("X", 0f);
		LAnim.SetBool("Resting", false);
		RAnim.SetBool("Resting", false);
		start = false;
		startTimer = 4f;
	}

	public void PlayerVictory(Transform camMoveTo, string trigger,Animator playerAnim,GameObject replayButton,Player player)
	{
		camZoom.animTrigger = trigger;
		camZoom.winnerPosition = camMoveTo;
		camZoom.playerAnim = playerAnim;
		camZoom.firstSelect = replayButton;
		camZoom.player = player;
		DontMove();
		camManager.enabled = false;
		camZoom.enabled = true;

		// turn off everything for victory
		DisableOrEnableScripts(false);
		distanceBar.SetActive(false);
		playerConditions[0].SetActive(false);
		playerConditions[1].SetActive(false);
		playerConditions[2].SetActive(false);
		playerConditions[3].SetActive(false);
	}

	public void HalfTimeEvent()
	{
		// halftime only happens once
		if (LScore == halftimeScore || RScore == halftimeScore) 
		{
			if (!halfTime)
			{
				StartCoroutine(ResetPlayers());
				halftimeAnim.SetFloat("Multiplier",1);
				halfTime = true;
				stop = true;
			}
			else if (statsL.statsDone && statsR.statsDone && stop)
			{						
				halftimeAnim.SetFloat("Multiplier", -1);
				stop = false;
			}
		}
		
	}

	//if player 1 score, player 2 gain morale
	public void LLandHit()
	{
		LHit = true;
		LeftLight.SetActive(true);
		StartCoroutine(ResetPlayers());
	}

	//if player 2 score, player 1 gain morale
	public void RLandHit()
	{
		RHit = true;
		RightLight.SetActive(true);
		StartCoroutine(ResetPlayers());
	}

	IEnumerator HitChecks()
	{
		// only check result after 1 second
		yield return new WaitForSecondsRealtime(1f);
		if (LHit && !RHit)
		{
			LGainPoint();
		}
		else if(RHit && !LHit)
		{
			RGainPoint();
		}
		else if(LHit && RHit)
		{
			if(RController.priority)
			{
				RGainPoint();
			}
			else if(LController.priority)
			{
				LGainPoint();
			}
		}
		
	}

	void LGainPoint()
	{
		LScore += 1;
		RPerk.MoraleIncrease();
	}

	void RGainPoint()
	{
		RScore += 1;
		LPerk.MoraleIncrease();
	}

	public void StartFighting()
	{
		priority = 0;
		DisableOrEnableScripts(true);
	}

	public void DontMove()
	{
		LStamina.staminaVFX.SetActive(false);
		RStamina.staminaVFX.SetActive(false);
		LController.moveSound.SetActive(false);
		LController.dashSound.SetActive(false);
		RController.moveSound.SetActive(false);
		RController.dashSound.SetActive(false);
		priority = 0;
		DisableOrEnableScripts(false);
	}

	void DisableOrEnableScripts(bool state)
	{
		// diable or enable all scripts according to state
		LPerk.enabled = state;
		RPerk.enabled = state;
		LController.enabled = state;
		RController.enabled = state;
		LMovement.enabled = state;
		RMovement.enabled = state;
		LStamina.enabled = state;
		RStamina.enabled = state;
	}

	public void Replay()
	{
		victory = false;
	}
}
