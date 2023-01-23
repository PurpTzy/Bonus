using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    [SerializeField]
    Text EndScoreText;
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
    
    private void Awake()
    {
        EndScoreText.text = "Score: " + PlayerPrefs.GetInt("PlayerScore").ToString();
    }
    private void Update()
    {
    }
    
}
