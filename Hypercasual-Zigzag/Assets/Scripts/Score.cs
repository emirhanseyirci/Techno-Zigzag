using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public  int score;
    public TextMeshProUGUI scoretext;


    void Start()
    {
        score = 0;

    }

    void Update()
    {
        scoretext.text = score.ToString();

    }
}
