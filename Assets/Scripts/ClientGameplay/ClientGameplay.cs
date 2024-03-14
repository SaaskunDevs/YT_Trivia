using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientGameplay : MonoBehaviour
{
    [Header ("Questions")]
    [SerializeField] List<Questions> _questions = new List<Questions>();
    [SerializeField] TextMeshProUGUI _questionText;
    [SerializeField] Transform buttonParent;
    [SerializeField] GameObject buttonPrefab;
    int _currentIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RandomIndex();
            PushQuestionsAnswers(_currentIndex);
            TCPEmiter.SendQuestionIndex("Questions",_currentIndex);
        }
    }
    public void GetQuestions(int index)
    {
        
    }

    public void ClientEntered()
    {
        TCPEmiter.SendHostEntered("ClientEntered", true);
        Debug.Log("ClientEntered desde ClientGameplay");
    }
    public void ReadyToPlay()
    {
    }

    public void RandomIndex()
    {
        if (_questions.Count == 0)
        {
            Debug.Log("No hay más preguntas");
            // Pantalla de fin de juego
            return;
        }
        int randomIndex = Random.Range(0, _questions.Count);
        // PushQuestionsAnswers(randomIndex);
        _currentIndex = randomIndex;
    }

    public void PushQuestionsAnswers(int index)
    {
        _questionText.text = _questions[index].GetQuestion();
        int answerCount = _questions[index].GetAnswerLength();

        //Destruimos los botones anteriores si existen
        if (buttonParent.childCount > 0)
        {
            foreach (Transform child in buttonParent)
            {
                Destroy(child.gameObject);
            }
        }
        //Creamos los botones con las respuestas
        for (int i = 0; i < answerCount; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonParent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = _questions[index].GetAnswer(i);
            button.GetComponent<BtnAnswer>().SetIndex(i);

            // button.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(button.GetComponent<BtnAnswer>().GetIndex()));
        }
    }
}
