using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotAndPan : MonoBehaviour, IInteracable
{
    [HideInInspector] public float minTargetPoint;
    public float maxTargetPoint;
    public float targetPointStep;

    public float maxPoint;
    public float pointMul;
    public float disTimeMul;
    public float cookingTime;

    [HideInInspector] public float currentTime;
    [HideInInspector] public float currentPoint;

    public ChairObj s_cookingChair;
    public bool b_canUse;
    public bool b_hasCooker;
    public bool b_isWorking;

    int count;

    MeshRenderer meshrnd;

    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

    public void ApplyOutlineColor(Color color, float scale)
    {
        meshrnd = transform.GetComponent<MeshRenderer>();

        Mpb.SetColor("_Color", color);
        Mpb.SetFloat("_Scale", scale);

        meshrnd.SetPropertyBlock(mpb);

    }


    private void Update()
    {
        minTargetPoint = maxTargetPoint - targetPointStep;
        if (minTargetPoint <= 0) minTargetPoint = 0;

        GameState state = GameManager.Instance.s_gameState;
        if (state.s_currentState == state.s_openState)
        {
            if (b_canUse)
            {
                if (s_cookingChair.b_finishCooking || s_cookingChair.s_currentCookingEmployee != null ||
                    s_cookingChair.s_currentCustomer == null ||
                    s_cookingChair.s_currentCustomer.s_currentState != s_cookingChair.s_currentCustomer.s_waitFoodState)
                {
                    UIManager.Instance.DestroyHelpCooker();
                    RestaurantManager.Instance.currentPotAndPan = null;
                    b_canUse = false;
                }

                if (currentPoint > minTargetPoint && currentPoint < maxTargetPoint)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    if (currentTime > 0) currentTime -= Time.deltaTime;
                }

                if (currentTime >= cookingTime)
                {
                    s_cookingChair.b_finishCooking = true;
                    b_canUse = false;

                    UIManager.Instance.DestroyHelpCooker();
                    RestaurantManager.Instance.currentPotAndPan = null;
                    s_cookingChair = null;
                    currentTime = 0;
                    currentPoint = 0;
                    count = 0;
                }

                if (currentPoint > 0) currentPoint -= disTimeMul * Time.deltaTime;

                ApplyOutlineColor(Color.white, 0.005f);

            }
            else
            {
                currentTime = 0;
                currentPoint = 0;
                count = 0;
                Color noColor = new Color(0, 0, 0, 0);
                ApplyOutlineColor(noColor, 0f);
            }

        }
        else if (state.s_currentState == state.s_afterOpenState)
        {
            if (!RestaurantManager.Instance.RestaurantIsEmpty())
            {
                if (b_canUse)
                {
                    if (s_cookingChair.b_finishCooking || s_cookingChair.s_currentCookingEmployee != null ||
                        s_cookingChair.s_currentCustomer == null ||
                        s_cookingChair.s_currentCustomer.s_currentState != s_cookingChair.s_currentCustomer.s_waitFoodState)
                    {
                        UIManager.Instance.DestroyHelpCooker();
                        RestaurantManager.Instance.currentPotAndPan = null;
                        b_canUse = false;
                    }

                    if (currentPoint > minTargetPoint && currentPoint < maxTargetPoint)
                    {
                        currentTime += Time.deltaTime;
                    }
                    else
                    {
                        if (currentTime > 0) currentTime -= Time.deltaTime;
                    }

                    if (currentTime >= cookingTime)
                    {
                        s_cookingChair.b_finishCooking = true;
                        b_canUse = false;

                        UIManager.Instance.DestroyHelpCooker();
                        RestaurantManager.Instance.currentPotAndPan = null;
                        s_cookingChair = null;
                        currentTime = 0;
                        currentPoint = 0;
                        count = 0;
                    }

                    if (currentPoint > 0) currentPoint -= disTimeMul * Time.deltaTime;

                    ApplyOutlineColor(Color.white, 0.005f);

                }
                else
                {
                    currentTime = 0;
                    currentPoint = 0;
                    count = 0;
                    Color noColor = new Color(0, 0, 0, 0);
                    ApplyOutlineColor(noColor, 0f);
                }
            }
        }
        else
        {
            currentTime = 0;
            currentPoint = 0;

            Color noColor = new Color(0, 0, 0, 0);
            ApplyOutlineColor(noColor, 0f);
        }

        if (currentTime > 0 || currentPoint > 0)
        {
            b_isWorking = true;
        }
        else
        {
            b_isWorking = false;
        }

    }

    public void Interaction()
    {
        GameState state = GameManager.Instance.s_gameState;

        if (state.s_currentState == state.s_openState)
        {
            if (b_canUse)
            {
                if (PlayerManager.Instance.g_interactiveObj == this.gameObject)
                {
                    if (currentPoint < maxPoint)
                    {
                        if (count == 0)
                        {
                            int dish = s_cookingChair.s_currentCustomer.i_dish;
                            int drink = s_cookingChair.s_currentCustomer.i_drink;

                            float dishCost = RestaurantManager.Instance.menuHandler.mainDish_Status[dish].cost;
                            float dirnkCost = RestaurantManager.Instance.menuHandler.drinks_Status[drink].cost;

                            float dishIng = dishCost / 2;
                            float drinkIng = dirnkCost / 2;

                            float IngCost = dishIng + drinkIng;

                            RestaurantManager.Instance.AddIngredientCost(IngCost);
                        }
                        currentPoint += pointMul;
                        count++;
                    }
                }
            }

        }
        else if (state.s_currentState == state.s_afterOpenState)
        {

        }
    }

    public string InteractionText()
    {
        GameState state = GameManager.Instance.s_gameState;

        if (state.s_currentState == state.s_openState)
        {
            return "[E] to cooking";
        }
        else return string.Empty;
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
