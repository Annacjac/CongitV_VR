using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechTimer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header ("Timer Settings")]
    public bool isOn = false;
    public float currentTime;
    public bool speechDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn){
            currentTime += Time.deltaTime;
            timerText.text = "Speech Duration: " + currentTime.ToString();
            speechDone = true;
        }
        else{
            currentTime = 0;
            timerText.text = "";
        }   
    }

    public void toggleTimer(){
        if(!speechDone && !isOn){
            isOn = true;
            speechDone = true;
        }
        else if(isOn){
            isOn = false;
        }
    }
}
