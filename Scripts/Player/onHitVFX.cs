using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHitVFX : MonoBehaviour
{
    public bool headvfxOn = false, sidevfxOn = false, chestvfxOn = false;
	[Header("PUT THIS PLAYER HIT VFX")]
    public GameObject[] headVFX, chestVFX, sideVFX;

    // Update is called once per frame
    void Update()
    {
		if (headvfxOn)
		{
			for (int i = 0; i < headVFX.Length; i++)
			{
				headVFX[i].SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < headVFX.Length; i++)
			{
				headVFX[i].SetActive(false);
			}
		}

		if (sidevfxOn)
		{
			for (int i = 0; i < sideVFX.Length; i++)
			{
				sideVFX[i].SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < sideVFX.Length; i++)
			{
				sideVFX[i].SetActive(false);
			}
		}

		if (chestvfxOn)
		{
			for (int i = 0; i < chestVFX.Length; i++)
			{
				chestVFX[i].SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < chestVFX.Length; i++)
			{
				chestVFX[i].SetActive(false);
			}
		}
	}
}
