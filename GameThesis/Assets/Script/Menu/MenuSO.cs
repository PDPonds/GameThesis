using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Menu")]
public class MenuSO : ScriptableObject
{
    public int i_requiredLevel;
    public Image img_menuIcon;
    public Vector3 t_cookingPos;
}

[Serializable]
public class MenuState
{
    public MenuSO menu;
    public bool processStatus;
}
