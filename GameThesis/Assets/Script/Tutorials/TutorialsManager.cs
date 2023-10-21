using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> employee = new List<GameObject>();
    [SerializeField] private List<GameObject> tutorials_Text = new List<GameObject>();
    public GameObject CheckStaffObjective;

    private int tutorials_number = 0;
    private bool isObjComplete;

    void Start()
    {
        foreach (GameObject employeePrefabs in employee)
        {
            employeePrefabs.SetActive(false);
        }

        ShowTutorials();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isObjComplete)
        {
            NextTutorials();
        }
    }

    private void NextTutorials()
    {
        tutorials_number++;
        ShowTutorials();
    }

    private void ShowTutorials()
    {
        foreach (GameObject tutorN in tutorials_Text)
        {
            tutorN.SetActive(false);
        }
        tutorials_Text[tutorials_number].SetActive(true);
        CheckObjectiveType();
    }

    private void CheckObjectiveType()
    {
        if (tutorials_Text[tutorials_number].transform.childCount >= 3)
        {
            isObjComplete = false;
            if (tutorials_number == 3)
            { 
                CheckStaffObjective.gameObject.SetActive(true);
                foreach (GameObject employeePrefabs in employee) { employeePrefabs.SetActive(true); }
            }
        }
        else
        {
            isObjComplete = true;
        }
    }

    public void SetObjectiveToComplete()
    {
        isObjComplete=true;
        Debug.Log("Obj Complete");
        CheckStaffObjective.gameObject.SetActive(false);
        NextTutorials();
    }
}
