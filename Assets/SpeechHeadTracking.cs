using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using TMPro;

public class SpeechHeadTracking : MonoBehaviour
{
    public Rig rig;
    // Start is called before the first frame update
    private float time = 0;
    private float tp = 5;
    private bool isOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(isOn){
                rig.weight=1;
            }
            else{
                rig.weight=0;
            }
        time += Time.deltaTime;
    }

    public void ToggleWeight(){
        if(!isOn){
            isOn = true;
        }
        else if(isOn){
            isOn = false;
        }
    }

     IEnumerator SmoothRig(float s, float e){
            float elapsedTime = 0; 

            float waitTime = 20;

             while (elapsedTime < waitTime){
                 rig.weight = Mathf.Lerp(s,e, (elapsedTime/waitTime));
                 elapsedTime += Time.deltaTime;

                yield return null; 
             }
        }
}
