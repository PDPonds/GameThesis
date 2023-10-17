using UnityEngine;

public class CustomerDeadState : BaseState
{
    float f_currentDestroyTime;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_currentDestroyTime = customerStateManager.f_destroyTime;
        customerStateManager.b_escape = false;
        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
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
