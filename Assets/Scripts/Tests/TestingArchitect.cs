using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace TESTING {
public class TestingArchitect : MonoBehaviour
{
    DialogHandler ds;
    TextArchitect architect;
    int counter;
    string[] text = new string[5] 
    {
        "Ok so we have like 35 days left to complete this game jam",
        "We all gotta lock in fr fr",
        "Tomorrow i'm going to plan a work session meeting",
        "So try to pull up if you can",
        "Lets give it our all and communicate with each other everyday!!"
    };
    void Start()
    {
        ds = DialogHandler.instance;
        architect = new TextArchitect(ds.dialogContainer.dialogText);
        architect.buildMethod = TextArchitect.BuildMethod.typewriter;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if(architect.isBuilding) 
            {
                if(!architect.haste)
                    architect.haste = true;
                else
                    architect.ForceComplete();

            }
            else 
            {
                architect.Build(text[counter]);
                counter++;
            }
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            architect.Append(text[counter]);
        }
    }   
  }
}