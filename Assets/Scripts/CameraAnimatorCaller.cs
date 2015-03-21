using UnityEngine;
using System.Collections;

public class CameraAnimatorCaller : MonoBehaviour {

    public PlayerController playerController;

    public float sensitivity= 0.1f;

    public void FinishGoodElectionAnimation()
    {
        playerController.ICanMove = true;
        playerController.DisableLollipops();

        playerController.introGameController.ChooseYet = true;
    }


    public void FinishBadElectionAnimation()
    {

    }

    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("MouseWheel") * sensitivity;
    }
}
