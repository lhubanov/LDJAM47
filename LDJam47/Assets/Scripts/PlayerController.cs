using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private int CurrentObjective = 0;
    private List<Vector3> Objectives;
    public NavMeshAgent agent;
    private GameObject Home;

    [SerializeField]
    private int TargetLeeway = 5;

    private void Start()
    {
        Home = GameObject.FindGameObjectWithTag("RobotHome");
        StartCycle();
    }

    void StartCycle()
    {
        CurrentObjective = 0;

        GameObject[] ObjectiveMarkers = GameObject.FindGameObjectsWithTag("Objective");

        Objectives = new List<Vector3>();
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
        else if(  Vector3.Distance(agent.transform.position, Home.transform.position) < TargetLeeway)
        {
            StartCycle();
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
}
