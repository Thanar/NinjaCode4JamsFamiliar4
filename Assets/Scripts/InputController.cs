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

    public float mouseX;
    public float mouseY;

    // Update is called once per frame
    void Update()
    {
       
        forward =   Input.GetKey(KeyCode.W) ? 1 :  Input.GetAxis("Vertical") > 0   ?  Input.GetAxis("Vertical")  : 0;
        Debug.Log(forward);
        backward =  Input.GetKey(KeyCode.S) ? 1 : -Input.GetAxis("Vertical") > 0   ? -Input.GetAxis("Vertical")  : 0;
        left =      Input.GetKey(KeyCode.A) ? 1 : -Input.GetAxis("Horizontal") > 0 ? -Input.GetAxis("Horizontal"): 0;
        right =     Input.GetKey(KeyCode.D) ? 1 :  Input.GetAxis("Horizontal") > 0 ?  Input.GetAxis("Horizontal"): 0;

        jump = Input.GetKeyDown(KeyCode.Space) ? 1 : Input.GetButton("Jump")? 1 : 0;

        force = Input.GetMouseButton(1) ? 1 : Input.GetButton("Force") ? 1 : 0;
        bulletTime = Input.GetKeyDown(KeyCode.F) ? 1 : Input.GetButton("BulletTime") ? 1 : 0;
        shoot = Input.GetMouseButton(0) ? 1 : Input.GetButton("Shoot") ? 1 : 0;

        drop = Input.GetKeyDown(KeyCode.Q) ? 1 : Input.GetButton("Drop") ? 1 : 0;

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

        mouseX = Input.GetAxis("MouseX");
        mouseY = Input.GetAxis("MouseY");


        infoInputText.text = "forward :  " + forward + " backward :" + backward + " left : " + left + " right : " + right + " jump : " + jump + "\nforce : " + force + " bulletTime : " + bulletTime + " shoot : " + shoot + " drop : " + drop + "\nscreenZoom : " + screenZoom + " MouseX: " + mouseX + " MouseY: " + mouseY;
    }
}