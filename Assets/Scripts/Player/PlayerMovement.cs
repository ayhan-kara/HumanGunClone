using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region Public-Private Variables
    private float firstValue;
    private float currentValue;
    private float screenWidth;
    private float lastSumValue;
    private bool handsUp;

	public float forwardSpeed;
    public float sumValue;
    public float sensivity = 100f;
    public float X_clamp;
    #endregion


    #region Monobehaviour
    private void Start()
    {
        screenWidth = Screen.width;
    }

    private void Update()
    {
        MovePlayer();
    }
    #endregion

    #region Movement
    void MovePlayer()
    {
        //if (!UIManagement.instance.isStarted)
        //    return;
        //if (!GameManager.instance.isAlive)
        //    return;

        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        if (Input.GetMouseButton(0))
        {
            if (handsUp)
            {
                firstValue = Input.mousePosition.x;
                handsUp = false;
            }

            currentValue = Input.mousePosition.x;

            sumValue = currentValue - firstValue;
            sumValue /= screenWidth;

            sumValue *= sensivity;
            sumValue += lastSumValue;
        }
        else
        {
            lastSumValue = sumValue;
            handsUp = true;
        }

        if (sumValue > X_clamp)
        {
            sumValue = X_clamp;
            lastSumValue = X_clamp;
            handsUp = true;
        }
        else if (sumValue < -X_clamp)
        {
            sumValue = -X_clamp;
            lastSumValue = -X_clamp;
            handsUp = true;
        }
    }
    #endregion
}
