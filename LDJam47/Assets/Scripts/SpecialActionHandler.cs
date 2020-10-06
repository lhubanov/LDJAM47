using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialActionHandler : MonoBehaviour
{
    bool has_been_ten_secs_since_last_audio_reaction = true;
    public float audiotimertime = 3;
    public Timer audio_timer;

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

    [SerializeField]
    private AudioClip[] robotNoises;

    [SerializeField]
    private AudioSource noiseSource;

    private int SAD_NOISE_INDEX = 0;
    private int HAPPY_NOISE_INDEX = 1;
    private int SURPRISED_NOISE_INDEX = 2;
    private int ANGRY_NOISE_INDEX = 3;
    private int NEUTRAL_NOISE_INDEX = 4;


    private void Start()
    {
        animator = GetComponent<Animator>();
        audio_timer = new Timer();
        audio_timer.Start( 0 );
    }

    private void OnTriggerEnter(Collider other)
    {
        SpecialActionEmitter specialActionEmitter = other.GetComponent<SpecialActionEmitter>();
        if (specialActionEmitter)
        {
            Debug.Log("Special action emitter found!");
            DoSpecialAction(specialActionEmitter.GetSpecialAction());
        }
    }

    private void PlayNoise(int soundIndex )
    {
        if (!noiseSource.isPlaying && audio_timer.HasFinished())
        {
            noiseSource.clip = robotNoises[soundIndex]; 
            noiseSource.Play();
            has_been_ten_secs_since_last_audio_reaction = false;

            audio_timer.Reset();
            audio_timer.Start(audiotimertime);
        }
    }

    void Update()
    {
        if ( audio_timer.IsActive() )
        {
            audio_timer.Tick();
        }
    }

    private void DoSpecialAction(SPECIAL_ACTION action)
    {
        switch (action)
        {
            case SPECIAL_ACTION.HUM_MUSIC:
            {
                // FIXME: LH: Messy way to do this? cause specialActions may not necessarily be sprite reactions
                // - whatever I'm tired
                reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.HUM_MUSIC];
                robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.HUM_MUSIC];
              
                animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                PlayNoise(HAPPY_NOISE_INDEX);

                break;
            }
            case SPECIAL_ACTION.NEW_DISCOVERY:
            {
                reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.NEW_DISCOVERY];
                robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.NEW_DISCOVERY];

                animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                PlayNoise(SURPRISED_NOISE_INDEX);
                break;
            }

            case SPECIAL_ACTION.CLEANING:
            {
                reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.CLEANING];
                robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.CLEANING];

                animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                PlayNoise(SAD_NOISE_INDEX);
                break;
            }

            case SPECIAL_ACTION.FIXING:
            {
                reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.FIXING];
                robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.FIXING];

                animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                PlayNoise(ANGRY_NOISE_INDEX);
                break;
            }

            case SPECIAL_ACTION.EXCLAMATION:
            {
                reactionSpriteRenderer.sprite = reactionSprites[(int)SPECIAL_ACTION.EXCLAMATION];
                robotSpriteRenderer.sprite = robotSprites[(int)SPECIAL_ACTION.EXCLAMATION];

                animator.Play(Animator.StringToHash("RobotSpriteReaction"));
                PlayNoise(HAPPY_NOISE_INDEX);
                break;
            }
        }
    }
}
