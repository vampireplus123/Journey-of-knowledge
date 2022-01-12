#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EzEditor;

public class QuizWindow: EditorWindow {

    #region EditorVariables
    int isOpenQuestion = 0;
    List<string> HeaderButtons = new List<string>();

    Rect bgRect = new Rect();
    Color btn = new Color(0, 0.8f, 0.8f, 0.8f);
    Texture2D bgTexture, apply, erase, save;
    #endregion

    #region QuestionsVariables
    int correct_awnser, StartShowingOn;
    string theQuestion, aboutMe;
    string a_awnser, b_awnser, c_awnser, d_awnser, e_awnser;
    string[] answers = { "A", "B", "C", "D", "E" };
    #endregion

    #region GUIStyles
    private GUIStyle CreateStyle(TextAnchor textAnchor, FontStyle fontStyle, int fontSize) {
        GUIStyle newFont = new GUIStyle(GUI.skin.label) {
            alignment = textAnchor,
            fontStyle = fontStyle,
            fontSize = fontSize
        };

        newFont.normal.textColor = Color.white;
        return newFont;
    }
    private GUIStyle TextArea {
        get {
            GUIStyle TextArea = new GUIStyle(GUI.skin.textArea);
            TextArea.normal.textColor = Color.white;
            TextArea.focused.textColor = Color.yellow;
            return TextArea;
        }
    }
    private GUIStyle TitleStyle { get { return CreateStyle(TextAnchor.MiddleCenter, FontStyle.Bold, 25); } }
    #endregion

    [MenuItem("AJS Tools/XML QUIZ EDITOR")]
    static void OpenWindow() {
        QuizWindow window = (QuizWindow)GetWindow(typeof(QuizWindow));
        window.minSize = new Vector2(800, 500);
        window.maxSize = new Vector2(800, 500);
        window.Show();
    }

    private void OnEnable() {
        //Loading Textures
        bgTexture = Resources.Load("BG", typeof(Texture2D)) as Texture2D;
        apply = Resources.Load("apply", typeof(Texture2D)) as Texture2D;
        erase = Resources.Load("erase", typeof(Texture2D)) as Texture2D;
        save = Resources.Load("save", typeof(Texture2D)) as Texture2D;

        XML_Manager.Load();
        AJS_UpdateAndSelect(0);
        StartShowingOn = 0;
    }

    private int QuestionsBlock() {
        if (gui.EzButton(gui.MoveUpButton, GUILayout.MaxWidth(40), GUILayout.Height(18f))) {  //Width 44
            if (StartShowingOn > 0) {
                StartShowingOn--;
                AJS_UpdateAndSelect(isOpenQuestion);
            }
        }

        int pressed = GUILayout.SelectionGrid(isOpenQuestion, HeaderButtons.ToArray(), 1, GUILayout.MaxWidth(44));

        if (gui.EzButton(gui.AddButton, GUILayout.MaxWidth(44), GUILayout.MinHeight(20f))) {
            AJS_NEWQUESTION();
            pressed = isOpenQuestion;
        }

        if (gui.EzButton(gui.MoveDownButton, GUILayout.MaxWidth(40), GUILayout.Height(18f))) {
            if (XML_Manager.Q.Count - StartShowingOn > 20) {
                StartShowingOn++;
                AJS_UpdateAndSelect(HeaderButtons.Count);
            }
        }

        return pressed;
    }

    private void OnGUI() {
        bgRect = new Rect(0, 0, Screen.width, Screen.height);
        GUI.DrawTexture(bgRect, bgTexture);

        GUIStyle NormalStyle = CreateStyle(TextAnchor.MiddleLeft, FontStyle.BoldAndItalic, 14);

        gui.HBlockBegin();

        #region LEFTBAR
        gui.VBlockBegin();

        int pressed = QuestionsBlock();
        if (isOpenQuestion != pressed) {
            GUI.FocusControl(null);
            isOpenQuestion = pressed;
            AJS_SELECTQUESTION(isOpenQuestion + StartShowingOn);
        }
        gui.VBlockEnd();
        #endregion

        gui.VBlockBegin();

        //TITLES
        gui.Separator();
        EditorGUILayout.LabelField("XML Quiz Editor", TitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50), GUILayout.Width(700));
        EditorGUILayout.LabelField("QUESTION " + (isOpenQuestion + StartShowingOn + 1), TitleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(50), GUILayout.Width(700));

        //QUESTION - TO ASK
        gui.Separator(13);
        EditorGUILayout.LabelField("  Question (To Ask):", NormalStyle);
        gui.Separator();
        gui.HBlockBegin();
        EditorGUILayout.LabelField(" ", GUILayout.Width(5));
        BGColor(btn);
        theQuestion = EditorGUILayout.TextArea(theQuestion, TextArea, GUILayout.MinHeight(36), GUILayout.ExpandWidth(true));
        BGColor();
        gui.HBlockEnd();

        //ABOUT THE QUESTION
        gui.Separator(10);
        gui.HBlockBegin(); NormalStyle.fontSize = 12;
        EditorGUILayout.LabelField("   About: ", NormalStyle, GUILayout.MaxWidth(58));
        BGColor(btn);
        aboutMe = EditorGUILayout.TextArea(aboutMe, TextArea, GUILayout.MinHeight(25));
        BGColor();
        gui.HBlockEnd();

        #region ANSWERs
        gui.Separator(13);
        gui.VBlockBegin();
        a_awnser = AnswerField("ANSWER A ~>", a_awnser, btn);
        b_awnser = AnswerField("ANSWER B ~>", b_awnser, Color.cyan);
        c_awnser = AnswerField("ANSWER C ~>", c_awnser, btn);
        d_awnser = AnswerField("ANSWER D ~>", d_awnser, Color.cyan);
        e_awnser = AnswerField("ANSWER E ~>", e_awnser, btn);
        gui.VBlockEnd();
        #endregion

