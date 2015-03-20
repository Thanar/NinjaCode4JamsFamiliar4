﻿using UnityEngine;
using System.Collections;

public class PlayerController : Character {

    public UI ui;
    public float MaxFocus;
    public float focus;
    public float focusDecreaseRate = 0.1f;
    public bool onFocus;

    public Weapon weapon;
    public bool hasWeapon=false;

    Vector3 newRotation = new Vector3(0,0,0);

    Vector3 rayVector = new Vector3();
    

    public float movementForceScale=1;

    public Transform fistsPosition;

    EnemyAI auxCharacter = null;

    public Vector3 pushForce = new Vector3();

    Vector3 finalVelocity = new Vector3();



    public GameObject specialAttackEffectPrefab;

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

        pushForce = Vector3.Lerp(pushForce, Vector3.zero, 0.05f);

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
                        auxCharacter = c.GetComponent<EnemyAI>();
                        if (auxCharacter)
                        {
                            fistsTimeReady = Time.time + fistsCooldown;
                            auxCharacter.Damage(fistsDamage, fistsArmorPenetration);
                            //auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                            auxCharacter.Push(transform.forward.normalized * 50);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (focus > 0.2f)
            {
                focus -= 0.2f;
                ui.SetFocus();
                if (specialAttackEffectPrefab != null)
                {
                    Instantiate(specialAttackEffectPrefab, transform.position, Quaternion.identity);    
                }
                
                foreach (Collider c in Physics.OverlapSphere(transform.position, 5f))
                {
                    auxCharacter = c.GetComponent<EnemyAI>();
                    if (auxCharacter)
                    {
                        auxCharacter.Damage(fistsDamage * 5, fistsArmorPenetration * 2);
                        //auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                        auxCharacter.Push((auxCharacter.transform.position - transform.position).normalized * 50);
                    }
                }
            }
        }
    }

    public void Push(Vector3 direction)
    {
        pushForce = direction + direction.normalized * maxSpeed;
    }

    void FixedUpdate()
    {
        finalVelocity.Set(0,0,0);
        if (Input.GetKey(KeyCode.A))
        {
            //if (Vector3.Project(rigidbody.velocity, -Vector3.right).magnitude < maxSpeed)
            //{
            //    rigidbody.AddForce(-Vector3.right.normalized * charachterImpulse * movementForceScale);
            //}

            //rigidbody.velocity = -Vector3.right.normalized * maxSpeed + pushForce;
            finalVelocity.x -=  maxSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //if (Vector3.Project(rigidbody.velocity, Vector3.right).magnitude < maxSpeed)
            //{
            //    rigidbody.AddForce(Vector3.right.normalized * charachterImpulse * movementForceScale);
            //}
            //rigidbody.velocity = Vector3.right.normalized * maxSpeed + pushForce;
            finalVelocity.x += maxSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            //if (Vector3.Project(rigidbody.velocity, Vector3.forward).magnitude < maxSpeed)
            //{
            //    rigidbody.AddForce(Vector3.forward.normalized * charachterImpulse * movementForceScale);
            //}
            //rigidbody.velocity = Vector3.forward.normalized * maxSpeed + pushForce;
            finalVelocity.z += maxSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //if (Vector3.Project(rigidbody.velocity, -Vector3.forward).magnitude < maxSpeed)
            //{
            //    rigidbody.AddForce(-Vector3.forward.normalized * charachterImpulse * movementForceScale);
            //}
            //rigidbody.velocity = -Vector3.forward.normalized * maxSpeed + pushForce;
            finalVelocity.z -= maxSpeed;
        }

        finalVelocity = finalVelocity.normalized * maxSpeed*movementForceScale;

        rigidbody.velocity = finalVelocity+pushForce;

        
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
            focus = Mathf.Clamp(focus - (focusDecreaseRate * Time.deltaTime), 0f, MaxFocus);
            ui.SetFocus();
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
        ui.SetHealth();
    }

}
