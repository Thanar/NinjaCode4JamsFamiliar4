using UnityEngine;
using System.Collections;

public class PressAnyKeyToLoadScene : MonoBehaviour {

    public int LevelToLoad = 1;

	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        {
            Application.LoadLevel(LevelToLoad);
        }

	}
}
