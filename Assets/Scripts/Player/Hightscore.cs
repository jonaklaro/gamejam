using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hightscore : MonoBehaviour
{
    int highscore = 0;

    [SerializeField] TextMeshProUGUI highscoreText;

    void Update()
    {
        highscore = (int)(Time.time * 10);
        highscoreText.text = highscore.ToString();
    }

    public void KilledEnemy()
    {
        highscore += 50;
    }
}
