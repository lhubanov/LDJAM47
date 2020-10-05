using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int CurrentObjective = 0;

    [SerializeField]
    private List<Vector3> Objectives;

    [SerializeField]
    private GameObject Home;

    public NavMeshAgent agent;

    [SerializeField]
    private int TargetLeeway = 5;

    [SerializeField]
    private float TimeTosleep = 10;

    private float StartTime = 0;
    private float TimeRemaining = 0;
    private bool TimerActive = false;
    

    private void Start()
    {
        Home = GameObject.FindGameObjectWithTag("RobotHome");
        StartCycle();
    }

    void StartCycle()
    {
        CurrentObjective = 0;

        GameObject[] ObjectiveMarkers = GameObject.FindGameObjectsWithTag("Objective");

        if( Objectives == null )
        {
            Objectives = new List<Vector3>();
        }

        for (int i = 0; i < ObjectiveMarkers.Length; i = i + 1)
        {
            Objectives.Add( ObjectiveMarkers[i].transform.position );
        }

        agent.SetDestination(Objectives[CurrentObjective]);
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
        if( Objectives.Count > 0 )
        {
            float dist = Vector3.Distance(agent.transform.position, Objectives[CurrentObjective]);
            if (dist < TargetLeeway)
            {
                CompleteObjective();
            }
        }
        else if( Vector3.Distance(agent.transform.position, Home.transform.position) < TargetLeeway)
        {
            if( TimerActive )
            { 
                if(TimeRemaining > 0)
                {
                    TimeRemaining -= Time.deltaTime;
                }
                else
                {
                    StartCycle();
                    TimerActive = false;
                }
            }
            else
            {
                // Start timer
                StartTime = Time.deltaTime;
                TimeRemaining = TimeTosleep;
                TimerActive = true;

                agent.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            }
        }
    }

    void CompleteObjective()
    {
        CurrentObjective = CurrentObjective + 1;
        if (CurrentObjective >= Objectives.Count )
        {
            ProgramToReturnHome();
            Objectives.Clear();
        }
        else
        {
            agent.SetDestination(Objectives[CurrentObjective]);
        }
    }

    public void InsertObjective( Vector3 newObjective )
    {
        if( Objectives == null )
        {
            Objectives = new List<Vector3>();
        }

        int insertionIndex = CurrentObjective + 1;
        if( insertionIndex >= Objectives.Count )
        {
            Objectives.Add( newObjective );
        }
        else
        {
            Objectives.Insert(insertionIndex, newObjective);
        }
    }
}
