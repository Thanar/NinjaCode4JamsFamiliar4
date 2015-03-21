using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public bool moving;
    public Transform player;

	// Use this for initialization
	void Start () {
        moving = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 0.1f);

        }
        

	}
}
