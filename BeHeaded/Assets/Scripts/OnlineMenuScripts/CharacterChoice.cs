using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;

public class CharacterChoice : MonoBehaviour
{
    public ToggleGroup characterChoice;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject warningText;

    public bool hamsterReady = false;
    public bool fishReady = false;
    private PhotonView view;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();


    private void Start()
    {
        playerProperties["name"] = currentSelection.name;
        view = GetComponent<PhotonView>();
        warningText.SetActive(false);
    }

    private void Update() 
    {
        playButton.SetActive(fishReady && hamsterReady && view.IsMine && PhotonNetwork.IsMasterClient);
    }

    public Toggle currentSelection
    {
        get { return characterChoice.ActiveToggles().FirstOrDefault(); }
    }

    public void OnToggleClick()
    {
        playerProperties["name"] = currentSelection.name;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public void NextScene()
    {
        if(view.IsMine)
            PhotonNetwork.LoadLevel(sceneToLoad);
    }

    public void ReadyUp()
    { 
        if((currentSelection.name == "FishChoice" && !fishReady) || (currentSelection.name == "HamsterChoice" && !hamsterReady))
        {
            view.RPC("PlayerReady", RpcTarget.All, currentSelection.name);
            if(warningText.activeSelf) warningText.SetActive(false);
        }
        else 
            warningText.SetActive(true);


    }

    [PunRPC]
    public void PlayerReady(string choice) 
    {
        if(choice == "FishChoice")
            fishReady = true;
        else if(choice == "HamsterChoice")
            hamsterReady = true;
    }

}
