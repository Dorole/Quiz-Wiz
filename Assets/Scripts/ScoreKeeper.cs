using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int _correctAnswers = 0;
    int _questionsSeen = 0;
    [SerializeField] int _successThreshold = 70;

    public int correctAnswers
    {
        get { return _correctAnswers; }
        set { _correctAnswers = value; }
    }

    public int questionsSeen
    {
        get { return _questionsSeen; }
        set { _questionsSeen = value; }
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

    public bool IsSuccessful()
    {
        return CalculateScore() >= _successThreshold;
    }
}
