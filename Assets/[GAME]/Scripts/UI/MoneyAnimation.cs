using UnityEngine;

public class MoneyAnimation : MonoBehaviour
{
    #region Singleton
    public static MoneyAnimation Instance;
    #endregion

    #region Private-Public References
    public Transform moneyUIPosition;

    private Camera mainCamera;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }
    #endregion

    #region GetMoneyPoisition
    public Vector3 GetMoneyUIPosisiton(Vector3 target)
    {
        Vector3 moneyUiPosition = moneyUIPosition.position;
        moneyUiPosition.z = (target - mainCamera.transform.position).z;

        Vector3 result = mainCamera.ScreenToWorldPoint(moneyUiPosition);

        return result;

    }
    #endregion
}
