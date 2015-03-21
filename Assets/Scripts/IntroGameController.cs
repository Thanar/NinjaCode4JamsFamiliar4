using UnityEngine;
using System.Collections;

public class IntroGameController : MonoBehaviour {

    public GameObject SpawnPoint;

    public bool ChooseYet = false;

    public PlayerController playerController;

    void Update()
    {
        if (ChooseYet)
        {
            playerController.ICanMove = true;
            SpawnPoint.SetActive(true);
        }


    }


}
