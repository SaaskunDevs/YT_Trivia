using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HostGameplay : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<Questions> _questions = new List<Questions>();
    [SerializeField] TextMeshProUGUI _questionText;
    [SerializeField] Transform buttonParent;
    [SerializeField] GameObject buttonPrefab;
    HostManager _hostManager;

    public bool hostEntered = false;
    public bool clientEntered = false;
    public bool hostReadyToPlay = false;
    public bool clientReadyToPlay = false;

    int _currentIndexHost = 0;

    // Start is called before the first frame update
    void Start()
    {
        _hostManager = GetComponent<HostManager>();
    }
    public void GetQuestions(int index)
    {
        _currentIndexHost = index;
        StartCoroutine(WaitForHost());
        // PushQuestionsAnswersHost(index);
    }

    IEnumerator WaitForHost()
    {
        yield return new WaitForSeconds(3);
        PushQuestionsAnswersHost(_currentIndexHost);
    }

    public void PushQuestionsAnswersHost(int index)
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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HostEntered();
        }
    }

    public void ClientEntered()
    {
        clientEntered = true;
        CheckInitialScreen();
        Debug.Log("ClientEntered");
    }

    public void ClientReadyToPlay()
    {
        clientReadyToPlay = true;
        CheckReadyScreen();
    }

    public void HostEntered()
    {
        hostEntered = true;
        CheckInitialScreen();
        Debug.Log("HostEntered");

        TCPEmiter.SendHostEntered("HostEntered", hostEntered);
    }

    public void HostReadyToPlay()
    {
        hostReadyToPlay = true;
        CheckReadyScreen();
    }

    void CheckInitialScreen()
    {
        if (hostEntered && clientEntered)
            {
                Debug.Log("Change to instructions UI");
                _hostManager.InstructionUI();
                TCPEmiter.SendHostEntered("ChangeToInstructions", true);
            }
    }

    void CheckReadyScreen()
    {
        if (hostReadyToPlay && clientReadyToPlay)
            {
                Debug.Log("Start UI");
            }
    }

}
