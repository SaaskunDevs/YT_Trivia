using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientGameplay : MonoBehaviour
{
    [Header("WinLoseComponents")]

    [Header("UI")]
    [SerializeField] GameObject _homeUI;
    [SerializeField] GameObject _gameUI;
    [SerializeField] GameObject _instructionsUI;
    [SerializeField] GameObject _winnerUI;

    // Start is called before the first frame update
    void Start()
    {

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
    public void InstructionUI()
    {
        _homeUI.SetActive(false);
        _instructionsUI.SetActive(true);
        Debug.Log("UI de instrucciones Cliente");
    }
    public void GameUI()
    {
        _instructionsUI.SetActive(false);
        _gameUI.SetActive(true);
    }

    public void RondaUI(string winLose)
    {
        _gameUI.SetActive(false);



        switch (winLose)
        {
            case "Cliente":
                //Pantalla de ganador
                _winnerUI.SetActive(true);
                break;
            case "Host":
                //Pantalla de perdedor
                _winnerUI.SetActive(true);
                break;
        }
    }
}
