using UnityEngine;
using System.Collections;

public class CameraAnimatorCaller : MonoBehaviour {

    public PlayerController playerController;

    public float sensitivity= 0.001f;

    public void FinishGoodElectionAnimation()
    {
        playerController.ICanMove = true;
        playerController.DisableLollipops();

        playerController.introGameController.ChooseYet();

        this.GetComponent<Animator>().enabled = false;

        

    }

    public void PlayPlayerAcceptLollipoop()
    {
        playerController.PlayAcceptLollipop();
    }
    public void FinishBadElectionAnimation()
    {
        Application.LoadLevel(2);
    }

    void Update()
    {
        transform.position += transform.forward * playerController.inputController.screenZoom * sensitivity;
    }
}
