using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPlayers : MonoBehaviour
{
    #region Singleton
    public static CheckPlayers Instance;
    #endregion

    #region Private Variables, Boolean
    public int playerCount = 0;

    public bool isPistol = false;
    public bool isShootgun = false;
    #endregion

    #region References
    [Header("Player Animator")]
    [SerializeField] Animator anim;

    [Header ("Guns Positions")]
    [Space]
    [SerializeField] List<Transform> pistolRefPositions = new List<Transform>();

    [Space]
    [SerializeField] List<Transform> shootgunRefPositions = new List<Transform>();

    [Header ("Players")]
    [Space]
    [SerializeField] List<Transform> gunsPlayers = new List<Transform>();

    [Space]
    [SerializeField] List<Transform> newShootgunPlayers = new List<Transform>();

    [Header ("Materials")]
    [SerializeField] Material greyPlayerMaterial;
    [SerializeField] Material redPlayerMaterial;
    [SerializeField] Material bluePlayerMaterial;
    [SerializeField] Material yellowPlayerMaterial;
    #endregion

    #region Pooler
    [Header("Pooler")]
    [SerializeField] Pooler poolPlayers;
    private Players Players()
    {
        return poolPlayers.GetGo<Players>();
    }
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isPistol || isShootgun)
        {
            SetPositions();
            SetRotations();
            SetMaterial();
            SetAnimations();
        }
    }
    #endregion

    #region Trigger-Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (playerCount >= 1)
            {
                playerCount--;
                Transform lastChild = transform.GetChild(transform.childCount - 1);
                lastChild.parent = null;
                gunsPlayers.Remove(lastChild);
                lastChild.position = Vector3.Lerp(lastChild.position, new Vector3(lastChild.position.x, lastChild.position.y + 2, lastChild.position.z + 2), Time.deltaTime * 5);
                lastChild.AddComponent<Rigidbody>();
            }
            else
            {
                Debug.Break();
                Debug.LogError("Fail");
                anim.Play("Fail");
                //fail bool eklenecek
            }
        }
        if (other.CompareTag("UpgradeDoor"))
        {
            playerCount += other.GetComponent<UpgradeDoor>().doorCount;
            SetPool(other.transform);
        }
        if (other.CompareTag("DecreaseDoor"))
        {
            if (playerCount >= 1)
            {
                playerCount--;
                Transform lastChild = transform.GetChild(transform.childCount - 1);
                lastChild.parent = null;
                gunsPlayers.Remove(lastChild);
                lastChild.position = Vector3.Lerp(lastChild.position, new Vector3(lastChild.position.x, lastChild.position.y + 2, lastChild.position.z + 2), Time.deltaTime * 5);
                lastChild.AddComponent<Rigidbody>();
            }
            else
            {
                Debug.Break();
                Debug.LogError("Fail");
                anim.Play("Fail");
                //fail bool eklenecek
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectiblePlayer"))
        {
            other.transform.parent = transform;
            playerCount++;
            anim.SetBool("GunPos", true);
            if (playerCount <= 3)
            {
                isPistol = true;
                Destroy(other.gameObject.GetComponent<BoxCollider>());
                Destroy(other.gameObject.GetComponent<Rigidbody>());
                other.gameObject.GetComponent<Animator>().SetBool("Pos1", true);
                gunsPlayers.Add(other.transform);
            }
            else if (playerCount >= 4)
            {
                if (isPistol)
                    isPistol = false;
                isShootgun = true;
                gunsPlayers.Add(other.transform);
                newShootgunPlayers.Add(other.transform);
                Destroy(other.gameObject.GetComponent<BoxCollider>());
                Destroy(other.gameObject.GetComponent<Rigidbody>());
                other.gameObject.GetComponent<Animator>().SetBool("Pos1", true);
            }
        }
    }
    #endregion

    #region Players Adjustments
    void SetPositions()
    {
        if (isPistol)
        {
            switch (playerCount)
            {
                case 1:
                    gunsPlayers[0].position = Vector3.Lerp(gunsPlayers[0].position, pistolRefPositions[0].position, Time.deltaTime * 10f);
                    break;
                case 2:
                    gunsPlayers[1].position = Vector3.Lerp(gunsPlayers[1].position, pistolRefPositions[1].position, Time.deltaTime * 10f);
                    break;
                case 3:
                    gunsPlayers[2].position = Vector3.Lerp(gunsPlayers[2].position, pistolRefPositions[2].position, Time.deltaTime * 10f);
                    break;
            }
        }
        else if (isShootgun)
        {
            gunsPlayers[2].position = Vector3.Lerp(gunsPlayers[2].position, shootgunRefPositions[1].position, Time.deltaTime * 10f);
            gunsPlayers[3].position = Vector3.Lerp(gunsPlayers[3].position, shootgunRefPositions[0].position, Time.deltaTime * 10f);
            gunsPlayers[1].position = Vector3.Lerp(gunsPlayers[1].position, shootgunRefPositions[2].position, Time.deltaTime * 10f);

            switch (playerCount)
            {
                case 5:
                    gunsPlayers[4].position = Vector3.Lerp(gunsPlayers[4].position, shootgunRefPositions[3].position, Time.deltaTime * 10f);
                    break;
                case 6:
                    gunsPlayers[5].position = Vector3.Lerp(gunsPlayers[5].position, shootgunRefPositions[4].position, Time.deltaTime * 10f);
                    break;
            }
        }
    }

    void SetRotations()
    {
        if (isPistol)
        {
            switch (playerCount)
            {
                case 1:
                    gunsPlayers[0].rotation = Quaternion.Lerp(gunsPlayers[0].rotation, pistolRefPositions[0].rotation, Time.deltaTime * 10f);
                    break;
                case 2:
                    gunsPlayers[1].rotation = Quaternion.Lerp(gunsPlayers[1].rotation, pistolRefPositions[1].rotation, Time.deltaTime * 10f);
                    break;
                case 3:
                    gunsPlayers[2].rotation = Quaternion.Lerp(gunsPlayers[2].rotation, pistolRefPositions[2].rotation, Time.deltaTime * 10f);
                    break;
            }
        }
        else if (isShootgun)
        {
            gunsPlayers[3].rotation = Quaternion.Lerp(gunsPlayers[3].rotation, shootgunRefPositions[0].rotation, Time.deltaTime * 10f);
            gunsPlayers[1].rotation = Quaternion.Lerp(gunsPlayers[1].rotation, shootgunRefPositions[2].rotation, Time.deltaTime * 10f);

            switch (playerCount)
            {
                case 5:
                    gunsPlayers[4].rotation = Quaternion.Lerp(gunsPlayers[4].rotation, shootgunRefPositions[3].rotation, Time.deltaTime * 10f);
                    break;
                case 6:
                    gunsPlayers[5].rotation = Quaternion.Lerp(gunsPlayers[5].rotation, shootgunRefPositions[4].rotation, Time.deltaTime * 10f);
                    break;
            }
        }
    }

    void SetMaterial()
    {
        if (isPistol)
        {
            switch (playerCount)
            {
                case 1:
                    gunsPlayers[0].GetChild(1).GetComponent<Renderer>().material = greyPlayerMaterial;
                    break;
                case 2:
                    gunsPlayers[1].GetChild(1).GetComponent<Renderer>().material = redPlayerMaterial;
                    break;
                case 3:
                    gunsPlayers[2].GetChild(1).GetComponent<Renderer>().material = redPlayerMaterial;
                    break;
            }
        }
        else if (isShootgun)
        {
            if (playerCount <= 4)
            {
                transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material = yellowPlayerMaterial;
                gunsPlayers[0].GetChild(1).GetComponent<Renderer>().material = greyPlayerMaterial;
                gunsPlayers[1].GetChild(1).GetComponent<Renderer>().material = yellowPlayerMaterial;
                gunsPlayers[2].GetChild(1).GetComponent<Renderer>().material = greyPlayerMaterial;
                newShootgunPlayers[0].GetChild(1).GetComponent<Renderer>().material = greyPlayerMaterial;
            }
            else if (playerCount > 4)
            {
                switch (playerCount)
                {
                    case 4:
                        newShootgunPlayers[0].GetChild(1).GetComponent<Renderer>().material = yellowPlayerMaterial;
                        break;
                    case 5:
                        newShootgunPlayers[1].GetChild(1).GetComponent<Renderer>().material = greyPlayerMaterial;
                        break;
                    case 6:
                        newShootgunPlayers[2].GetChild(1).GetComponent<Renderer>().material = yellowPlayerMaterial;
                        break;
                }
            }
        }
    }

    void SetAnimations()
    {
        if (isPistol)
        {

        }
        else if (isShootgun)
        {
            gunsPlayers[1].GetComponent<Animator>().SetBool("Pos2", true);
            if (newShootgunPlayers.Count == 3)
            {
                if (newShootgunPlayers[2].GetComponent<Animator>().GetBool("Pos2") != true)
                {
                    newShootgunPlayers[2].GetComponent<Animator>().SetBool("Pos2", true);
                    newShootgunPlayers[2].GetComponent<Animator>().SetBool("Pos3", true);
                }
            }
        }
    }

    void SetPool(Transform other)
    {
        Players players = Players();
        players.gameObject.SetActive(true);

        players.transform.position = other.position;
        players.transform.rotation = Quaternion.identity;
        players.transform.parent = transform;

        players.GetComponent<Animator>().SetBool("Pos1", true);

        if (playerCount <= 3)
        {
            gunsPlayers.Add(players.transform);
        }
        else if (playerCount >= 4)
        {
            gunsPlayers.Add(players.transform);
            newShootgunPlayers.Add(players.transform);
        }
    }
    #endregion


}
