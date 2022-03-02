using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private GameObject scoreParent;

    private readonly float _scoreTime = 0.5f;
    private float _scoreTimer;
    
    void Update()
    {
        if(Time.time >= _scoreTimer)
        {
            UpdateScores();
            _scoreTimer = Time.time + _scoreTime;
        }
    }

    private void UpdateScores()
    {
        foreach (var score in FindObjectsOfType<Score>())
        {
            Destroy(score.gameObject);
        }
        
        foreach (var player in FindObjectsOfType<Player>())
        {
            Score score = Instantiate(scorePrefab, scoreParent.transform).GetComponent<Score>();
            score.NameText.text = player.PlayerName;
            score.ScoreText.text = player.Score.ToString();
        }
    }
}
