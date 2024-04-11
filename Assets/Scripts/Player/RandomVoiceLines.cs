using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomVoiceLines : MonoBehaviour
{
    public AudioClip[] voiceLines;
    AudioClip voiceLine;
    AudioClip oldVoiceline;
    void Start()
    {
        StartCoroutine(RandomTiming());
    }

    IEnumerator RandomTiming()
    {
        while(true)
        {
            if(!GameManager.Instance.isGamePaused)
            {
                if(voiceLine)
                {
                    oldVoiceline = voiceLine;
                }
                float randomTime = Random.Range(10, 20);
                int numberOfVoiceline = Random.Range(0, voiceLines.Count());
                voiceLine = voiceLines[numberOfVoiceline];
                if(oldVoiceline)
                {
                    
                    while(voiceLine == oldVoiceline)
                    {
                        randomTime = Random.Range(10, 20);
                        numberOfVoiceline = Random.Range(0, voiceLines.Count());
                        voiceLine = voiceLines[numberOfVoiceline];
                    }
                }
                
                AudioManager.Instance.PlaySound(voiceLine);
                yield return new WaitForSeconds(randomTime);
            }
        }
    }
}
