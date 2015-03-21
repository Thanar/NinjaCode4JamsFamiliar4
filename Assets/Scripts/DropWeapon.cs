using UnityEngine;
using System.Collections;

public class DropWeapon : MonoBehaviour {
    public bool iAmDropped;
    public Weapon w;

    public void Start()
    {
        w = gameObject.GetComponent<Weapon>();
        w.dropWeapon = this;
    }

    void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("triger: " + other.tag);
        if (other.tag == "Player")
        {
            
            PlayerController p = other.GetComponent<PlayerController>();

            if (iAmDropped && !p.hasWeapon)
            {Debug.Log("Player!");
                p.EquipThis(w);
                iAmDropped = false;
            }
        }
    }

}
