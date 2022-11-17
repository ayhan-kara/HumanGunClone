using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	#region Singleton
	public static InputManager Instance;
	#endregion

	#region Public Variables
	public bool isStarted = false;
	public Animator playerAnim;
	#endregion

	#region Monobehaviour
	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartGame();
		}
	}

	#endregion

	#region CheckStart
	void StartGame()
	{
        isStarted = true;
		playerAnim.SetBool("IsRunning", true);
    }
	#endregion
}
