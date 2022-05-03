using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AudioCharacterManager : MonoBehaviour
{
    [SerializeField]
    public AudioListener audioListener;
    public PhotonView view;
    void Awake()
    {
        audioListener.enabled = view.IsMine;
        // audioListener.GetComponent<AudioListener>().enabled = true;
    }
}
