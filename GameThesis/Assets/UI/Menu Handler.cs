using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MenuHandler : MonoBehaviour
{
    public List<MenuStatus> mainDish_Status = new List<MenuStatus>();
    public List<MenuStatus> drinks_Status = new List<MenuStatus>();

    public GameObject LiqourLicense;

    void Start()
    {
        ActivateDishMenu(0);
        ActivateDrinksMenu(0);
    }

    public bool DishStatus(int menuIndex)
    {
        return mainDish_Status[menuIndex].status;
    }

    public bool DrinkStatus(int menuIndex)
    {
        return drinks_Status[menuIndex].status;
    }

    private void Update()
    {
        GameState state = GameManager.Instance.s_gameState;
        bool isLevel2 = RestaurantManager.Instance.i_level > 1;

        if (state.s_currentState == state.s_beforeOpenState)
        {
            if(TutorialManager.Instance.currentTutorialIndex < 19)
            {
                for (int i = 0; i < mainDish_Status.Count; i++)
                {
                    if (mainDish_Status[i].status == true)
                    {
                        if (!mainDish_Status[i].menuGameobject.activeSelf)
                            mainDish_Status[i].menuGameobject.SetActive(true);

                    }
                    else
                    {
                        if (mainDish_Status[i].menuGameobject.activeSelf)
                            mainDish_Status[i].menuGameobject.SetActive(false);
                    }
                }

                for (int i = 0; i < drinks_Status.Count; i++)
                {
                    if (drinks_Status[i].status == true)
                    {
                        if (!drinks_Status[i].menuGameobject.activeSelf)
                            drinks_Status[i].menuGameobject.SetActive(true);
                    }
                    else
                    {
                        if (drinks_Status[i].menuGameobject.activeSelf)
                            drinks_Status[i].menuGameobject.SetActive(false);
                    }
                }
            }
            else if(TutorialManager.Instance.currentTutorialIndex > 18 &&
                TutorialManager.Instance.currentTutorialIndex < 26)
            {
                for (int i = 0; i < mainDish_Status.Count; i++)
                {
                    if (mainDish_Status[i].status == true)
                    {
                        if (!mainDish_Status[i].menuGameobject.activeSelf)
                            mainDish_Status[i].menuGameobject.SetActive(true);

                    }
                    else
                    {
                        if (mainDish_Status[i].menuGameobject.activeSelf)
                            mainDish_Status[i].menuGameobject.SetActive(false);
                    }
                }

                for (int i = 0; i < drinks_Status.Count; i++)
                {
                    if (drinks_Status[i].status == true)
                    {
                        if (!drinks_Status[i].menuGameobject.activeSelf)
                            drinks_Status[i].menuGameobject.SetActive(true);
                    }
                    else
                    {
                        if(i == 1)
                        {
                            if (!drinks_Status[i].menuGameobject.activeSelf)
                                drinks_Status[i].menuGameobject.SetActive(true);
                        }
                        else
                        {
                            if (drinks_Status[i].menuGameobject.activeSelf)
                            {
                                drinks_Status[i].menuGameobject.SetActive(false);
                            }
                        }
                    }
                }
            }
            else
            {
                if (isLevel2)
                {
                    for (int i = 0; i < mainDish_Status.Count; i++)
                    {
                        if (!mainDish_Status[i].menuGameobject.activeSelf)
                        {
                            mainDish_Status[i].menuGameobject.SetActive(true);
                        }
                    }
                    for (int i = 0; i < drinks_Status.Count; i++)
                    {
                        if (!drinks_Status[i].menuGameobject.activeSelf)
                        {
                            drinks_Status[i].menuGameobject.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mainDish_Status.Count; i++)
                    {
                        if (i != 2)
                        {
                            if (!mainDish_Status[i].menuGameobject.activeSelf)
                            {
                                mainDish_Status[i].menuGameobject.SetActive(true);
                            }
                        }
                        else
                        {
                            if (mainDish_Status[i].menuGameobject.activeSelf)
                                mainDish_Status[i].menuGameobject.SetActive(false);
                        }
                    }
                    for (int i = 0; i < drinks_Status.Count; i++)
                    {
                        if (!drinks_Status[i].menuGameobject.activeSelf)
                        {
                            drinks_Status[i].menuGameobject.SetActive(true);
                        }
                    }
                }

            }
            

        }
        else
        {
            for (int i = 0; i < mainDish_Status.Count; i++)
            {
                if (mainDish_Status[i].status == true)
                {
                    if (!mainDish_Status[i].menuGameobject.activeSelf)
                        mainDish_Status[i].menuGameobject.SetActive(true);

                }
                else
                {
                    if (mainDish_Status[i].menuGameobject.activeSelf)
                        mainDish_Status[i].menuGameobject.SetActive(false);
                }
            }

            for (int i = 0; i < drinks_Status.Count; i++)
            {
                if (drinks_Status[i].status == true)
                {
                    if (!drinks_Status[i].menuGameobject.activeSelf)
                        drinks_Status[i].menuGameobject.SetActive(true);
                }
                else
                {
                    if (drinks_Status[i].menuGameobject.activeSelf)
                        drinks_Status[i].menuGameobject.SetActive(false);
                }
            }
        }

        if (drinks_Status[1].status) LiqourLicense.SetActive(true);
        else LiqourLicense.SetActive(false);

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

    public void ActivateDishMenu(int menuIndex)
    {
        if (mainDish_Status[menuIndex].status == false)
        {
            mainDish_Status[menuIndex].status = true;
        }

    }

    public void ActivateDrinksMenu(int menuIndex)
    {
        if (drinks_Status[menuIndex].status == false)
        {
            drinks_Status[menuIndex].status = true;
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
