/*
 * Last Update: Sept 29, 2021
 * 
 * Desc: Game Manager to manage da game
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // vars
    #region GameManager Singleton
    static GameManager gm; 
    public static GameManager GM { get { return gm; } }

    void CheckGameManagerIsInScene()
    {
        if (gm == null) gm = this;
        else Destroy(this.gameObject);
    }
    #endregion

    public static int Score;
    public string scorePrefix = string.Empty;
    public string healthPrefix = string.Empty;
    public TMP_Text ScoreText = null;
    public TMP_Text HealthText = null;
    public TMP_Text GameOverText = null;
    public static bool isPlayerDead = false;
    public GameObject Player = null;

    void Awake()
    {
        CheckGameManagerIsInScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreText != null)
        {
            ScoreText.text = scorePrefix + Score.ToString();
        }
        if (HealthText != null)
        {
            if (Player != null) HealthText.text = healthPrefix + Player.gameObject.GetComponent<Health>().hp.ToString();
            else HealthText.text = "Health: 0";
        }
    }

    public static void GameOver()
    {
        if (gm.GameOverText != null)
        {
            gm.GameOverText.gameObject.SetActive(true);
            gm.GameOverText.text = "GAME OVER\n<size=48>Score: " + Score+"</size>";
        }

        if (isPlayerDead)
        {
            gm.GameOverText.gameObject.SetActive(true);
            gm.Player.SetActive(false);
        }
    }
}
