using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quizmanager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;

    public GameObject[] options;

    public int currentQuestion;
    
    public Text QuestionText;

    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestion();
    }

    // Update is called once per frame
    public void Correct()
    {
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }
    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answer[i];

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }
    }
    void GenerateQuestion()
    {
        currentQuestion = Random.Range(0,QnA.Count);
        QuestionText.text = QnA[currentQuestion].Question;
        SetAnswer();
    }
}
