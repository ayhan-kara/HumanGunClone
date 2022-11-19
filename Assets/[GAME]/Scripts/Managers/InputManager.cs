using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

	#endregion

	#region CheckStart
	public void StartGame()
	{
		UIManagement.instance.startPanel.SetActive(false);
		UIManagement.instance.gameInPanel.SetActive(true);
        isStarted = true;
		playerAnim.SetBool("IsRunning", true);
    }
    #endregion
}
