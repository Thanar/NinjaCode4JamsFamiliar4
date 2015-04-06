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

    public GameJoltAPIManager GJManager;

    public GameObject newScoreText;

    public Text player1;
    public Text rounds1;
    public Text kills1;

    public Text player2;
    public Text rounds2;
    public Text kills2;

    public Text player3;
    public Text rounds3;
    public Text kills3;

    public Text yourPlayer;
    public Text yourRounds;
    public Text yourKills;

    public bool newScore;

    int lastHS;
    int rounds;
    int kills;

    public bool verified;

    void Start()
    {
        GJManager = GameObject.FindGameObjectWithTag("GJAPI").GetComponent<GameJoltAPIManager>();
        if (GJManager != null)
        {
            verified = GJManager.verified;
        }
        
        lastHS = 0;
        rounds = 0;
        kills = 0;

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
        if (verified)
        {
            GJAPI.Scores.Get();
            GJAPI.Scores.GetMultipleCallback += OnReceivedHighScore;
            yourPlayer.text = GJAPI.User.Name; 
        }

        yourRounds.text = rounds.ToString();
        yourKills.text = kills.ToString();
        //score.text = "HIGHSCORE: " + lastHS + "\nROUNDS: " + rounds + "\nKILLS: " + kills;

    }

    

    void OnReceivedHighScore(GJScore[] scores)
    {

        GJAPI.Scores.Add(rounds.ToString(), (uint)kills);
        if (scores.Length > 0)
        {
            player1.text = scores[0].Username;
            rounds1.text = scores[0].Score;
            kills1.text = scores[0].Sort.ToString();
        }

        if (scores.Length > 1)
        {
            player2.text = scores[1].Username;
            rounds2.text = scores[1].Score;
            kills2.text = scores[1].Sort.ToString();
        }

        if (scores.Length > 2)
        {
            player3.text = scores[2].Username;
            rounds3.text = scores[2].Score;
            kills3.text = scores[2].Sort.ToString();
        }


    }


    void OnDestroy()
    {
        PlayerPrefs.SetInt("kills", 0);
        PlayerPrefs.SetInt("rounds", 0);
    }


}
