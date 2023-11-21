using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotAndPan : MonoBehaviour, IInteracable
{
    public float minTargetPoint;
    public float maxTargetPoint;

    public float maxPoint;

    public float pointMul;

    public float disTimeMul;

    public float cookingTime;

    [HideInInspector] public float currentTime;
    [HideInInspector] public float currentPoint;

    private void Update()
    {
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
            currentTime = 0;
            currentPoint = 0;
            Debug.Log("cooking Finish");
        }

        if (currentPoint > 0) currentPoint -= disTimeMul * Time.deltaTime;

    }

    public void Interaction()
    {
        if (PlayerManager.Instance.g_interactiveObj == this.gameObject)
        {
            if (currentPoint < maxPoint)
            {
                currentPoint += pointMul;
            }
        }

    }

    public string InteractionText()
    {
        return "[E] to cooking";
    }

    public Color InteractionTextColor()
    {
        return Color.white;
    }
}
