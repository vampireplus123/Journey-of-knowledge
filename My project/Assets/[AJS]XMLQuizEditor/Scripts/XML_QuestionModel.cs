using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

[System.Serializable]
public class XML_QuestionModel {

    [XmlElement("toAsk")]
    public List<XML_Questions> questions = new List<XML_Questions>();

}

[System.Serializable]
public class XML_Questions {

    [XmlAttribute("QuestionText")]
    public string text;

    [XmlAttribute("about")]
    public string about;

    [XmlAttribute("correctAnswer")]
    public int correctAnswer;

    [XmlElement("answer")]
    public List<XML_Answer> answer = new List<XML_Answer>();

    public XML_Questions() {
        text = "";
        about = "";
        correctAnswer = 0;
    }


}

[System.Serializable]
public class XML_Answer {
    [XmlText]
    public string text;

    public XML_Answer() {
        text = "";
    }

    public XML_Answer(string txt) {
        text = txt;
    }

}