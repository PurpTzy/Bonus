using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text ScoreText;
    public int Score;

    private void Awake()
    {
       LoadData();
    }

    public void LoadData()
    {
        if(PlayerPrefs.HasKey("PlayerX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"),PlayerPrefs.GetFloat("PlayerY"),PlayerPrefs.GetFloat("PlayerZ"));
            Score =  PlayerPrefs.GetInt("PlayerScore");
            ScoreText.text = Score.ToString("000,000,000");
            
        }
    }

    public void EditScore(int amount)
    {   
        Score += amount;
        PlayerPrefs.SetInt("PlayerScore", GetComponent<PlayerStats>().Score);
        ScoreText.text = Score.ToString("000,000,000");
    }
}
