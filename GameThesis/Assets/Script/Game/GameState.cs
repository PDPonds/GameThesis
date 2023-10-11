using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameState : StateManager
{
    public override BaseState s_currentState { get; set; }

    public BeforeStoreOpenState s_beforeState = new BeforeStoreOpenState();
    public OpenStoreState s_openState = new OpenStoreState();
    public SummaryDayState s_summaryState = new SummaryDayState();

    public GameObject g_customerPrefab;
    public Transform t_spawnPoint;

    public int i_maxCustomerCount;


    private void Start() {

        SwitchState(s_openState);

    }
    
    private void Update() {

        s_currentState.UpdateState(this);

        SpawnCustomer();
        

    }

    void SpawnCustomer()
    {
        if(s_currentState == s_openState)
        {
            if(RestaurantManager.Instance.allCustomers.Length < i_maxCustomerCount)
            {
                Instantiate(g_customerPrefab,t_spawnPoint.position,Quaternion.identity);
            }
        }
    }

}
