using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz _quiz;
    EndScreen _endScreen;

    void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
        _endScreen = FindObjectOfType<EndScreen>();
    }

    private void Start()
    {
        _endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_quiz.isComplete)
        {
            _endScreen.gameObject.SetActive(true);
            _quiz.gameObject.SetActive(false);
            _endScreen.ShowFinalScore();
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
