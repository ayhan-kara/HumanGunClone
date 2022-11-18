using UnityEngine;

public class FireController : MonoBehaviour
{
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
            Fire();
        }
    }
    #endregion

    #region Shooting
    void Fire()
    {
        Bullet bullet = Bullet();
        bullet.gameObject.SetActive(true);

        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = Quaternion.identity;
    }
    #endregion
}
