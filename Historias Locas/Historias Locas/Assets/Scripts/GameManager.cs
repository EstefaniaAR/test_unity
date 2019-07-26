using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button playButton;
    InputField inputField;
    Text text;

    string[] questions;
    string[] answers;
    int questionIndex= 0;
    int storyNumber;
    const int numberStories = 1;
    void Init()
    {
        storyNumber = Random.Range(0,numberStories);
        questionIndex = 0;
        inputField.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
        playButton.transform.GetComponentInChildren<Text>().text="Volver a Jugar";
        text.text = "Historias Locas";
        playButton.onClick.AddListener(Ask);

        questions = Resources.Load<TextAsset>("Questions/Preguntas_{storyNumber}").text.Split('\n');
        answers = new string[questions.Length];
    }
    void Start()
    {
        inputField= GameObject.Find("InputField").GetComponent<InputField>();
        playButton = GameObject.Find("Button").GetComponent<Button>();
        inputField.gameObject.SetActive(false);
        text = GameObject.Find("Question").GetComponent<Text>();

        //Add event
        playButton.onClick.AddListener(Ask);

        questions = Resources.Load<TextAsset>("Questions/Preguntas_{storyNumber}").text.Split('\n');
        answers = new string[questions.Length];
        this.inputField.onEndEdit.AddListener(delegate { this.Answer(); });

        //Debug.Log("Working...");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ask()
    {
        inputField.text="";
        inputField.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        text.text = this.questions[questionIndex];
    }

    void Answer()
    {
        this.answers[this.questionIndex] = inputField.text;
        questionIndex++;
        if (this.questionIndex < this.questions.Length)
            Ask();
        else
            ShowHistory();
    }

    void ShowHistory()
    {
        var story = Resources.Load<TextAsset>("Stories/Historias_{storyNumber}").text;
        text.text = string.Format(story, answers);
        this.inputField.gameObject.SetActive(false);
        this.playButton.gameObject.SetActive(true);

        playButton.onClick.RemoveAllListeners();
        playButton.transform.GetComponentInChildren<Text>().text = "Volver a Jugar";
        playButton.onClick.AddListener(this.Init);

    }
   
}
