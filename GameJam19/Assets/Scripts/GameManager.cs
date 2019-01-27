using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public TimePhase timePhase = TimePhase.Day;

    public int initialDayPoints;
    public int dayActionPoints;
    
    //Hiding timer values
    float hidingTimer = 15.0f;
    bool canCount = true;
    bool doOnce = false;

    //Sun
    public GameObject sun, moon;
    public Transform lightTarget;
    float morningAngle, dayAngle, nightAngle;
    private Vector3 startPosition;

    Player player;
    EnemyAgent enemy;

    public AudioClip dayAmbient, nightAmbient;
    public AudioSource musicSource;

    public float lengthOfNightInSeconds = 10;
    public bool isTimerRunning = false;

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

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAgent>();

        morningAngle = 60.0f;
        dayAngle = 120.0f;
        nightAngle = 180.0f;

        musicSource = GetComponent<AudioSource>();

        musicSource.clip = dayAmbient;
        musicSource.Play();
    }

    private void Update()
    {
        if (timePhase == TimePhase.Day)
        {
            enemy.SetState(AgentState.Inactive);

            musicSource.clip = dayAmbient;
            float currentAngle = Vector3.Angle(lightTarget.position - startPosition, lightTarget.position - sun.transform.position);

            int third = 30 * initialDayPoints / 100; // One third of the initial days, calculated for convenience

            if ((currentAngle < morningAngle) && (dayActionPoints <= initialDayPoints) && (dayActionPoints >= third * 2)) {
                sun.transform.RotateAround(lightTarget.position, Vector3.right, 10f * Time.deltaTime);
            } 
            else if ((currentAngle < dayAngle) && (dayActionPoints <= third * 2) && (dayActionPoints >= third))
            {
                sun.transform.RotateAround(lightTarget.position, Vector3.right, 10f * Time.deltaTime);
            } 
            else if ((currentAngle < nightAngle) && (dayActionPoints <= third)) {
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
        } else if (timePhase == TimePhase.Night)
        {
            enemy.SetState(AgentState.Patrolling);
            if(!isTimerRunning)
            {
                StartCoroutine(StartCountDown());
            }
            //musicSource.clip = nightAmbient;
            //musicSource.Play();
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

    private IEnumerator StartCountDown()
    {
        isTimerRunning = true;
        if(lengthOfNightInSeconds > 0)
        {
            lengthOfNightInSeconds -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            StartCoroutine(StartCountDown());
        }
        else
        {
            GameOver(true);
        }
    }

    public void GameOver(bool win)
    {
        if(win)
        {
            SceneManager.LoadScene("WIN", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("LOSE", LoadSceneMode.Single);
        }
    }
}

public enum TimePhase
{
    Day, 
    Night
}
