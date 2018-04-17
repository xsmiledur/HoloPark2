using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogMenuBehavior : MonoBehaviour {

    [SerializeField]
    private Text m_textUI = null;
    string preTxt = "";
    private void Awake()
    {
        m_textUI = GetComponent<Text>();
        m_textUI.text = "";
        Application.logMessageReceived += OnLogMessage;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived += OnLogMessage;
    }

    private void OnLogMessage(string i_logText, string i_stackTrace, LogType i_type)
    {
        if (string.IsNullOrEmpty(i_logText))
        {
            return;
        }
        //if (preTxt != i_logText)
        //{
            m_textUI.text += i_logText + "\n";
        //}
        preTxt = i_logText;
    }

}
