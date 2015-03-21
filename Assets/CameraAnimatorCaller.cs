using UnityEngine;
using System.Collections;

public class CameraAnimatorCaller : MonoBehaviour {

    public PlayerController playerController;

    public void FinishGoodElectionAnimation()
    {
        playerController.ICanMove = true;
    }


    public void FinishBadElectionAnimation()
    {

    }
}
