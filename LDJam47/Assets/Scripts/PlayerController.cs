using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private int CurrentObjective = 0;
    private Vector3[] Objectives;
    public Camera cam;
    public NavMeshAgent agent;

    [SerializeField]
    private int TargetLeeway = 5;

    private void Start()
    {
        StartCycle();
    }

    void StartCycle()
    {
        CurrentObjective = 0;

        GameObject[] ObjectiveMarkers = GameObject.FindGameObjectsWithTag("Objective");

        Objectives = new Vector3[ObjectiveMarkers.Length];
        for (int i = 0; i < ObjectiveMarkers.Length; i = i + 1)
        {
            Objectives[i] = ObjectiveMarkers[i].transform.position;
        }

        agent.SetDestination(Objectives[CurrentObjective]);
    }

    void ProgramToReturnHome()
    {
        GameObject RobotHome = GameObject.FindGameObjectWithTag("RobotHome");
        if( RobotHome )
        {
            agent.SetDestination(RobotHome.transform.position);
        }
    }

    bool IsWithinBound( float position, float target )
    {
        return position == Mathf.Clamp(position, target - TargetLeeway, target + TargetLeeway);
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(agent.transform.position, Objectives[CurrentObjective]);
        if ( dist < TargetLeeway  )
        {
            CompleteObjective();
        }
    }

    void CompleteObjective()
    {
        CurrentObjective = CurrentObjective + 1;
        if (CurrentObjective >= Objectives.Length )
        {
            ProgramToReturnHome();
        }
        else
        {
            agent.SetDestination(Objectives[CurrentObjective]);
        }
    }
}
