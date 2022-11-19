using TMPro;
using UnityEngine;

public class DecreaseDoor : MonoBehaviour
{
    #region References
    [SerializeField] TextMeshProUGUI doorText;
    public int doorCount;
    #endregion

    #region Monobehaviour
    private void Start()
    {
        doorText.text = "- " + doorCount.ToString();
    }
    #endregion
}
