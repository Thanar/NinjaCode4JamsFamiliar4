using UnityEngine;
using System.Collections;

public class PlayerController : Character {

    public Animator animator;
    public IntroGameController introGameController;

    public UI ui;
    public float MaxFocus;
    public float focus;
    public float focusDecreaseRate = -0.1f;
    public bool onFocus;

    public Weapon weapon;
    public bool hasWeapon=false;

    Vector3 newRotation = new Vector3(0,0,0);

    Vector3 rayVector = new Vector3();
    

    public float movementForceScale=1f;

    public Transform fistsPosition;
    public Transform firstWeapon;


    public GameObject lollipopRed;
    public GameObject lollipopBlue;

    public AudioSource punch;
    public AudioSource punchHit;

    public AudioSource tiempoBala;
    public AudioSource sonidoPulso;

    public void DisableLollipops()
    {
        lollipopBlue.SetActive(false);
        lollipopRed.SetActive(false);
    }

    EnemyAI auxCharacter = null;


    Vector3 finalVelocity = new Vector3();

    public bool ICanMove = false;
    /// <summary>
    /// 0 - elegiendo| 1 - eligiendo ROJO | 2 - eligiendo AZUL | 3 - ROJO elegido | 4 - AZUL elegido
    /// </summary>
    public int Chosing = 0;

    public GameObject specialAttackEffectPrefab;

    public GameObject focusEffect;

    /// <summary>
    /// 0 - No | 1 - Forward | 2 - Right | 3 - Back | 4 - Left
    /// </summary>
    public int TypeJump = 0;

    public void setTypeJump(int value)
    {
        if (value >= 0 && value < 5)
        {
            TypeJump = value;
        }
        else
        {
            Debug.Log("ERROR TypeJump");
            TypeJump = 0;
        }
    }

    public bool LockRotation = false;

    public void EnableRotation()
    {
        Debug.Log("Desbloqueando la rotación");
        LockRotation = false;
    }
    public void DisableRotation()
    {
        Debug.Log("Bloqueando la rotación");
        LockRotation = true;
    }



    public AudioSource LookLollipoop;
    public AudioSource AcceptLollipoop;
    public AudioSource jumpsSound;

    public void PlayLookLollipop()
    {
        LookLollipoop.Play();
    }
    public void PlayAcceptLollipop()
    {
        AcceptLollipoop.Play();
    }



	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;

        ICanMove = false;
        Chosing = 0;

