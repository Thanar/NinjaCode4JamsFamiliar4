using UnityEngine;
using System.Collections;

public class PlayerController : Character {

    public UI ui;

    public float focus;
    public float focusDecreaseRate = 0.1f;
    public bool onFocus;


    public Vector3 newRotation = new Vector3(0,0,0);

    Vector3 rayVector = new Vector3();
    

    public float movementForceScale=1;

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
            rigidbody.AddForce(-Vector3.right.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector3.right.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(Vector3.forward.normalized * 1000 * movementForceScale);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(-Vector3.forward.normalized * 1000 * movementForceScale);
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
