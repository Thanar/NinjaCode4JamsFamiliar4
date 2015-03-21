using UnityEngine;
using System.Collections;

public class DropWeapon : MonoBehaviour {
    public bool iAmDropped;
    public Weapon w;

    public void Awake()
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
            if (iAmDropped)
            {
                if (!p.hasWeapon)
                {
                    //Debug.Log("Player!");
                    p.EquipThis(w);
                    iAmDropped = false;
                }
                else if (w.GetType() == p.weapon.GetType())
                {
                    Debug.Log("Ammo");
                    p.weapon.bulletsTotal += w.bulletsTotal + w.currentBullets;
                    p.ui.SetAmmo(p.weapon);
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
