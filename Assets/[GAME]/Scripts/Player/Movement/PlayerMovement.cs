using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    public static PlayerMovement Instance;
    #endregion

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

    float timer = 0.0f;
    #endregion


    #region Monobehaviour

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        screenWidth = Screen.width;
    }

    private void Update()
    {
        bool isPointerOverGameobject = IsPointerOverUIObject();
        if (Input.GetMouseButtonDown(0) && !isPointerOverGameobject)
        {
            InputManager.Instance.StartGame();
        }
        if (!InputManager.Instance.isStarted)
            return;
        timer += Time.deltaTime;
        if (timer >= .1f)
        {
            MovePlayer();
        }
    }
    #endregion

    #region Movement
    void MovePlayer()
    {
        if (UIManagement.instance.isFinished)
            if (UIManagement.instance.isFail)
                return;
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

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = GetPointerPosition();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private Vector2 GetPointerPosition()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
}
