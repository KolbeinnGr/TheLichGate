using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource sfx;
    void Start()
    {
        AudioManager.Instance._sfxSource = sfx;
    }

}
