using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayer : MonoBehaviour
{
    public PlayerUI playerUI;

    public void ToggleGameObject(bool tag) => playerUI.ToogleCustom(gameObject.name, tag);
}
