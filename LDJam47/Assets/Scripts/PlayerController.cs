using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Timer
{
    public void Start( float TimeToCount )
    {
        TimeRemaining = TimeToCount;
        StartTime = Time.deltaTime;
        TimerActive = true;
    }
    public void Tick()
    {
        TimeRemaining = TimeRemaining - Time.deltaTime - StartTime;
    }

    public bool HasFinished()
    {
        return TimeRemaining <= 0;
    }
    public void Reset()
    {
        StartTime = 0;
        TimerActive = false;
        TimeRemaining = 0;
    }
    public bool IsActive()
    {
        return TimerActive;
    }

    private float StartTime = 0;
    private float TimeRemaining = 0;
    private bool TimerActive = false;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int CurrentObjective = 0;

    [SerializeField]
    private List<GameObject> Objectives;

    [SerializeField]
    private GameObject Home;

    [SerializeField]
    private GameObject End;

    public NavMeshAgent agent;

    [SerializeField]
    private int TargetLeeway = 5;

    [SerializeField]
    private float TimeTosleep = 10;

    [SerializeField]
    private float InitialWaitTime = 5;

    private Timer SleepTimer;
    private Timer ObjectiveTimer;
    private Timer InitialWait;
    
    private void Start()
    {
        SleepTimer = new Timer();
        ObjectiveTimer = new Timer();
        InitialWait = new Timer();

        Home = GameObject.FindGameObjectWithTag("RobotHome");
        End = GameObject.FindGameObjectWithTag("FinalDestination");

        Reset();
    }

    private void Reset()
    {
        InitialWait.Start(InitialWaitTime);
    }

    void StartCycle()
    {
        CurrentObjective = 0;

        GameObject[] ObjectiveMarkers = GameObject.FindGameObjectsWithTag("Objective");

        if( Objectives == null )
        {
            Objectives = new List<GameObject>();
        }

        for (int i = 0; i < ObjectiveMarkers.Length; i = i + 1)
        {
            Objectives.Add( ObjectiveMarkers[i] );
        }

        agent.SetDestination(GetCurrentObjectivePosition());
    }

    void ProgramToReturnHome()
    {
        Home = GameObject.FindGameObjectWithTag("RobotHome");
        if(Home)
        {
            agent.SetDestination(Home.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( InitialWait.IsActive() )
        {
            if( InitialWait.HasFinished() )
            {
                InitialWait.Reset();
                StartCycle();
            }
            else
            {
                InitialWait.Tick();
            }
        }
        else
        {
            if (Objectives.Count > 0)
            {
                float dist = Vector3.Distance(agent.transform.position, GetCurrentObjectivePosition());
                if (dist < TargetLeeway)
                {
                    CompleteObjective();
                }
            }
            else if (Vector3.Distance(agent.transform.position, Home.transform.position) < TargetLeeway)
            {
                if (SleepTimer.IsActive())
                {
                    if (!SleepTimer.HasFinished())
                    {
                        SleepTimer.Tick();
                    }
                    else
                    {
                        StartCycle();
                        SleepTimer.Reset();
                    }
                }
                else
                {
                    SleepTimer.Start(TimeTosleep);

                    agent.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                }
            }
        }

        
        if( Vector3.Distance( this.transform.position, End.transform.position ) < 5 )
        {
            SceneManager.LoadScene("Menu");
        }
    }

    Vector3 GetCurrentObjectivePosition()
    {
        return Objectives[CurrentObjective].transform.position;
    }

    void SetNextTarget()
    {
        CurrentObjective = CurrentObjective + 1;
        if (CurrentObjective >= Objectives.Count)
        {
            ProgramToReturnHome();
            Objectives.Clear();
        }
        else
        {
            agent.SetDestination(GetCurrentObjectivePosition());
        }
    }

    void CompleteObjective()
    {
        if (ObjectiveTimer.IsActive())
        {
            if (ObjectiveTimer.HasFinished())
            {
                ObjectiveTimer.Reset();
                SetNextTarget();
            }
            else
            {
                ObjectiveTimer.Tick();
            }
        }
        else
        {
            // If current objective has a wait timer lets wait 
            GameObject Objective = Objectives[CurrentObjective];
            RobotTargetScript Script = Objective.GetComponent<RobotTargetScript>();
            if (Script && Script.WaitTimer > 0)
            {
                ObjectiveTimer.Start(Script.WaitTimer);
            }
            else
            {
                SetNextTarget();
            }
        }
    }

    public void InsertObjective(Vector3 position)
    {
        if( Objectives == null )
        {
            Objectives = new List<GameObject>();
        }

        int insertionIndex = CurrentObjective + 1;

        GameObject NewObjective = new GameObject();
        NewObjective.transform.position = position;
        if ( insertionIndex >= Objectives.Count )
        {
            Objectives.Add(NewObjective);
        }
        else
        {
            Objectives.Insert(insertionIndex, NewObjective);
        }
    }
}
