using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject custom;
    [SerializeField] private GameObject frontCamera;
    [SerializeField] private GameObject mainCamera;

    public GameObject[] customObjects; 
    private IDictionary<string, GameObject> customObjectsDict = new Dictionary<string, GameObject>();

    private GameObject detectionBar;
    private GameObject enemyEye;
    private PhotonView view;


    void Start() 
    {
        view = GetComponent<PhotonView>();

        custom.GetComponent<GraphicRaycaster>().enabled = view.IsMine;
        detectionBar = UI.transform.Find("DetectionBarAI").gameObject;
        enemyEye = UI.transform.Find("EnemyEye").gameObject;

        detectionBar.SetActive(false);
        enemyEye.SetActive(false);

        // UI.SetActive(view.IsMine);

        foreach(var obj in customObjects)
            customObjectsDict.Add(obj.name, obj);
    }

    public void ToogleCustom(string name, bool state)
    {
        Debug.Log("ToogleCustom " + state);
        view.RPC("ToogleAccessory", RpcTarget.All, name, state);
    }

    [PunRPC]
    public void ToogleAccessory(string name, bool state)
    {
        customObjectsDict[name].SetActive(state);
        // obj.SetActive(state);
    }


    [PunRPC] 
    public void UpdateDetectionBar(bool isActive, float detectionValue = 0)
    {
        if(view.IsMine)
        {
            detectionBar.gameObject.SetActive(isActive);
            if(isActive)
                detectionBar.GetComponent<DetectionBarAI>().DetectionChange(detectionValue);
            else
                detectionBar.GetComponent<DetectionBarAI>().ReinitializeBar();
        }
    }

    [PunRPC] 
    public void UpdateEnemyEye(bool isActive)
    {
        if(view.IsMine)
            enemyEye.SetActive(isActive);
    }

    public void PrepareCustomization() 
    {
        Debug.Log("Activation de la customization");
        Custom(false);
    }
    public void DeactivateCustomization()
    {
        Debug.Log("Desactivation de la customization");
        Custom(true);
    }

    private void Custom(bool activate)
    {
        if(!view) view = GetComponent<PhotonView>();

        if(view.IsMine)
        {
            view.gameObject.GetComponent<PlayerMovement>().enabled = activate;
            view.gameObject.GetComponent<GunScript>().enabled = activate;
            UI.SetActive(activate);
            custom.SetActive(!activate);

            mainCamera.SetActive(activate);
            frontCamera.SetActive(!activate);
            Cursor.visible = !activate;
        }
    }
}
