using UnityEngine;
using System.Collections;

public class MenuPlaySound : MonoBehaviour {

    public AudioSource menuflushAudio;

    public void play()
    {
        menuflushAudio.Play();
    }
}
