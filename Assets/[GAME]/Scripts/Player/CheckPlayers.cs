using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayers : MonoBehaviour
{
    public Animator anim;
    public List<Transform> pistolRefPositions = new List<Transform>();
    public List<Transform> newPistolPlayers = new List<Transform>();

    public List<Transform> shootgunRefPositions = new List<Transform>();
    public List<Transform> newShootgunPlayers = new List<Transform>();

    private int playerCount = 0;


    bool isPistol = false;
    bool isShootgun = false;

    private void Update()
    {
        if (isPistol)
        {
            SetPositions();
            SetRotations();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectiblePlayer"))
        {
            other.transform.parent = transform;
            playerCount++;
            anim.SetBool("GunPos", true);
            isPistol = true;
            if (playerCount <= 4)
            {
                Destroy(other.GetComponent<BoxCollider>());
                other.GetComponent<Animator>().SetBool("Pos1", true);
                newPistolPlayers.Add(other.transform);
            }
            else if (playerCount > 4)
            {
                isPistol = false;
                isShootgun = true;
                //other.GetComponent<Animator>().SetBool("Pos2", true);
                newShootgunPlayers.Add(other.transform);
            }
        }
    }


    void SetPositions()
    {
        switch (playerCount)
        {
            case 1:
                newPistolPlayers[0].position = Vector3.Lerp(newPistolPlayers[0].position, pistolRefPositions[0].position, Time.deltaTime * 10f);
                break;
            case 2:
                newPistolPlayers[1].position = Vector3.Lerp(newPistolPlayers[1].position, pistolRefPositions[1].position, Time.deltaTime * 10f);
                break;
            case 3:
                newPistolPlayers[2].position = Vector3.Lerp(newPistolPlayers[2].position, pistolRefPositions[2].position, Time.deltaTime * 10f);
                break;
            case 4:
                newPistolPlayers[3].position = Vector3.Lerp(newPistolPlayers[3].position, pistolRefPositions[3].position, Time.deltaTime * 10f);
                break;
        }
    }

    void SetRotations()
    {
        switch (playerCount)
        {
            case 1:
                newPistolPlayers[0].rotation = Quaternion.Lerp(newPistolPlayers[0].rotation, pistolRefPositions[0].rotation, Time.deltaTime * 10f);
                break;
            case 2:
                newPistolPlayers[1].rotation = Quaternion.Lerp(newPistolPlayers[1].rotation, pistolRefPositions[1].rotation, Time.deltaTime * 10f);
                break;
            case 3:
                newPistolPlayers[2].rotation = Quaternion.Lerp(newPistolPlayers[2].rotation, pistolRefPositions[2].rotation, Time.deltaTime * 10f);
                break;
            case 4:
                newPistolPlayers[3].rotation = Quaternion.Lerp(newPistolPlayers[3].rotation, pistolRefPositions[3].rotation, Time.deltaTime * 10f);
                break;
        }
    }
}
