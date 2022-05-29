using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundSystem : MonoBehaviour
{
	public static SoundSystem audioScript;
	public AudioMixerSnapshot normal;
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot victory;
	public AudioSource mainMenu;

	public bool isPaused;
	public bool playerWin;

    // Start is called before the first frame update
    void Awake()
    {
		//disable mouse
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		DontDestroyOnLoad(this);
		if (System.Object.ReferenceEquals(audioScript, null))
		{
			audioScript = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Update()
	{
		if (isPaused)
		{
			paused.TransitionTo(1f);
		}
		else if (playerWin)
		{
			victory.TransitionTo(1f);
		}
		else if (!isPaused && !isPaused)
		{
			normal.TransitionTo(1f);
		}

		if (SceneManager.GetActiveScene().buildIndex == 1 && !mainMenu.isPlaying)
		{
			mainMenu.Play();
		}
		else if(SceneManager.GetActiveScene().buildIndex == 2)
		{
			mainMenu.Stop();
		}
	}
}
