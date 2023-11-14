using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuHandler : MonoBehaviour
{
    public List<MenuStatus> mainDish_Status = new List<MenuStatus>();
    public List<MenuStatus> drinks_Status = new List<MenuStatus>();

    void Start()
    {
        ActivateDishMenu(0);
        ActivateDrinksMenu(0);

        MenuBoardUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateDishMenu(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateDishMenu(2);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ActivateDrinksMenu(1);
        }
    }

    public int RandomDish()
    {
        while (true)
        {
            int index = UnityEngine.Random.Range(0, mainDish_Status.Count);
            if (mainDish_Status[index].status == true)
            {
                return index;
            }
        }
    }

    public int RandomDrink()
    {
        while (true)
        {
            int index = UnityEngine.Random.Range(0, drinks_Status.Count);
            if (drinks_Status[index].status == true)
            {
                return index;
            }
        }
    }

    public void MenuBoardUpdate()
    {
        for (int i = 0; i < mainDish_Status.Count; i++)
        {
            if (mainDish_Status[i].status == true)
            {
                mainDish_Status[i].menuGameobject.SetActive(true);
            }
            else { mainDish_Status[i].menuGameobject.SetActive(false); }
        }

        for (int i = 0; i < drinks_Status.Count; i++)
        {
            if (drinks_Status[i].status == true)
            {
                drinks_Status[i].menuGameobject.SetActive(true);
            }
            else { drinks_Status[i].menuGameobject.SetActive(false); }
        }
    }

    public void ActivateDishMenu(int menuIndex)
    {
        if (mainDish_Status[menuIndex].status == false)
        {
            mainDish_Status[menuIndex].status = true;
            MenuBoardUpdate();
        }

    }

    public void ActivateDrinksMenu(int menuIndex)
    {
        if(drinks_Status[menuIndex].status == false)
        {
            drinks_Status[menuIndex].status = true;
            MenuBoardUpdate();
        }
        
    }
}

[Serializable]
public class MenuStatus
{
    public GameObject menuGameobject;
    public float cost;
    public bool status;
}
