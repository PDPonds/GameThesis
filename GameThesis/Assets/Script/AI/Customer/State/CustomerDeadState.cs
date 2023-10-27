using UnityEngine;

public class CustomerDeadState : BaseState
{
    float f_currentDestroyTime;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_currentDestroyTime = customerStateManager.f_destroyTime;
        customerStateManager.b_escape = false;
        customerStateManager.b_isDrunk = false;

        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(true);

        if (!RestaurantManager.Instance.HasCustomerInFightState())
        {
            for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
            {
                SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
                if (shrSM.s_currentState == shrSM.s_waitForFightEnd &&
                    customerStateManager.currentAreaStay == AreaType.OutRestaurant)
                {
                    shrSM.SwitchState(shrSM.s_activeThrong);
                }
            }
        }
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.RagdollOn();
        customerStateManager.DisablePunch();

        if (PlayerManager.Instance.g_dragObj != ai.transform.gameObject)
        {
            f_currentDestroyTime -= Time.deltaTime;
            if (f_currentDestroyTime <= 0)
            {
                customerStateManager.DestroyAI();
            }
        }
        else
        {
            f_currentDestroyTime = customerStateManager.f_destroyTime;
        }

    }
}
