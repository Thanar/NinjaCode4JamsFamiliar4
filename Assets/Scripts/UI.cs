using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    public Image health;
    public Image focus;
    public GameObject ammoObject;
    public Text ammo;
    public GameObject reloading;

    public GameObject ChoseLolipop;
    public GameObject InfoInGame;

    public PlayerController player;



    public void SetHealth()
    {
        health.fillAmount = player.health / player.Maxhealth;


        //Debug.Log("player.health: " + player.health + " - health.fillAmount: " + health.fillAmount);
    }

    public void SetFocus()
    {
        focus.fillAmount = player.focus / player.MaxFocus;


    }

    public void ToggleAmmo(bool active)
    {
        ammoObject.SetActive(active);
    }

    public void SetAmmo(Weapon w)
    {
        ammo.text = w.currentBullets+" | "+w.bulletsTotal;
    }

    public void SetReloading(float time)
    {
        reloading.SetActive(true);
        StartCoroutine(DeactivateReloadingDeferred(time));
    }


    public IEnumerator DeactivateReloadingDeferred(float time)
    {
        yield return new WaitForSeconds(time);
        reloading.SetActive(false);
    }

    public void Update()
    {
        if (player.Chosing < 3)
        {
            InfoInGame.SetActive(false);
            ChoseLolipop.SetActive(true);
        }
        else
        {
            InfoInGame.SetActive(true);
            ChoseLolipop.SetActive(false);
        }

    }

}
