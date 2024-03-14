using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostManager : MonoBehaviour
{
    [SerializeField] GameObject _homeUI;
    [SerializeField] GameObject _instructionsUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstructionUI()
    {
        _homeUI.SetActive(false);
        _instructionsUI.SetActive(true);
        Debug.Log("UI de instrucciones Host");
    }
}
