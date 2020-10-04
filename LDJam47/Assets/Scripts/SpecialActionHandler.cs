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


    // FIXME: LH:
    // - Need to add collider
    // - Need to set right sprite before animating
    //      - Then just play the same sprite animation?
    // - Can play other animations/two animations at same time?
    // - Can play sounds

    private void Start()
    {
        animator = GetComponent<Animator>();
        reactionSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter( Collider other )
    {
        SpecialActionEmitter specialActionEmitter = other.GetComponent<SpecialActionEmitter>();
        if ( specialActionEmitter )
        {
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

                    animator.Play( Animator.StringToHash("RobotSpriteReaction") );
                    break;
                }
            case SPECIAL_ACTION.SMILE:
                {
                    reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.SMILE];


                    animator.Play( Animator.StringToHash("RobotSpriteReaction") );
                    break;
                }
        }
    }
}
