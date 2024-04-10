using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipStory : MonoBehaviour
{
    private float timeOfCutscene;
    int counter = 1;
    // Start is called before the first frame update
    void Start()
    {
        timeOfCutscene = 99;
        StartCoroutine(time());
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            timeOfCutscene = -1;
        }
        if (timeOfCutscene > 0 && timeOfCutscene < 10 && counter == 1)
        {
            counter -= 1;
            StartCoroutine(AudioManager.Instance.FadeOutMusic(2500));
        }
        if (timeOfCutscene < 0)
        {
            SceneManager.LoadScene("RichardScene");
        }
    }

    IEnumerator time(){
    while (true)
    {
        yield return new WaitForSeconds(1);
        timeOfCutscene -= 1;
    }
}

}
