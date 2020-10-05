using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIAL_ACTION : byte
{
    HUM_MUSIC = 0,
    NEW_DISCOVERY,
    CLEANING,
    FIXING,
    EXCLAMATION
}

public class SpecialActionEmitter : MonoBehaviour
{
    [SerializeField]
    private SPECIAL_ACTION specialAction;

    [SerializeField]
    private ProgressionManager progressionManager;

    private void Start()
    {
        progressionManager = GameObject.FindObjectOfType<ProgressionManager>();
    }

    public SPECIAL_ACTION GetSpecialAction()
    {
        if( specialAction == SPECIAL_ACTION.NEW_DISCOVERY || 
            specialAction == SPECIAL_ACTION.HUM_MUSIC )
        {
            progressionManager.CompleteActionOfType( specialAction );
        }

        return specialAction;
    }
}
