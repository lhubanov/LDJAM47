using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter_Level_Controller : MonoBehaviour
{
    public Animator transition;
    private float transition_time = 3f;
    private const int chicken_ending_index = 11;
    private const int human_ending_index = 12;
    private const int thank_you_index = 13;
    public bool auto_advance_level = false;


    public void load_next_level()
    {
        StartCoroutine(load_level(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void reload_current_level()
    {
        StartCoroutine(load_level(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void quit_game()
    {
        StartCoroutine(quit());
    }

    IEnumerator quit()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transition_time);
        Application.Quit();
    }

    IEnumerator load_level(int level_index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transition_time);
        SceneManager.LoadScene(level_index);
    }
}
