using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtPlayerInWorldSpace : MonoBehaviour
{
    
    void Update()
    {
        Vector3 lookAtPos = new Vector3(PlayerManager.Instance.transform.position.x, transform.position.y, PlayerManager.Instance.transform.position.z);
        transform.LookAt(lookAtPos);    
    }
}
