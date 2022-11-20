using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomBarrel : MonoBehaviour
{
    public int barrelCount;
    public TextMeshProUGUI barrelCountText;
    public ParticleSystem explosionVFX;

    public bool isExplosion = false;

    private void Start()
    {
        barrelCountText.text = barrelCount.ToString();
    }
}
