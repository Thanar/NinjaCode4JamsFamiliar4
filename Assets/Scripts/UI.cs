using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    public Image health;
    public Image focus;
    public GameObject ammoObject;
    public Image ammo;

    public PlayerController player;

    public void SetHealth(float value)
    {
        health.fillAmount = Mathf.Clamp(value* 0.001f, 0f, 1f);
    }

    public void SetFocus(float value)
    {
        focus.fillAmount = Mathf.Clamp(value, 0f, 1f);
    }

    public void ToggleAmmo(bool active)
    {
        ammoObject.SetActive(active);
    }

    public void SetAmmo(float value)
    {
        ammo.fillAmount = Mathf.Clamp(value * 0.001f, 0f, 1f);
    }
}
