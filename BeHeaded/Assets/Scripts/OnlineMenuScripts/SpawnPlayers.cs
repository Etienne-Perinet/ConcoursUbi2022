using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SpawnPlayers : MonoBehaviour
{

    // Ce script permet le spawn des joueurs selon la page de choix
    // Les proprietes sont normallement dans PhotonNetwork.LocalPlayer.CustomProperties
    // A voir le scipt CreateAndJoinRooms et TestChoice

    public GameObject fishPrefab;
    public GameObject hamsterPrefab;
    public GameObject enemyPrefab;
    public bool spawnEnemy;

    public Transform fishSpawn;
    public Transform hamsterSpawn;
    public Transform enemySpawn; 

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    private int playerCount;

    [SerializeField]
    private GameObject fishObject;
    private GameObject hamsterObject;
    private GameObject enemyObject;

    [SerializeField]
    private GameObject electricDoor;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Stop("MenuMusic");
        FindObjectOfType<AudioManager>().Play("GameplayLoop");
        Debug.Log("Player instantiation");
        Debug.Log("Prefab name" + (string)PhotonNetwork.LocalPlayer.CustomProperties["name"]);
        string toSpawn = (string)PhotonNetwork.LocalPlayer.CustomProperties["name"];
        //Debug.Log(GameObject.FindWithTag("FishPlayer"));
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        if (toSpawn == "FishChoice")
        {
            SpawnFishPlayer(fishSpawn.position);
            Debug.Log("FishPlayer SPAWN");
            
        } 
        else if(toSpawn == "HamsterChoice")
        {
            SpawnHamsterPlayer(hamsterSpawn.position);
            Debug.Log("HamsterPlayer SPAWN");
        }

        // Deadline spawn if 2 players
        // playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(PhotonNetwork.IsMasterClient)
            Invoke("SpawnEnemyPlayer", 5);

        if (!electricDoor)
        {
            electricDoor = GameObject.FindGameObjectsWithTag("ElectricalDoor")[1];
            //electricDoor.GetComponent<ElectricalDoor>().Unlock(new Vector3());
        }

    }

    private void SpawnEnemyPlayer()
    {
        if (spawnEnemy)
        {
            enemyObject = PhotonNetwork.Instantiate(enemyPrefab.name, enemySpawn.position, Quaternion.identity);
            Invoke("updateAllHooks", 1);
        }
    }


    private void updateAllHooks()
    {
        Debug.Log("fish hooks");
        if(!fishObject) fishObject = GameObject.FindGameObjectWithTag("FishPlayer");
        fishObject.GetComponent<HamsterInteraction>().updateHooks();

        Invoke("otherHook", 1);

        
    }

    private void otherHook()
    {
        Debug.Log("hamster hooks");
        if(!hamsterObject) hamsterObject = GameObject.FindGameObjectWithTag("HamsterPlayer");
        hamsterObject.GetComponent<HamsterInteraction>().updateHooks();
    }

    private void SpawnFishPlayer(Vector3 position)
    {
        fishObject = PhotonNetwork.Instantiate(fishPrefab.name, position, Quaternion.identity);
        fishObject.GetComponent<PlayerUI>().PrepareCustomization();

        // GameObject.FindGameObjectWithTag("FishPlayer").GetComponent<CustomPlayer>().PrepareCustomization();
        // piste : https://forum.unity.com/threads/photon-question-about-how-to-access-the-gameobject-that-has-photon-view-on-it.971142/
        // fishPrefab.GetComponent<CustomPlayer>().PrepareCustomization();
        // fishPrefab.GetPhotonView().GetComponent<CustomPlayer>().PrepareCustomization();


       // if (!fish.GetPhotonView().IsMine)
        //    return;
        //fish.transform.Find("HamsterCamera").gameObject.GetComponent<Camera>().enabled = true;
    }

    private void SpawnHamsterPlayer(Vector3 position)
    {
        hamsterObject = PhotonNetwork.Instantiate(hamsterPrefab.name, position, Quaternion.identity);
        hamsterObject.GetComponent<PlayerUI>().PrepareCustomization();
        //if (!hamster.GetPhotonView().IsMine)
        //    return;
        //hamster.transform.Find("HamsterCamera").gameObject.GetComponent<Camera>().enabled = true;
    }


}
