using UnityEngine;
using System.Collections;

public class IntroGameController : MonoBehaviour {

    public GameObject SpawnPoint;


    public PlayerController playerController;
    public Animator CameraAnimator;

    public void ChooseYet()
    {
        playerController.ICanMove = true;
        SpawnPoint.SetActive(true);
    }


    internal void Chosen()
    {
        if(playerController.Chosing == 3)
        {
            CameraAnimator.SetTrigger("Good Election");
        }else if (playerController.Chosing == 4)
        {
            CameraAnimator.SetTrigger("Bad Election");
        }



    }
}
