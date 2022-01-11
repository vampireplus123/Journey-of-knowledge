using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    GameObject player;
    GameObject enemy;

    GameObject canva;

    public int seconds = 30;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        canva = GameObject.FindGameObjectWithTag("Canvas");
    }

    public Quizmanager quizmanager;    
    // Start is called before the first frame update
    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct answer");
            player.GetComponent<Movement>().enabled = true;
            canva.SetActive(false);
            StartCoroutine(waitfor());
            quizmanager.Correct();
        }
        else
        {
            Debug.Log("Wrong answer");
            canva.SetActive(false);
            enemy.GetComponent<Chase>().enabled = true;
            StartCoroutine(waitfor());
            quizmanager.Correct();
        }
    }
    public IEnumerator waitfor()
    {
        yield return new WaitForSeconds(seconds);
        canva.SetActive(true);
    }
}
