using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scores : MonoBehaviour
{

    /*
     * PlayerPrefts keys
     * 
     * Ultimo record - "lastHS"
     * Rondas de esta partida - "rounds"
     * Enemigos muertos - "kills"
     * 
     */

    public Text score;
    public GameObject newScoreText;

    public Text score1;
    public Text score2;
    public Text score3;

    public Text yourScore;

    public bool newScore;

    void Start()
    {
        int lastHS = 0;
        int rounds = 0;
        int kills = 0;

        if (PlayerPrefs.HasKey("lastHS"))
        {
            lastHS = PlayerPrefs.GetInt("lastHS");
        }

        if (PlayerPrefs.HasKey("rounds"))
        {
            rounds = PlayerPrefs.GetInt("rounds");
        }

        if (PlayerPrefs.HasKey("kills"))
        {
            kills = PlayerPrefs.GetInt("kills");
        }

        if (rounds > lastHS)
        {
            newScore = true;
            lastHS = rounds;
            PlayerPrefs.SetInt("lastHS", lastHS);
        }

        if (newScore)
        {
            newScoreText.SetActive(true);
        }
        else
        {
            newScoreText.SetActive(false);
        }

        GJAPI.Scores.Add(kills.ToString(), (uint)kills);

        GJAPI.Scores.Get();
        GJAPI.Scores.GetMultipleCallback += OnReceivedHighScore;

        //score.text = "HIGHSCORE: " + lastHS + "\nROUNDS: " + rounds + "\nKILLS: " + kills;

    }

    

    void OnReceivedHighScore(GJScore[] scores)
    {
        score1.text = "User " + scores[0].Username + " Score " + scores[0].Score;
        score2.text = "User " + scores[1].Username + " Score " + scores[1].Score;
        score3.text = "User " + scores[2].Username + " Score " + scores[2].Score;

        yourScore.text = "User " + GJAPI.User.Name + " Score " + kills;
    }


    void OnDestroy()
    {
        PlayerPrefs.SetInt("kills", 0);
        PlayerPrefs.SetInt("rounds", 0);
    }


}
