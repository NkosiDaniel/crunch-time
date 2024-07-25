using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    public string currentText => tmpro.text;
    public TMP_Text tmpro => tmproUI != null ? tmproUI : tmpro_world;
    public string targetText {get; private set;} = ""; //what we're trying to build/append
    public string preText {get; private set;} = ""; //targetText goes on top of preText when appending
    public string fullTargetText => preText + targetText;
    private int preTextLength = 0;
    private TextMeshProUGUI tmproUI; //UI text
    private TextMeshPro tmpro_world; //world text

    //Different modes for generating text
    public enum BuildMethod 
    {
        instant,
        typewriter,
        fade
    }

    public BuildMethod buildMethod = BuildMethod.typewriter; 

    public Color textColor { get {return tmpro.color;} set {tmpro.color = value;} } 

    public float speed {get {return baseSpeed * speedMultiplier;} set {speedMultiplier = value; }}
    private const float baseSpeed = 1; //Universal speed 
    private float speedMultiplier; //Inidvidual speed
    public int characterPerCycle { get {return speed <= 2f ? characterMultipler : speed <= 2.5f ? characterMultipler * 2 : characterMultipler * 3;}}
    private int characterMultipler = 1;
    public bool haste = false;

    //Constructers for wether UI text or world text is being passed
    public TextArchitect(TextMeshProUGUI tmpro_ui) 
    {
        this.tmproUI = tmpro_ui;
    }

     public TextArchitect(TextMeshPro tmpro_world) 
    {
        this.tmpro_world = tmpro_world;
    }

    public Coroutine Build(string text) 
    {
        preText = "";
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    /// <summary>
    /// Appends text to what is already in the text architect.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Coroutine Append(string text) 
    {
        preText = tmpro.text;
        targetText = text;

        Stop();

        buildProcess = tmpro.StartCoroutine(Building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

/// <summary>
/// Will stop the BuildProcess if text is already being built.
/// </summary>
    public void Stop() 
    {
        if(!isBuilding) 
        return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }
    /// <summary>
    /// Starts building text depending on the build method.
    /// </summary>
    /// <returns></returns>
    IEnumerator Building() 
    {
        Prepare();
        
        switch(buildMethod) 
        {
            case BuildMethod.typewriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
        }
        OnComplete();
    }
    /// <summary>
    /// Will empty the buildProcess after it is done building 
    /// </summary>
    private void OnComplete() 
    {
        buildProcess = null;
    }

    private void Prepare() 
    {
        switch(buildMethod) 
        {
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typewriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
    }

    private void Prepare_Instant() 
    {
        tmpro.color = tmpro.color; //Reapplies the color to all of the text verticies
        tmpro.text = fullTargetText; 
        tmpro.ForceMeshUpdate(); //Any changes we make is going to be applied at this point
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount; //Make sure every character is visible on screen
    }
     private void Prepare_Typewriter() {}
      private void Prepare_Fade() {}

    private IEnumerator Build_Typewriter() 
    {
        yield return null;
    }

     private IEnumerator Build_Fade() 
    {
        yield return null;
    }
}