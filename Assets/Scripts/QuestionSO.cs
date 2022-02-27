using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string _question = "Enter new question text here";
    [SerializeField] string[] _answers = new string[4];
    [SerializeField] int _correctAnswerIndex;

    public string GetQuestion()
    {
        return _question;
    }

    public int GetCorrectAnswerIndex()
    {
        return _correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return _answers[index];
    }
}
