using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIAL_ACTION : byte
{
    HUM_MUSIC = 0,
    NEW_DISCOVERY,
    CLEANING,
    FIXING,
    EXCLAMATION,
    NONE
}

public class SpecialActionEmitter : MonoBehaviour
{
    [SerializeField]
    private SPECIAL_ACTION specialAction;

    [SerializeField]
    private ProgressionManager progressionManager;

    [SerializeField]
    private float cooldown = 5f;

    private Timer cooldownTimer;
    

    private void Start()
    {
        progressionManager = GameObject.FindObjectOfType<ProgressionManager>();

        cooldownTimer = new Timer();
        cooldownTimer.Start(cooldown);
    }

    private void Update()
    {
        if (cooldownTimer.IsActive())
        {
            cooldownTimer.Tick();
        }
    }

    public SPECIAL_ACTION GetSpecialAction()
    {
        if ( cooldownTimer.IsActive() && cooldownTimer.HasFinished() )
        {
            if (specialAction == SPECIAL_ACTION.NEW_DISCOVERY ||
            specialAction == SPECIAL_ACTION.HUM_MUSIC)
            {
                progressionManager.CompleteActionOfType(specialAction);
            }

			cooldownTimer.Reset();
			cooldownTimer.Start( cooldown );

            return specialAction;
        }

        return SPECIAL_ACTION.NONE;
    }
}
