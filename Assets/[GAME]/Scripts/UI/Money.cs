using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    #region Boolean
    public bool isCollected = false;
    #endregion

    #region Monobehaviour
    private void FixedUpdate()
    {
        if (isCollected)
        {
            Vector3 targetPosition = MoneyAnimation.Instance.GetMoneyUIPosisiton(transform.position);
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15);
            }
            else
            {
                Destroy(gameObject);
                //add money
            }
        }
    }
    #endregion

    #region Collect
    public void SetCollect()
    {
        isCollected = true;
    }
    #endregion
}
