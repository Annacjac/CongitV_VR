using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.Idle Walk Run Blend", 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Walk(){
        
    }

    public void Idle(){
        anim.Play("Base Layer.Idle", 0, 0);
    }*/
}
