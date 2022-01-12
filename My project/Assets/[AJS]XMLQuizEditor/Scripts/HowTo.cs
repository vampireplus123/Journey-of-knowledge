using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTo : MonoBehaviour {

	void Start () {
        //This is all you need to setup/load the questions of XML file on your game. 
        XML_Manager.Load();
	}
	
	void Update () {
        //if you whant to get some question text and his answers you need some things
        //the index or the number of the question you whant form the XML
        //REMEMBER the array starts on 'ZERO' - (0).
        int index = 0;

        //So now you can you can return/get the texts inside other strings
        string questionText = XML_Manager.QUEST[index].text;
        string aboutTheQuestion = XML_Manager.QUEST[index].about;

        //When you got on the 'correctAwnswer' field, you need an INT 
        //value 0 = A; 1 = B; 2 = C; 4 = D; 5 = E;
        int correctAnswer = XML_Manager.QUEST[index].correctAnswer;

        //and tho get the answers text the code is a little bigger
        string a_awnserText = XML_Manager.QUEST[index].answer[0].text;
        string b_awnserText = XML_Manager.QUEST[index].answer[1].text;
        string c_awnserText = XML_Manager.QUEST[index].answer[2].text;
        string d_awnserText = XML_Manager.QUEST[index].answer[3].text;
        string e_awnserText = XML_Manager.QUEST[index].answer[4].text;

        //but if you whant to get all the answers in only one array, you can do that to.
        //and if you do that, you can acess the answers more easily using... "answers[index]"
        List<XML_Answer> answers = XML_Manager.QUEST[index].answer;

    }

}
