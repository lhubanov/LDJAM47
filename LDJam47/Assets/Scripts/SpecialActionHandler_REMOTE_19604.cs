using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionHandler : MonoBehaviour
{
    // Emoji animator
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer reactionSpriteRenderer;

    [SerializeField]
    private SpriteRenderer robotSpriteRenderer;

    [SerializeField]
    private Sprite[] reactionSprites;

    [SerializeField]
    private Sprite[] robotSprites;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter( Collider other )
    {
        SpecialActionEmitter specialActionEmitter = other.GetComponent<SpecialActionEmitter>();
        if ( specialActionEmitter )
        {
            Debug.Log("Special action emitter found!");
            DoSpecialAction( specialActionEmitter.GetSpecialAction() );
        }
    }

    private void DoSpecialAction( SPECIAL_ACTION action )
    {
        switch(action)
        {
            case SPECIAL_ACTION.HUM_MUSIC:
                {
                    // FIXME: LH: Messy way to do this? cause specialActions may not necessarily be sprite reactions
                    // - whatever I'm tired
                    reactionSpriteRenderer.sprite = reactionSprites[ (int)SPECIAL_ACTION.HUM_MUSIC ];
                    robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.HUM_MUSIC];

                    animator.Play( Animator.StringToHash("RobotSpriteReaction") );
                    break;
                }
            case SPECIAL_ACTION.NEW_DISCOVERY:
                {
                    reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.NEW_DISCOVERY];
                    robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.NEW_DISCOVERY];

                    animator.Play( Animator.StringToHash("RobotSpriteReaction") );
                    break;
                }

            case SPECIAL_ACTION.CLEANING:
                {
                    reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.CLEANING];
                    robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.CLEANING];

                    animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                    break;
                }

            case SPECIAL_ACTION.FIXING:
                {
                    reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.FIXING];
                    robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.FIXING];

                    animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                    break;
                }

            case SPECIAL_ACTION.EXCLAMATION:
                {
                    reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.EXCLAMATION];
                    robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.EXCLAMATION];

                    animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                    break;
                }
        }
    }
}
