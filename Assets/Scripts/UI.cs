using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    public Image health;
    public Image focus;
    public Image damageImage;
    public Color damageImageColor;
    public GameObject ammoObject;
    public Text ammo;
    public GameObject reloading;
    public Text currentRound;

    public GameObject ChoseLolipop;
    public GameObject InfoInGame;

    public PlayerController player;

    public bool damageDecreasing;
    public float damageDecreasingRate = 0.5f;

    void Start()
    {
        damageDecreasing = false;
        damageImageColor = damageImage.color;
        damageImageColor.a = 0f;
        damageImage.color = damageImageColor;
    }

    public void SetRound(EnemySpawner es)
    {
        currentRound.text = "Round: " + es.round + "\nEnemies On Screen: "+es.enemiesOnScreen;
    }

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

    public void DamageReceived()
    {
        damageDecreasing = true;
        damageImageColor.a = 1f;
        damageImage.color = damageImageColor;
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

        if (damageDecreasing)
        {
            damageImageColor.a = Mathf.Lerp(damageImageColor.a, 0f, damageDecreasingRate * Time.deltaTime);
            damageImage.color = damageImageColor;

            if (damageImageColor.a <= 0.0001f)
            {
                damageDecreasing = false;
            }
        }

    }

}
