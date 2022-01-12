using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public static class XML_Manager {

    public static List<XML_Questions> Q { get { return questions.questions; } set { questions.questions = value; } }
    public static List<XML_Questions> QUEST { get { return questions.questions; } set { questions.questions = value; } }
    public static XML_QuestionModel questions = new XML_QuestionModel();

    private static string fileName = "[AJS]XMLQuizEditor";
    private static string filePath = Application.dataPath + "/[AJS]XMLQuizEditor/Resources/" + fileName + ".xml";

    public static void Save() {

        var serializer = new XmlSerializer(typeof(XML_QuestionModel));
        var encoding = Encoding.GetEncoding("UTF-8");

        using (StreamWriter stream = new StreamWriter(filePath, false, encoding)) {
            serializer.Serialize(stream, questions);
        }

        Debug.Log("[AJS] FILE SAVED. filePath -> " + filePath);
    }

    public static void Load() {
        Debug.Log("[AJS] LOADING AND IMPORTING...");
        XmlSerializer serializer = new XmlSerializer(typeof(XML_QuestionModel));

        var xmlFile = Resources.Load<TextAsset>(fileName);

        if (xmlFile) {

            questions = serializer.Deserialize(GenerateStreamFromString(xmlFile.text)) as XML_QuestionModel;
            Debug.Log("[AJS] IMPORTED WITH SUCCESS. :)");

        } else {
            Debug.LogError("[AJS] IMPORTING FAILED ... File doesen't exist. :/");
        }

    }

    public static Stream GenerateStreamFromString(string s) {
        return new MemoryStream(Encoding.UTF8.GetBytes(s));
    }


}

