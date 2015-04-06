using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController : MonoBehaviour
{

    public Text infoInputText;

    public float forward;
    public float backward;
    public float left;
    public float right;

    public float jump;

    public float force;
    public float bulletTime;
    public float shoot;

    public float drop;

    public float screenZoom;

    // Update is called once per frame
    void Update()
    {

        forward = Input.GetKey(KeyCode.W) ? 1 : Input.GetAxis("Horizontal");
        backward = Input.GetKey(KeyCode.S) ? 1 : Input.GetAxis("Horizontal");
        left = Input.GetKey(KeyCode.A) ? 1 : Input.GetAxis("Vertical");
        right = Input.GetKey(KeyCode.D) ? 1 : Input.GetAxis("Vertical");

        jump = Input.GetKeyDown(KeyCode.Space) ? 1 : Input.GetAxis("Jump");

        force = Input.GetMouseButton(1) ? 1 : Input.GetAxis("Force");
        bulletTime = Input.GetKeyDown(KeyCode.F) ? 1 : Input.GetAxis("BulletTime");
        shoot = Input.GetMouseButton(0) ? 1 : Input.GetAxis("Shoot");

        drop = Input.GetKeyDown(KeyCode.Q) ? 1 : Input.GetAxis("Drop");

        screenZoom = Input.GetAxis("MouseWheel");


        //forward    = Input.GetAxis("Horizontal");
        //backward   = Input.GetAxis("Horizontal");
        //left = Input.GetAxis("Vertical");
        //right = Input.GetAxis("Vertical");

        //jump = Input.GetAxis("Jump");

        //force = Input.GetAxis("Force");
        //bulletTime = Input.GetAxis("BulletTime");
        //shoot = Input.GetAxis("Shoot");

        //drop = Input.GetAxis("Drop");

        if (screenZoom == 0)
        {
            screenZoom = Input.GetAxis("ScreenZoom");
        }



        infoInputText.text = "forward :  " + forward + " backward :" + backward + " left : " + left + " right : " + right + " jump : " + jump + "\nforce : " + force + " bulletTime : " + bulletTime + " shoot : " + shoot + " drop : " + drop + "\nscreenZoom : " + screenZoom;
    }
}