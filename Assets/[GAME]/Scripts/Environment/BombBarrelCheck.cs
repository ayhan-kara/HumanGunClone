using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombBarrelCheck : MonoBehaviour
{
    [SerializeField] BoomBarrel boomBarrel;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            if (boomBarrel.isExplosion)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
