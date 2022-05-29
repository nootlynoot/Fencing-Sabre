using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeWaySys : MonoBehaviour
{
    public GameObject headPress, flankPress, chestPress;

    // Start is called before the first frame update
    void Start()
    {
        headPress.SetActive(false);
        flankPress.SetActive(false);
        chestPress.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("u"))
		{
            headPress.SetActive(true);
		}
        if(headPress.activeInHierarchy == true)
		{
            flankPress.SetActive(false);
            chestPress.SetActive(false);
		}
		if (Input.GetKeyUp("u"))
		{
            headPress.SetActive(false);
        }
        if (Input.GetKeyDown("h"))
        {
            flankPress.SetActive(true);
        }
        if (flankPress.activeInHierarchy == true)
        {
            headPress.SetActive(false);
            chestPress.SetActive(false);
        }
        if (Input.GetKeyUp("h"))
        {
            flankPress.SetActive(false);
        }
        if (Input.GetKeyDown("k"))
        {
            chestPress.SetActive(true);
        }
        if (chestPress.activeInHierarchy == true)
        {
            headPress.SetActive(false);
            flankPress.SetActive(false);
        }
        if (Input.GetKeyUp("k"))
        {
            chestPress.SetActive(false);
        }
    }
}
