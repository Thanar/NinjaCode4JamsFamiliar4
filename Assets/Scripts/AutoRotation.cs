using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour {

    public float speed;
    public Vector3 a = new Vector3(1, 0, 0);

    public bool rotate;

	// Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(a, speed);
        }
    }
}
