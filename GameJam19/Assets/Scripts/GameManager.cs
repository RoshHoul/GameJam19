using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public TimePhase timePhase = TimePhase.Day;
    public int dayActions = 0;
    
    //Hiding timer values
    float hidingTimer = 15.0f;
    bool canCount = true;
    bool doOnce = false;


    //GM Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if ( instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timePhase == TimePhase.Day)
        {
            if (DayActionsOver())
            {
                if (hidingTimer >= 0.0f && canCount)
                {
                    hidingTimer -= Time.deltaTime;
                    Debug.Log(hidingTimer);
                    //update UI
                }

                if (hidingTimer <= 0.0f && !doOnce)
                {
                    
                    canCount = false;
                    doOnce = true;
                    hidingTimer = 0.0f;
                    //update UI

                    //change to Night Conditions
                    timePhase = TimePhase.Night;
                    Debug.Log("HIDE BEIBE");
                }
            }
        }
    }

    private bool DayActionsOver()
    {
        if ( dayActions <= 0)
        {
            return true;
        }
        return false;
    }

    public void ApplyDayAction(int act)
    {
        Debug.Log("Apply Action");
        dayActions -= act;
    }



}

public enum TimePhase
{
    Day, 
    Night
}
