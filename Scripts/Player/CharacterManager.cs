using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
	public string savedCharcterParam;
	public GameObject mordenFencer, fantasyFencer;
	public SkinnedMeshRenderer mordenMesh, fantasyMesh;
	public Material[] fencerSkins;
	public Animator mordenAnim,fantasyAnim;
	public PlayerController playerController;
	public playerMovement playerMovement;
	public Stamina stamina;
	
	
	int chosenFencer;
    // Start is called before the first frame update
    void Awake()
    {
		chosenFencer = PlayerPrefs.GetInt(savedCharcterParam);

		//white=1,red=2,Fred=3,Fblue=4,blue=5
		switch (chosenFencer)
		{
			case 1:
				playerController.anim = mordenAnim;
				playerMovement.anim = mordenAnim;
				stamina.anim = mordenAnim;
				mordenMesh.material = fencerSkins[chosenFencer];
				mordenFencer.SetActive(true);
			break;
			case 2:
				playerController.anim = mordenAnim;
				playerMovement.anim = mordenAnim;
				stamina.anim = mordenAnim;
				mordenMesh.material = fencerSkins[chosenFencer];
				mordenFencer.SetActive(true);
			break;
			case 3:
				playerController.anim = fantasyAnim;
				playerMovement.anim = fantasyAnim;
				stamina.anim = fantasyAnim;
				fantasyMesh.material = fencerSkins[chosenFencer];
				fantasyFencer.SetActive(true);
			break;
			case 4:
				playerController.anim = fantasyAnim;
				playerMovement.anim = fantasyAnim;
				stamina.anim = fantasyAnim;
				fantasyMesh.material = fencerSkins[chosenFencer];
				fantasyFencer.SetActive(true);
			break;
			case 5:
				playerController.anim = mordenAnim;
				playerMovement.anim = mordenAnim;
				stamina.anim = mordenAnim;
				mordenMesh.material = fencerSkins[chosenFencer];
				mordenFencer.SetActive(true);
			break;
		}
		
    }
}