        //CORRECT ANSWER
        gui.Separator();
        gui.HBlockBegin();
        NormalStyle.alignment = TextAnchor.MiddleCenter;
        EditorGUILayout.LabelField("CORRECT" + System.Environment.NewLine + "    ANSWER: ", NormalStyle, GUILayout.MaxWidth(106), GUILayout.Height(30));

        BGColor(btn);
        correct_awnser = GUILayout.SelectionGrid(correct_awnser, answers, 5, GUILayout.ExpandWidth(true), GUILayout.MinHeight(30));
        BGColor();
        gui.HBlockEnd();

        //BOTTOM BUTTONS
        gui.Separator(20);
        AJS_GUIWithSpace(() => { AJS_Button(erase, btn, 60, AJS_ERASEQUESTION); AJS_Button(apply, btn, 60, AJS_APPLYQUESTION); });
        AJS_GUIWithSpace(() => AJS_Button(save, Color.yellow, 45, AJS_GENERATE));

        gui.VBlockEnd();
        gui.HBlockEnd();

    }

    #region To Have Beautiful and Clean Code
    private void BGColor() {
        GUI.backgroundColor = Color.white;
    }
    private void BGColor(Color bg) {
        GUI.backgroundColor = bg;
    }
    private void AJS_GUIWithSpace(params System.Action[] func) {
        gui.HBlockBegin();
        EditorGUILayout.LabelField(" ", GUILayout.Width(5));
        for (int i = 0; i < func.Length; i++) func[i]();
        gui.HBlockEnd();
    }
    private void AJS_Button(Texture2D texture, Color color, int height, System.Action func) {
        BGColor(color);
        if (gui.EzButton(texture, GUILayout.Height(height))) func();
        BGColor();
    }
    private void AJS_UpdateAndSelect(int index) {
        AJS_UPDATEQBUTTONS();
        AJS_SELECTQUESTION(index);
    }
    private string AnswerField(string title, string theAnswer, Color blockColor) {
        GUIStyle style = CreateStyle(TextAnchor.MiddleRight, FontStyle.BoldAndItalic, 11);

        gui.HBlockBegin();
        BGColor(blockColor);
        EditorGUILayout.LabelField(title, style, GUILayout.MaxWidth(106));
        theAnswer = EditorGUILayout.TextField(theAnswer, TextArea);
        BGColor();
        gui.HBlockEnd();
        return theAnswer;
    }
    #endregion

    #region Manipulating XML
    private void AJS_NEWQUESTION() {
        XML_Questions newQuestion = new XML_Questions();
        for (int i = 0; i < 5; i++) {
            newQuestion.answer.Add(new XML_Answer());
        }

        XML_Manager.Q.Add(newQuestion);

        GUI.FocusControl(null);
        if (XML_Manager.Q.Count - StartShowingOn > 20) StartShowingOn++;

        AJS_UPDATEQBUTTONS();
        isOpenQuestion = HeaderButtons.Count - 1;
        AJS_SELECTQUESTION(isOpenQuestion + StartShowingOn);

        Debug.Log("[AJS] New Question Created.");
    }
    void AJS_UPDATEQBUTTONS() {
        HeaderButtons.Clear();

        int lastButton = (StartShowingOn + 20 > XML_Manager.Q.Count) ? XML_Manager.Q.Count : StartShowingOn + 20;

        for (int i = StartShowingOn; i < lastButton; i++)
            HeaderButtons.Add("Q" + ((i < 9) ? "0" : "") + (i + 1));

    }
    void AJS_APPLYQUESTION() {
        int index = isOpenQuestion + StartShowingOn;

        XML_Manager.QUEST[index].about = aboutMe;
        XML_Manager.QUEST[index].text = theQuestion;
        XML_Manager.QUEST[index].answer[0].text = a_awnser;
        XML_Manager.QUEST[index].answer[1].text = b_awnser;
        XML_Manager.QUEST[index].answer[2].text = c_awnser;
        XML_Manager.QUEST[index].answer[3].text = d_awnser;
        XML_Manager.QUEST[index].answer[4].text = e_awnser;
        XML_Manager.QUEST[index].correctAnswer = correct_awnser;

        Debug.Log("[AJS] Question Applied.");
    }
    void AJS_SELECTQUESTION(int button) {

        if (XML_Manager.Q.Count <= 0) {
            Debug.LogError("No Questions. But you can create one! This editor is for this :)");
            return;
        } else if (XML_Manager.Q.Count < button) {
            Debug.LogError("You only have " + XML_Manager.Q.Count + " questions.");
            return;
        }

        GUI.FocusControl(null);
        aboutMe = XML_Manager.QUEST[button].about;
        theQuestion = XML_Manager.QUEST[button].text;
        a_awnser = XML_Manager.QUEST[button].answer[0].text;
        b_awnser = XML_Manager.QUEST[button].answer[1].text;
        c_awnser = XML_Manager.QUEST[button].answer[2].text;
        d_awnser = XML_Manager.QUEST[button].answer[3].text;
        e_awnser = XML_Manager.QUEST[button].answer[4].text;
        correct_awnser = XML_Manager.QUEST[button].correctAnswer;
    }
    void AJS_ERASEQUESTION() {
        XML_Manager.Q.RemoveAt(isOpenQuestion);

        if (isOpenQuestion == XML_Manager.Q.Count)
            isOpenQuestion--;

        AJS_UpdateAndSelect(isOpenQuestion);

        Debug.Log("[AJS] Question Erased.");
    }
    private void AJS_GENERATE() {
        XML_Manager.Save();
    }
    #endregion

}
#endif