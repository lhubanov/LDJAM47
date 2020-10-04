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

    private void Start()
    {
        StartCycle();
    }

    void StartCycle()
    {
        CurrentObjective = 0;

        GameObject[] ObjectiveMarkers = GameObject.FindGameObjectsWithTag("Objective");
        for (int i = 0; i < ObjectiveMarkers.Length; i = i + 1)
        {
            Objectives[i] = ObjectiveMarkers[i].transform.position;
        }
        
    }

    void ProgramToReturnHome()
    {
        // Set Destination to the Home Pad;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown( 0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if( Physics.Raycast(ray, out hit) )
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void CompleteObjective()
    {
        CurrentObjective = CurrentObjective + 1;
        if (CurrentObjective > Objectives.Length )
        {
            ProgramToReturnHome();
        }
        else
        {
            agent.SetDestination(Objectives[CurrentObjective]);
        }
    }
}
