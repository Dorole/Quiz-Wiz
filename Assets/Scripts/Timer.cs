using UnityEngine;

public class Timer : MonoBehaviour
{
    [HideInInspector] public bool loadNextQuestion;
    [HideInInspector] public float fillFraction; //leave the graphic rep. here!

    float _timerValue;
    [SerializeField] float _timeToAnswerQuestion = 30f;
    [SerializeField] float _timeToReviewAnswer = 10f;

    bool _isAnsweringQuestion;
    public bool isAnsweringQuestion { get { return _isAnsweringQuestion; } }

    private void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer() //implement state pattern
    {
        _timerValue -= Time.deltaTime;

        if (_isAnsweringQuestion)
        {
            if (_timerValue > 0)
                fillFraction = _timerValue / _timeToAnswerQuestion;
            else
            {
                _isAnsweringQuestion = false;
                _timerValue = _timeToReviewAnswer;
            }
        }
        else
        {
            if (_timerValue > 0)
                fillFraction = _timerValue / _timeToReviewAnswer;
            else
            {
                _isAnsweringQuestion = true;
                _timerValue = _timeToAnswerQuestion;
                loadNextQuestion = true;
            }
        }    
    }

    public void CancelTimer()
    {
        _timerValue = 0;
    }
}
