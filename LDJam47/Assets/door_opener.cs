using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_opener : MonoBehaviour
{
    public Animator door_animator;
    public AudioSource close_audio;
    public AudioSource open_audio;

    private void OnTriggerEnter(Collider other)
    {
        open_doors();
    }

    private void OnTriggerExit(Collider other)
    {
        close_doors();
    }

    public void open_doors()
    {
        open_audio.Play();
        door_animator.SetBool("Open", true);
    }

    public void close_doors()
    {
        close_audio.Play();
        door_animator.SetBool("Open", false);
    }
}
