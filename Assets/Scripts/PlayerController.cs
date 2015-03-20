﻿using UnityEngine;
using System.Collections;

public class PlayerController : Character {

    public UI ui;

    public float focus;
    public float focusDecreaseRate = 0.1f;
    public bool onFocus;

    public Weapon weapon;
    public bool hasWeapon=false;

    Vector3 newRotation = new Vector3(0,0,0);

    Vector3 rayVector = new Vector3();
    

    public float movementForceScale=1;


    public Transform fistsPosition;

    Character auxCharacter = null;
    

	// Use this for initialization
	void Start () {
	    //CUANDO SE PONGAN LAS ARMAS QUITAR ESTA LINEA
        ui.ToggleAmmo(false);
	}

    void Update()
    {
        RaycastHit hit;
        rayVector = Input.mousePosition;
        rayVector.z = 10000;
        Ray ray = Camera.main.ScreenPointToRay(rayVector);
        if (Physics.Raycast(ray, out hit))
        {
            newRotation.y = Quaternion.LookRotation(hit.point - transform.position).eulerAngles.y;
            transform.eulerAngles = newRotation;
        }

        UpdateStatus();

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateFocus();
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Vector3.Project(rigidbody.velocity, -Vector3.right).magnitude < maxSpeed)
            {
                rigidbody.AddForce(-Vector3.right.normalized * charachterImpulse * movementForceScale);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Vector3.Project(rigidbody.velocity, Vector3.right).magnitude < maxSpeed)
            {
                rigidbody.AddForce(Vector3.right.normalized * charachterImpulse * movementForceScale);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (Vector3.Project(rigidbody.velocity, Vector3.forward).magnitude < maxSpeed)
            {
                rigidbody.AddForce(Vector3.forward.normalized * charachterImpulse * movementForceScale);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Vector3.Project(rigidbody.velocity, -Vector3.forward).magnitude < maxSpeed)
            {
                rigidbody.AddForce(-Vector3.forward.normalized * charachterImpulse * movementForceScale);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (hasWeapon)
            {
                weapon.Attack();
            }
            else
            {
                if (fistsTimeReady < Time.time)
                {
                    foreach (Collider c in Physics.OverlapSphere(fistsPosition.position, 0.1f))
                    {
                        auxCharacter = c.GetComponent<Character>();
                        if (c)
                        {
                            Debug.Log("FALCON PUNCH");
                            fistsTimeReady = Time.time + fistsCooldown;
                            auxCharacter.Damage(fistsDamage, fistsArmorPenetration);
                            auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                        }
                    }
                }
            }
        }
    
    }

    public void ActivateFocus()
    {
        onFocus = true;

        Time.timeScale = 0.5f;
        movementForceScale = 0.7f;

    }

    void UpdateStatus()
    {
        if (onFocus)
        {
            focus = Mathf.Clamp(focus - (focusDecreaseRate * Time.deltaTime), 0f, 1f);
            ui.SetFocus(focus);
            if (focus == 0)
            {
                onFocus = false;


                Time.timeScale = 1f;
                movementForceScale = 1f;
            }
        }
    }

    public override void Damage(float damage, float armorPenetration = 0)
    {
        base.Damage(damage, armorPenetration);
        ui.SetHealth(health);
    }
}
