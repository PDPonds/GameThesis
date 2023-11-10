using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameState : StateManager
{
    public override BaseState s_currentState { get; set; }

    public BeforeOpenStoreState s_beforeOpenState = new BeforeOpenStoreState();
    public OpenStoreState s_openState = new OpenStoreState();
    public AffterOpenStoreState s_afterOpenState = new AffterOpenStoreState();

    public List<GameObject> g_customerPrefab = new List<GameObject>();
    public List<Transform> t_spawnPoint = new List<Transform>();

    public int i_maxCustomerCount;

    private void Start()
    {
        SwitchState(s_beforeOpenState);
    }

    private void Update()
    {
        s_currentState.UpdateState(this);

        SpawnCustomer();

        if (TimeController.Instance.d_currentTime.Hour == TimeController.Instance.f_endTime)
        {
            if (s_currentState != s_afterOpenState)
                SwitchState(s_afterOpenState);

        }

    }

    public void SpawnCustomer()
    {
        if (RestaurantManager.Instance.allCustomers.Length < i_maxCustomerCount)
        {
            int prefabIndex = UnityEngine.Random.Range(0, g_customerPrefab.Count);
            int spawnPosIndex = UnityEngine.Random.Range(0, t_spawnPoint.Count);

            GameObject cus = Instantiate(g_customerPrefab[prefabIndex], t_spawnPoint[spawnPosIndex].position, Quaternion.identity);
            CustomerStateManager state = cus.GetComponent<CustomerStateManager>();

            state.CustomerClothes = state.GenerateClothes();

            float gangPercent = UnityEngine.Random.Range(0f, 100f);
            if (gangPercent <= state.f_isGangsterPercent) state.b_hasGang = true;
            else state.b_hasGang = false;

            state.i_prefabIndex = prefabIndex;
            state.i_spawnPosIndex = spawnPosIndex;
        }
    }

    public void SpawnCustomerGang(int prefabIndex, CustomerClothes closth)
    {
        int spawnPosIndex = UnityEngine.Random.Range(0, t_spawnPoint.Count);

        GameObject cus = Instantiate(g_customerPrefab[prefabIndex], t_spawnPoint[spawnPosIndex].position, Quaternion.identity);

        cus.name = "Gang";
        CustomerStateManager state = cus.GetComponent<CustomerStateManager>();
        state.SetUpClothes(closth.hair, closth.shirt, closth.pant, closth.hat);

        state.b_isGang = true;
        state.SwitchState(state.s_fightState);

    }


}
