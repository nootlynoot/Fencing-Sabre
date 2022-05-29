using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSetter : MonoBehaviour
{
	public StatSelect LStat, RStat;
    // Start is called before the first frame update
    void Start()
    {
		LStatSet();
		RStatSet();
	}

    public void LStatSet()
	{
		// get the sets so when halftime, players can adjust or stick with thier decision
		LStat.upgrades[0].fillAmount = PlayerPrefs.GetFloat(LStat.saveStats[0]);
		LStat.upgrades[1].fillAmount = PlayerPrefs.GetFloat(LStat.saveStats[1]);
		LStat.upgrades[2].fillAmount = PlayerPrefs.GetFloat(LStat.saveStats[2]);
	}

	public void RStatSet()
	{
		// get the sets so when halftime, players can adjust or stick with thier decision
		RStat.upgrades[0].fillAmount = PlayerPrefs.GetFloat(RStat.saveStats[0]);
		RStat.upgrades[1].fillAmount = PlayerPrefs.GetFloat(RStat.saveStats[1]);
		RStat.upgrades[2].fillAmount = PlayerPrefs.GetFloat(RStat.saveStats[2]);
	}
}
