using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipStory : MonoBehaviour
{
    public GameObject partOne;
    public GameObject partTwo;
    public GameObject partThree;
    public GameObject partFour;
    public GameObject logo;

    public TextMeshProUGUI textMeshProUGUI;

    int counter = 4;

    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if(counter == 4)
            {
                partOne.SetActive(false);
                partTwo.SetActive(true);
                counter--;
            }

            else if(counter == 3)
            {
                partTwo.SetActive(false);
                partThree.SetActive(true);
                counter--;
            }

            else if(counter == 2)
            {
                partThree.SetActive(false);
                partFour.SetActive(true);
                counter--;
            }

            else if(counter == 1)
            {
                partFour.SetActive(false);
                textMeshProUGUI.text = "Press spacebar to start game";
                logo.SetActive(true); 
                counter--;
            }

            else if(counter == 0)
            {
                AudioManager.Instance.StopMusic();
                SceneManager.LoadScene("RichardScene");
            }
        }
    }

}
