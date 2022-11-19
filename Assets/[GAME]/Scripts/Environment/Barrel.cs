using UnityEngine;
using TMPro;

public class Barrel : MonoBehaviour
{

    #region References
    public TextMeshProUGUI barrelText;
    public int barrelCount;
    #endregion

    #region Monobehaviour
    private void Start()
    {
        barrelText.text = barrelCount.ToString();
    }
    #endregion

}
