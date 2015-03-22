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

        score.text = "HIGHSCORE: " + lastHS + "\nROUNDS: " + rounds + "\nKILLS: " + kills;


    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("kills", 0);
        PlayerPrefs.SetInt("rounds", 0);
    }


}
