using UnityEngine;

public class FireController : MonoBehaviour
{
    #region Public-Private Variables
    public float gunDelay;

    private float timer = 0.0f;
    #endregion

    #region References
    [SerializeField] Transform firePosition;
    #endregion

    #region Pooler
    [SerializeField] Pooler poolerBullet;

    private Bullet Bullet()
    {
        return poolerBullet.GetGo<Bullet>();
    }
    #endregion

    #region Trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Fire(gunDelay);
        }
        if (other.CompareTag("BombBarrel"))
        {
            Fire(gunDelay);
        }
    }
    #endregion

    #region Shooting
    void Fire(float gunDelay)
    {
        if (PlayerMovement.Instance.forwardSpeed == 0)
            return;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Bullet bullet = Bullet();
            bullet.gameObject.SetActive(true);

            bullet.transform.position = firePosition.position;
            bullet.transform.rotation = Quaternion.identity;

            timer = gunDelay;
        }
    }
    #endregion
}
