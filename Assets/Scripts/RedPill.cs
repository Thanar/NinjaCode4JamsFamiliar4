﻿using UnityEngine;
using System.Collections;

public class RedPill : MonoBehaviour {

    public float focus = 0.25f;
    public GameObject parent;

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
            p.AddFocus(focus);
            Destroy(parent);


        }
    }
}
