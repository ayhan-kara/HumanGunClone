using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Public Variables
    public float buletSpeed;
    #endregion

    #region Pooler
    [Header("Pooler")]
    [SerializeField] Pooler poolerexplosionVFX;

    private ExplosionVFX ExplosionVFX()
    {
        return poolerexplosionVFX.GetGo<ExplosionVFX>();
    }
    #endregion

    #region Monobehaviour
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * buletSpeed;
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Destroy(other.gameObject);
            Explosion(other.transform); 
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Range"))
        {
            gameObject.SetActive(false);
        }
    }
    #endregion


    #region Explosion
    void Explosion(Transform other)
    {
        ExplosionVFX explosion = ExplosionVFX();
        explosion.gameObject.SetActive(true);

        explosion.transform.position = other.position;
        explosion.transform.rotation = Quaternion.identity;

        explosion.GetComponent<ParticleSystem>().Play();
    }
    #endregion
}
