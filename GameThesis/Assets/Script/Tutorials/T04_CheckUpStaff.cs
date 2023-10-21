using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T04_CheckUpStaff : MonoBehaviour
{
    public TutorialsManager TutorialsManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TutorialsManager.SetObjectiveToComplete();
            this.gameObject.SetActive(false);
        }
    }
}
