using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogHandler : MonoBehaviour
{
    public DialogContainer dialogContainer = new DialogContainer();


    public static DialogHandler instance;

    public void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);
    }

}
