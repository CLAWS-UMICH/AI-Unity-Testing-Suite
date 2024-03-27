using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class DictationScript : MonoBehaviour
{

    public TMP_Text transcription_text;

    private DictationRecognizer dictationRecognizer;

    bool keepLooping = false;

    private void Start()
    {        
        PhraseRecognitionSystem.Shutdown();
        transcription_text.SetText("Waiting for transcription");
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.AutoSilenceTimeoutSeconds = 3f;
        dictationRecognizer.DictationResult += OnDictationResult;
        dictationRecognizer.DictationHypothesis += OnDictationHypothesis;
        dictationRecognizer.Start();
    }

    private void Update()
    {
        if (keepLooping && dictationRecognizer.Status == SpeechSystemStatus.Stopped)
        {
            dictationRecognizer.Start();
        }
    }

    public void StartDictation()
    {
        PhraseRecognitionSystem.Shutdown();
        dictationRecognizer.Stop();
        if (!keepLooping)
        {
            keepLooping = true;
        }
        else
        {
            keepLooping = false;
        }
    }

    void OnDictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.Log("Dictation result: " + text); 
        transcription_text.SetText(text);
    }

    void OnDictationHypothesis(string text)
    {
        Debug.Log("Hypothesis: " + text);
        transcription_text.SetText(text);
    }
}