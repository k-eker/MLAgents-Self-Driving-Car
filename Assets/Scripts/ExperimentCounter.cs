using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperimentCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_AttemptsText;

    private int m_Attempts;

    private void Start()
    {
        m_Attempts = PlayerPrefs.GetInt("Attempts"); ;
        UpdateAttemptsText();
    }

    public void IncrementAttempts()
    {
        m_Attempts++;
        UpdateAttemptsText();
    }

    private void UpdateAttemptsText()
    {
        m_AttemptsText.text = "Attempts: " + m_Attempts.ToString();
    }

    [ContextMenu("Reset Attempts")]
    private void ResetAttempts()
    {
        PlayerPrefs.DeleteAll();
        m_Attempts = 0;
        UpdateAttemptsText();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Attempts", m_Attempts);
    }
}
