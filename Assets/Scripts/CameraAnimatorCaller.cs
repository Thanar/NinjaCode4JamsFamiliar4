using UnityEngine;
using System.Collections;

public class CameraAnimatorCaller : MonoBehaviour {

    public PlayerController playerController;

    public float sensitivity= 0.0001f;

    public void FinishGoodElectionAnimation()
    {
        playerController.ICanMove = true;
        playerController.DisableLollipops();

        playerController.introGameController.ChooseYet();
    }


    public void FinishBadElectionAnimation()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("MouseWheel") * sensitivity;
    }
}
