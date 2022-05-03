using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Transform otherPlayerTransform;
    public string otherPlayerTag;
    public GameObject cursorCamera;

    //private void Awake()
    //{
    //    otherPlayerTransform = GameObject.FindGameObjectWithTag(otherPlayerTag).GetComponent<Transform>();
    //}
    //void Update()
    //{
    //    if(otherPlayerTransform == null)
    //        otherPlayerTransform = GameObject.FindGameObjectWithTag(otherPlayerTag).GetComponent<Transform>();

    //    float playerDistance = Vector3.Distance(transform.position, otherPlayerTransform.position);
    //    Debug.DrawRay(transform.position, otherPlayerTransform.position - transform.position, Color.blue);
    //    Ray rayForward = new Ray(transform.position, otherPlayerTransform.position - transform.position);

    //    if (Physics.Raycast(rayForward, out RaycastHit hit, playerDistance) && (hit.collider.tag == otherPlayerTag))
    //    {
    //        cursorCamera.SetActive(false);
    //        Debug.Log("DEACTIVATED CURSORCAMERA " + cursorCamera.activeSelf);
    //    }
    //    else
    //    {
    //        cursorCamera.SetActive(true);
    //        Debug.Log("ACTIVATED CURSORCAMERA " + cursorCamera.activeSelf);
    //    }
    //}
}
