using UnityEngine;
using TMPro;

public class Barrel : MonoBehaviour
{

    #region References
    [SerializeField] TextMeshProUGUI barrelText;
    [SerializeField] int barrelCount;
    #endregion

    #region Monobehaviour
    private void Start()
    {
        barrelText.text = barrelCount.ToString();
    }
    #endregion

}
