using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public TimePhase timePhase = TimePhase.Day;

    public int initialDayPoints;
    private int dayActionPoints;
    
    //Hiding timer values
    float hidingTimer = 15.0f;
    bool canCount = true;
    bool doOnce = false;

    //Sun
    public GameObject sun, moon;
    public Transform lightTarget;
    float morningAngle, dayAngle, nightAngle;
    private Vector3 startPosition;

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

    private void Start()
    {
        dayActionPoints = initialDayPoints;
        startPosition = sun.transform.position;
        morningAngle = 60.0f;
        dayAngle = 120.0f;
        nightAngle = 180.0f;
    }

    private void Update()
    {
        if (timePhase == TimePhase.Day)
        {
            float currentAngle = Vector3.Angle(lightTarget.position - startPosition, lightTarget.position - sun.transform.position);

            if ((currentAngle < morningAngle) && (dayActionPoints <= initialDayPoints) && (dayActionPoints >= (initialDayPoints * 60 / 100))) {
                Debug.Log("Rotate");
                sun.transform.RotateAround(lightTarget.position, Vector3.right, 10f * Time.deltaTime);
            } 
            else if ((currentAngle < dayAngle) && (dayActionPoints <= (initialDayPoints * 60 / 100)) && (dayActionPoints >= (initialDayPoints * 30/100)))
            {
                sun.transform.RotateAround(lightTarget.position, Vector3.right, 10f * Time.deltaTime);
            } 
            else if ((currentAngle < nightAngle) && (dayActionPoints <= (initialDayPoints * 30 / 100))) {
                sun.transform.RotateAround(lightTarget.position, Vector3.right, 10f * Time.deltaTime);
            }


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
        if ( dayActionPoints <= 0)
        {
            return true;
        }
        return false;
    }

    private void SetSunPosition()
    {
        if (dayActionPoints <= initialDayPoints / 3) // if less than third of actions are left, evening
        {
            sun.transform.RotateAround(lightTarget.position, Vector3.right, 180.0f);
        }
        else if ((dayActionPoints <= (initialDayPoints / 3) * 2) && (dayActionPoints >= initialDayPoints / 3)) // if less than 2/3 of actions are left, set midday 
        {
            sun.transform.RotateAround(lightTarget.position, Vector3.right, 90.0f);
        }
        else if ((dayActionPoints <= initialDayPoints ) && (dayActionPoints >= initialDayPoints/3 * 2))
        {
            sun.transform.RotateAround(lightTarget.position, Vector3.right, 0.0f);
        }
    }

    public void ApplyDayAction(int act)
    {
        Debug.Log("Apply Action");
        dayActionPoints -= act;
    }



}

public enum TimePhase
{
    Day, 
    Night
}
