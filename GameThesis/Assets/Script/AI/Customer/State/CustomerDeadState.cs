using UnityEngine;

public class CustomerDeadState : BaseState
{
    float f_currentDestroyTime;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        f_currentDestroyTime = cus.f_destroyTime;
        cus.b_escape = false;
        cus.b_isDrunk = false;

        Color noColor = new Color(0, 0, 0, 0);
        cus.ApplyOutlineColor(noColor, 0f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(true);

        if (!RestaurantManager.Instance.HasCustomerInFightState())
        {
            for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
            {
                SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
                if (shrSM.s_currentState == shrSM.s_waitForFightEnd &&
                    cus.currentAreaStay == AreaType.OutRestaurant)
                {
                    shrSM.SwitchState(shrSM.s_activeThrong);
                }
            }
        }

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.RagdollOn();

        if (PlayerManager.Instance.g_dragObj != ai.transform.gameObject)
        {
            f_currentDestroyTime -= Time.deltaTime;
            if (f_currentDestroyTime <= 0)
            {
                cus.DestroyAI();
            }
        }
        else
        {
            f_currentDestroyTime = cus.f_destroyTime;
        }

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("walk", false);
        cus.anim.SetBool("run", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);
        cus.anim.SetBool("cheer", false);

        DisableAllSound(cus);

    }

    void DisableAllSound(CustomerStateManager cus)
    {
        AudioSource[] sources = cus.transform.GetComponents<AudioSource>();
        if (sources.Length > 0)
        {
            foreach (AudioSource source in sources)
            {
                source.Stop();
            }
        }
    }

}
