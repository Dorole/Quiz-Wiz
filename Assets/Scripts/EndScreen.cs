using UnityEngine;
using TMPro;
using System;

public class EndScreen : MonoBehaviour
{
    public static event Action onQuizCompleted;

    ScoreKeeper _scoreKeeper;
    [SerializeField] TextMeshProUGUI _finalScoreText;

    [TextArea(2,3)]
    [SerializeField] string _successMessage;

    [TextArea(2, 3)]
    [SerializeField] string _failMessage;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        onQuizCompleted?.Invoke();
    }

    public void ShowFinalScore()
    {
        if (_scoreKeeper.IsSuccessful())
            _finalScoreText.text = $"{_successMessage}{_scoreKeeper.CalculateScore()}%.";
        else
            _finalScoreText.text = $"{_failMessage}{_scoreKeeper.CalculateScore()}%.";
    }
}
