using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition,
            new Vector3(playerMovement.sumValue, transform.localPosition.y, transform.localPosition.z),
            10f * Time.deltaTime);
    }
}
