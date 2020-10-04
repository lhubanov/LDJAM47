using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SPECIAL_ACTION : byte
{
    HUM_MUSIC = 0,
    SMILE
}

public class SpecialActionEmitter : MonoBehaviour
{
    [SerializeField]
    private SPECIAL_ACTION specialAction;

    public SPECIAL_ACTION GetSpecialAction()
    {
        return specialAction;
    }
}
