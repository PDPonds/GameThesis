using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsTracker : MonoBehaviour
{

    public List<EventsName> tutorialsList = new List<EventsName>(); 
    //Counter
    private int T_ExplainUIcounter = 0;
    private int T_GoalOfTheDaycounter = 0;
    private int T_SlackOffcounter = 0;
    private int T_DineAndDashcounter = 0;
    private int T_Customer = 0;

    private bool isPause = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
            isPause = !isPause;
            if (isPause) { Time.timeScale = 0.0f; }
            else { 
                Time.timeScale = 1.0f; 
                //foreach (GameObject go in tutorialsList) { go.SetActive(false); }
            }
    }

    public void EventsCounter(string name)
    {
        foreach (var i in tutorialsList)
        {
            if(i.Event_Name == name)
            {
                i.eventsCount++;
                if(i.eventsCount == 1)
                {
                    PauseGame();
                    i.eventsUI.SetActive(true);
                }
            }
        }
    }
}

[Serializable] public class EventsName{
    public string Event_Name;
    public GameObject eventsUI;
    public int eventsCount;
}
