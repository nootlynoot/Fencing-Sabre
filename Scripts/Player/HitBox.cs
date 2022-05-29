using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	public LevelManager levelManager;
	public PlayerController currentController;
	public GameObject player;
	public bool hit;
	public GameObject parryVFX;

	private void OnTriggerEnter(Collider other)
	{
		PlayerController otherController = other.gameObject.GetComponent<PlayerController>();
		GameObject otherPlayer = other.gameObject;

		if (gameObject.CompareTag("SwordCol1") && !hit)
		{
			//ATTACKS
			if (player.CompareTag("headatk") && !otherController.headpry && otherPlayer.name != player.name)
			{
				//print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.headvfxOn = true;
				hit = true;
				levelManager.LLandHit();
			}
			else if (player.CompareTag("sideatk") && !otherController.sidepry && otherPlayer.name != player.name)
			{
				//print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.chestvfxOn = true;
				hit = true;
				levelManager.LLandHit();
			}
			else if (player.CompareTag("chestatk") && !otherController.chestpry && otherPlayer.name != player.name)
			{
				//print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.sidevfxOn = true;
				hit = true;
				levelManager.LLandHit();
			}
			else if (currentController.headpry && otherPlayer.CompareTag("headatk"))
			{
				print("yey");
				StartCoroutine(ParryVFX());
			}
			else if (currentController.sidepry && otherPlayer.CompareTag("sideatk"))
			{
				print("yey");
				StartCoroutine(ParryVFX());
			}
			else if (currentController.chestpry && otherPlayer.CompareTag("chestatk"))
			{
				print("yey");
				StartCoroutine(ParryVFX());
			}
			else
			{
				return;
			}
		}

		if (gameObject.CompareTag("SwordCol2") && !hit)
		{
			//ATTACKS
			if (player.CompareTag("headatk") && !otherController.headpry && otherPlayer.name != player.name)
			{
				print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.headvfxOn = true;
				hit = true;
				levelManager.RLandHit();
			}
			else if (player.CompareTag("sideatk") && !otherController.sidepry && otherPlayer.name != player.name)
			{
				print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.chestvfxOn = true;
				hit = true;
				levelManager.RLandHit();
			}
			else if (player.CompareTag("chestatk") && !otherController.chestpry && otherPlayer.name != player.name)
			{
				print(other.gameObject.name);
				onHitVFX vfx = other.gameObject.GetComponentInChildren<onHitVFX>();
				vfx.sidevfxOn = true;
				hit = true;
				levelManager.RLandHit();
			}
			else if (currentController.headpry && otherPlayer.CompareTag("headatk"))
			{
				print(gameObject.name);
				//playsound and on sparks vfx
				return;
			}
			else if (currentController.sidepry && otherPlayer.CompareTag("sideatk"))
			{
				print(gameObject.name);
				//playsound and on sparks vfx
				return;
			}
			else if (currentController.chestpry && otherPlayer.CompareTag("chestatk"))
			{
				print(gameObject.name);
				//playsound and on sparks vfx
				return;
			}
			else
			{
				return;
			}
		}
	}
	public IEnumerator ParryVFX()
	{
		parryVFX.SetActive(true);
		yield return new WaitForSeconds(1f);
		parryVFX.SetActive(false);
	}
}
