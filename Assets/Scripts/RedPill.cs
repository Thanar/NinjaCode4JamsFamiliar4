using UnityEngine;
using System.Collections;

public class RedPill : MonoBehaviour {

    public float focus = 25f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triger: " + other.tag);
        if (other.tag == "Player")
        {

            PlayerController p = other.GetComponent<PlayerController>();
            p.focus += focus;
            Destroy(this.gameObject);


        }
    }
}
