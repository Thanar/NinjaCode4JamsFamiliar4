using UnityEngine;
using System.Collections;

public class GameJoltAPIManager : MonoBehaviour
{

    public int gameID;
    public string privateKey;
    public string userName;
    public string userToken;

    public bool verified;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GJAPI.Init(gameID, privateKey);
        GJAPI.Users.VerifyCallback += OnVerifyUser;
        //GJAPI.Users.Verify(userName, userToken);
    }

    void Start()
    {
        Application.ExternalCall("GJAPI_AuthUser", gameObject.name, "LoginUser");
    }

    void OnEnable()
    {
        GJAPI.Users.VerifyCallback += OnVerifyUser;
    }

    void OnDisable()
    {
        GJAPI.Users.VerifyCallback -= OnVerifyUser;
    }

    void OnVerifyUser(bool success)
    {
        if (success)
        {
            Debug.Log("Yepee!");
            verified = true;
        }
        else
        {
            Debug.Log("Um... Something went wrong.");
            verified = false;
        }
    }



    public void LoginUser(string response)
    {
        string[] splittedResponse = response.Split(':');
        string user = splittedResponse[0];
        string token = splittedResponse[1];
        // Do whatever you want with it.
        this.userName = user;
        this.userToken = token;
        GJAPI.Users.Verify(userName, userToken);
    }



}