	    //CUANDO SE PONGAN LAS ARMAS QUITAR ESTA LINEA
        ui.ToggleAmmo(false);
        ui.SetHealth();
        ui.SetFocus();
	}

    void Update()
    {

        if (ICanMove)
        {

            if (health <= 0)
            {
                ICanMove = false;
                return;
            }


            // Si no está bloqueada la rotación
            if (LockRotation == false)
            {
                RaycastHit hit;
                rayVector = Input.mousePosition;
                rayVector.z = 10000;
                Ray ray = Camera.main.ScreenPointToRay(rayVector);
                if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Floor")))
                {
                    newRotation.y = Quaternion.LookRotation(hit.point - transform.position).eulerAngles.y;
                    transform.eulerAngles = newRotation;
                }
            }
            UpdateStatus();

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (onFocus)
                {
                    DeactivateFocus();
                }
                else
                {
                    ActivateFocus();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) && hasWeapon)
            {
                DropActualWeapon();
            }

            pushForce = Vector3.Lerp(pushForce, Vector3.zero, 0.05f);

            if (Input.GetMouseButton(0))
            {

                //Debug.Log("PJ Mouse 0");
                if (hasWeapon)
                {
                    weapon.Attack();
                    ui.SetAmmo(weapon);
                    if (weapon.depleted)
                    {
                        RemoveWeapon();
                    }else
                    if ((weapon).reloading)
                    {
                        ui.SetReloading((weapon).reloadTime);
                        (weapon).reloading = false;
                    }
                }
                else
                {
                    if (fistsTimeReady < Time.time)
                    {
                        punch.Play();
                        foreach (Collider c in Physics.OverlapSphere(fistsPosition.position, 0.5f))
                        {
                            auxCharacter = c.GetComponent<EnemyAI>();
                            if (auxCharacter)
                            {
                                punchHit.Play();
                                Instantiate(fistAttackEffectPrefab, fistsPosition.position, fistsPosition.rotation);
                                fistsTimeReady = Time.time + fistsCooldown;
                                auxCharacter.Damage(fistsDamage, fistsArmorPenetration);
                                //auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                                auxCharacter.Push(transform.forward.normalized * 10);

                            }
                        }

                        animator.SetTrigger("Punch");
                    }
                }

            }

            if (Input.GetMouseButtonDown(1))
            {
                if (focus > 0.2f)
                {
                    focus -= 0.2f;
                    ui.SetFocus();
                    Instantiate(specialAttackEffectPrefab, transform.position, Quaternion.identity);
                    Vector3 pushDirection;
                    sonidoPulso.Play();
                    foreach (Collider c in Physics.OverlapSphere(transform.position, 5f))
                    {
                        auxCharacter = c.GetComponent<EnemyAI>();
                        if (auxCharacter)
                        {
                            //auxCharacter.rigidbody.AddForce((transform.forward.normalized) * 8, ForceMode.VelocityChange);
                            pushDirection = (auxCharacter.transform.position - transform.position);
                            pushDirection.y = 0;
                            pushDirection = pushDirection.normalized * 30;
                            auxCharacter.Push(pushDirection);
                            auxCharacter.Damage(fistsDamage * 5, fistsArmorPenetration * 2);
                        }
                        else
                        {
                            if (c.rigidbody && !c.rigidbody.isKinematic)
                            {
                                c.rigidbody.AddForce((c.transform.position - transform.position) * 10, ForceMode.VelocityChange);
                            }
                        }
                    }
                }
            }
        }
        else if(Chosing == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("Roja");

                Chosing = 1;

            }

            if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("Azul");
                Chosing = 2;
            }
        }
        else if (Chosing == 1 || Chosing == 2)
        {

            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("Roja");

                Chosing = 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("Azul");
                Chosing = 2;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("SPAAAACE");
                Chosing += 2;
            }
        }
        else if (Chosing == 3 || Chosing == 4)
        {
            //Debug.Log("CHONSEN");
            introGameController.Chosen();
        }
    }


    void FixedUpdate()
    {
        if (ICanMove)
        {

            if (health <= 0)
            {
                ICanMove = false;
                return;
            }

            if (!LockRotation) 
            {

                finalVelocity.Set(0, 0, 0);
                if (Input.GetKey(KeyCode.A))
                {
                    //if (Vector3.Project(rigidbody.velocity, -Vector3.right).magnitude < maxSpeed)
                    //{
                    //    rigidbody.AddForce(-Vector3.right.normalized * charachterImpulse * movementForceScale);
                    //}

                    //rigidbody.velocity = -Vector3.right.normalized * maxSpeed + pushForce;
                    finalVelocity.x -= maxSpeed;
                    animator.SetTrigger("Moving");
                    

                    if (Input.GetKey(KeyCode.Space))
                    {
                        LeftJumpAnimation();
                    }
                }

                if (Input.GetKey(KeyCode.D))
                {
                    //if (Vector3.Project(rigidbody.velocity, Vector3.right).magnitude < maxSpeed)
                    //{
                    //    rigidbody.AddForce(Vector3.right.normalized * charachterImpulse * movementForceScale);
                    //}
                    //rigidbody.velocity = Vector3.right.normalized * maxSpeed + pushForce;
                    finalVelocity.x += maxSpeed;

                    animator.SetTrigger("Moving");


                    if (Input.GetKey(KeyCode.Space))
                    {
                        RightJumpAnimation();
                    }
                }

                if (Input.GetKey(KeyCode.W))
                {
                    //if (Vector3.Project(rigidbody.velocity, Vector3.forward).magnitude < maxSpeed)
                    //{
                    //    rigidbody.AddForce(Vector3.forward.normalized * charachterImpulse * movementForceScale);
                    //}
                    //rigidbody.velocity = Vector3.forward.normalized * maxSpeed + pushForce;
                    finalVelocity.z += maxSpeed;

                    animator.SetTrigger("Moving");


                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log("JUMP!");
                        ForwardJumpAnimation();
                    }
                }

                if (Input.GetKey(KeyCode.S))
                {
                    //if (Vector3.Project(rigidbody.velocity, -Vector3.forward).magnitude < maxSpeed)
                    //{
                    //    rigidbody.AddForce(-Vector3.forward.normalized * charachterImpulse * movementForceScale);
                    //}
                    //rigidbody.velocity = -Vector3.forward.normalized * maxSpeed + pushForce;
                    finalVelocity.z -= maxSpeed;

                    animator.SetTrigger("Moving");


                    if (Input.GetKey(KeyCode.Space))
                    {
                        BackJumpAnimation();
                    }
                }

                if (finalVelocity == Vector3.zero)
                {
                    animator.SetTrigger("Idle");
                }

                finalVelocity = finalVelocity.normalized * maxSpeed * movementForceScale;

                rigidbody.velocity = finalVelocity + pushForce + rigidbody.velocity.y*Vector3.up;
            }
        }
    }

    public void ActivateFocus()
    {
        onFocus = true;

        Time.timeScale = 0.3f;
        movementForceScale = 1.7f;

        focusEffect.SetActive(true);

        tiempoBala.Play();
    }

    public void DeactivateFocus()
    {

        focusEffect.SetActive(false);
        onFocus = false;

        Time.timeScale = 1f;
        movementForceScale = 1f;

        tiempoBala.Stop();
    }

    void UpdateStatus()
    {
        if (onFocus)
        {
            AddFocus(focusDecreaseRate * Time.deltaTime);
            if (focus <= 0)
            {
                focus = 0;
                ui.SetFocus();
                DeactivateFocus();
            }
        }
        else
        {
            AddFocus(-focusDecreaseRate * Time.deltaTime);
        }
    }

    public void AddFocus(float value)
    {
        focus = Mathf.Clamp(focus + value, 0f, 1f);
        ui.SetFocus();
    }

    public void AddHealth(float value)
    {
        health = Mathf.Clamp(health + value, 0, 100);
        ui.SetHealth();
    }

    public override void Damage(float damage, float armorPenetration = 0)
    {
        base.Damage(damage, armorPenetration);
        ui.DamageReceived();
        ui.SetHealth();
    }



    public void EquipThis(Weapon drop)
    {
        if (!hasWeapon)
        {

            drop.Taken();
            
            weapon = drop;
            weapon.gameObject.GetComponent<AutoRotation>().rotate = false;
            hasWeapon = true;
            ui.ToggleAmmo(true);
            ui.SetAmmo(weapon);
            weapon.transform.parent = firstWeapon;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = Vector3.one;
            //weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.rotation = firstWeapon.transform.rotation;
            //weapon.transform.eulerAngles = new Vector3(0, -90, 0);
            weapon.laserSightActivate = true;
            

            animator.SetTrigger("Weapon");

        }
    }

    public void DropActualWeapon()
    {
        if (hasWeapon)
        {
            weapon.Dropped();
            weapon.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            weapon.transform.position -= transform.forward;
            weapon.laserSightActivate = false;
            weapon = null;
            hasWeapon = false;
            ui.ToggleAmmo(false);

            animator.SetTrigger("NoWeapon");
        }
    }

    public override void Die()
    {
        Time.timeScale = 0.3f;
        //DropActualWeapon();
        animator.SetTrigger("Fin");
    }

    public void flai()
    {
        Time.timeScale = 1f;
        rigidbody.AddForce(Vector3.up * 100,ForceMode.VelocityChange);
    }

    public void loadGameOver()
    {
        Application.LoadLevel("GameOver");
    }
    public void RemoveWeapon()
    {
        hasWeapon = false;
        weapon.laserSightActivate = false;
        Destroy(weapon.gameObject);
        weapon = null;
    }



    #region Animaciones


    public void LeftJumpAnimation()
    {

        // A + Space

        jumpsSound.Play();


        Push(Vector3.left);
        // Adelante
        if (gameObject.transform.rotation.eulerAngles.y >= 315 || gameObject.transform.rotation.eulerAngles.y <= 45)
        {
            //Push(Vector3.left);
            animator.SetTrigger("LeftJump");
        }
        // Derecha
        else if (gameObject.transform.rotation.eulerAngles.y >= 45 && gameObject.transform.rotation.eulerAngles.y <= 135)
        {
            //Push(Vector3.back);
            animator.SetTrigger("BackJump");
        }
        // Atras
        else if (gameObject.transform.rotation.eulerAngles.y >= 135 && gameObject.transform.rotation.eulerAngles.y <= 225)
        {
            //Push(Vector3.right);
            animator.SetTrigger("RightJump");
        }
        // Izquierda
        else if (gameObject.transform.rotation.eulerAngles.y >= 225 && gameObject.transform.rotation.eulerAngles.y <= 315)
        {
            //Push(Vector3.forward);
            animator.SetTrigger("ForwardJump");
        }
    }

    public void RightJumpAnimation()
    {
        // D + Space

        jumpsSound.Play();
        Push(Vector3.right);
        // Adelante
        if (gameObject.transform.rotation.eulerAngles.y >= 315 || gameObject.transform.rotation.eulerAngles.y <= 45)
        {
            //Push(Vector3.right);
            animator.SetTrigger("RightJump");
        }
        // Derecha
        else if (gameObject.transform.rotation.eulerAngles.y >= 45 && gameObject.transform.rotation.eulerAngles.y <= 135)
        {
            //Push(Vector3.forward);
            animator.SetTrigger("ForwardJump");
        }
        // Atras
        else if (gameObject.transform.rotation.eulerAngles.y >= 135 && gameObject.transform.rotation.eulerAngles.y <= 225)
        {
            //Push(Vector3.left);
            animator.SetTrigger("LeftJump");
        }
        // Izquierda
        else if (gameObject.transform.rotation.eulerAngles.y >= 225 && gameObject.transform.rotation.eulerAngles.y <= 315)
        {
            //Push(Vector3.back);
            animator.SetTrigger("BackJump");
        }
    }

    public void BackJumpAnimation()
    {

        // S + Space


        jumpsSound.Play();
        Push(Vector3.back);
        // Adelante
        if (gameObject.transform.rotation.eulerAngles.y >= 315 || gameObject.transform.rotation.eulerAngles.y <= 45)
        {
            //Push(Vector3.back);
            animator.SetTrigger("BackJump");
        }
            // Derecha
        else if (gameObject.transform.rotation.eulerAngles.y >= 45 && gameObject.transform.rotation.eulerAngles.y <= 135)
        {
            //Push(Vector3.right);
            animator.SetTrigger("RightJump");
        }
            // Atras
        else if (gameObject.transform.rotation.eulerAngles.y >= 135 && gameObject.transform.rotation.eulerAngles.y <= 225)
        {
            //Push(Vector3.forward);
            animator.SetTrigger("ForwardJump");
        }
            // Izquierda
        else if (gameObject.transform.rotation.eulerAngles.y >= 225 && gameObject.transform.rotation.eulerAngles.y <= 315)
        {
            //Push(Vector3.left);
            animator.SetTrigger("LeftJump");
        }
    }

    public void ForwardJumpAnimation()
    {
        
        // W + Space


        jumpsSound.Play();
        Push(Vector3.forward);
        // Adelante
        if (gameObject.transform.rotation.eulerAngles.y >= 315 || gameObject.transform.rotation.eulerAngles.y <= 45)
        {
            //Push(Vector3.forward);
            animator.SetTrigger("ForwardJump");
        }
            // Derecha
        else if (gameObject.transform.rotation.eulerAngles.y >= 45 && gameObject.transform.rotation.eulerAngles.y <= 135)
        {
            //Push(Vector3.left);
            animator.SetTrigger("LeftJump");
        }
            // Atras
        else if (gameObject.transform.rotation.eulerAngles.y >= 135 && gameObject.transform.rotation.eulerAngles.y <= 225)
        {
            //Push(Vector3.back);
            animator.SetTrigger("BackJump");
        }
            // Izquierda
        else if (gameObject.transform.rotation.eulerAngles.y >= 225 && gameObject.transform.rotation.eulerAngles.y <= 315)
        {
            //Push(Vector3.right);
            animator.SetTrigger("RightJump");
        }
    }


    #endregion



}
