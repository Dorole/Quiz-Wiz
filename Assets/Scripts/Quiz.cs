using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO _currentQuestion;
    [SerializeField] List<QuestionSO> _questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI _questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] _answerButtons; 
    int _correctIndex;
    bool _hasAnswered = true;

    [Header("Buttons")]
    Image _buttonImage;
    [SerializeField] Sprite _defaultAnswerSprite;
    [SerializeField] Sprite _correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image _timerImage;
    Timer _timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI _scoreText;
    ScoreKeeper _scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider _progressBar;
    [HideInInspector] public bool isComplete;

    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();

        _progressBar.maxValue = _questions.Count;
        _progressBar.value = 0;
    }

    private void Update()
    {
        _timerImage.fillAmount = _timer.fillFraction; //why this here??

        if (_timer.loadNextQuestion) //dislike 
        {
            if (_progressBar.value == _progressBar.maxValue)
            {
                isComplete = true;
                return;
            }

            _hasAnswered = false;
            GetNextQuestion();
        }
        else if (!_hasAnswered && !_timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
        }
    }
    
    void DisplayQuestion()
    {
        _questionText.text = _currentQuestion.GetQuestion();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _currentQuestion.GetAnswer(i);
        }
    }
    
    void DisplayAnswer(int selectedIndex)
    {
        _correctIndex = _currentQuestion.GetCorrectAnswerIndex();

        if (selectedIndex == _correctIndex)
        {
            _questionText.text = "A person of culture, I see!";
            _buttonImage = _answerButtons[selectedIndex].GetComponent<Image>();
            _scoreKeeper.correctAnswers++;
        }
        else
        {
            string correctAnswer = _currentQuestion.GetAnswer(_correctIndex);
            _questionText.text = "One does not simply click whatever. \nIt's " + correctAnswer + "!";

            _buttonImage = _answerButtons[_correctIndex].GetComponent<Image>();
        }

        _buttonImage.sprite = _correctAnswerSprite;
        SetButtonState(false);
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            Button button = _answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void OnAnswerSelected(int selectedIndex)
    {
        _hasAnswered = true;
        DisplayAnswer(selectedIndex);
        _timer.CancelTimer();
        DisplayScore();
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, _questions.Count);
        _currentQuestion = _questions[index];

        if (_questions.Contains(_currentQuestion))
            _questions.Remove(_currentQuestion);
    }

    void GetNextQuestion()
    {
        if (_questions.Count <= 0)
        {
            _timer.CancelTimer();
            return;
        }

        _timer.loadNextQuestion = false; //dislike
        SetButtonState(true);
        SetDefaultButtonSprite();
        GetRandomQuestion();
        DisplayQuestion();
        IncrementProgressBar();
        _scoreKeeper.questionsSeen++;
    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            _buttonImage = _answerButtons[i].GetComponent<Image>();
            _buttonImage.sprite = _defaultAnswerSprite;
        }
    }

    void DisplayScore()
    {
        _scoreText.text = $"Score: {_scoreKeeper.CalculateScore()}%";
    }

    void IncrementProgressBar()
    {
        _progressBar.value++;
    }
}
