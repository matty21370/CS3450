using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text scoreText;

    public Text NameText => nameText;
    public Text ScoreText => scoreText;
}
