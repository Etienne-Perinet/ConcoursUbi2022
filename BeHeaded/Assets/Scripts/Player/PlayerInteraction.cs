using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDist = 4f;
    [SerializeField] private Camera cam;

    protected EnemyAI enemyAI;
    private TMPro.TextMeshProUGUI interactiveTxt;
    private Ray ray;
    private RaycastHit hit;
    private bool isHit = false;

    void Awake()
    {
        interactiveTxt = GameObject.Find("Canvas/TextBubble").GetComponent<TextMeshProUGUI>();
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    void Update()
    {
        ray = cam.ViewportPointToRay(Vector3.one/2f);
        isHit = false;

        if(Physics.Raycast(ray, out hit, interactionDist))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null) 
            {
                HandleInteraction(interactable, hit);
                interactiveTxt.text = interactable.GetDescription();
                isHit = true;
            }
        }

        if(!isHit)
            interactiveTxt.text = "";

        // If the players tried to find the object et the same time
        if(!enemyAI) GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    public abstract void HandleInteraction(Interactable interactable, RaycastHit hit);
}
