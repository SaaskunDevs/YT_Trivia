using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostGameplay : MonoBehaviour
{

    public bool hostEntered = false;
    public bool clientEntered = false;
    public bool hostReadyToPlay = false;
    public bool clientReadyToPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
