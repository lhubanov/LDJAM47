using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject FinalDestinationTarget; 

    [SerializeField]
    private SPECIAL_ACTION[] specialActionTypes;

    [SerializeField]
    private int[] specialActionsNeededToCompleteType;

    public void CompleteActionOfType( SPECIAL_ACTION completedActionType )
    {
        for( int i = 0; i < specialActionTypes.Length; ++i )
        {
            if( specialActionTypes[i] == completedActionType )
            {
                if( specialActionsNeededToCompleteType[i] > 0 )
                {
                    --specialActionsNeededToCompleteType[i];
                }

                return;
            }
        }
    }

    private void Update()
    {
        bool isGameFinished = true;

        foreach( int actions in specialActionsNeededToCompleteType )
        {
            isGameFinished &= ( actions == 0 );
        }

        if( isGameFinished )
        {
            PlayerController controller = FindObjectOfType<PlayerController>();
            if (controller)
            {
                controller.InsertObjective(FinalDestinationTarget.transform.position);
            }
        }
    }
}
