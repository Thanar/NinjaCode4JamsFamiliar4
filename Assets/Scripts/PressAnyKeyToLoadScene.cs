using UnityEngine;
using System.Collections;

public class PressAnyKeyToLoadScene : MonoBehaviour {

    public string levelName = "menu";

	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        {
            Application.LoadLevel(levelName);
        }

	}
}
