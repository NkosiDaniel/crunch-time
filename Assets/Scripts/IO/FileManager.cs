using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class FileManager 
{
    //A relative path will always be contained within the defined "root" directory (gamedata)
    //An absolute path can point to anywhere inside or outside our game's folders

    /// <summary>
    /// Reads ".txt" files from within Unity
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="includeBlankLines"></param>
    /// <returns></returns>
    
    public static List<string> ReadTextFile(string filePath, bool includeBlankLines = true) 
    {
        if(filePath.StartsWith("/")) //Using an absolute path
        {
            filePath = Filepath.root + filePath;
        }
        //Try and catch block used to run code once then catch any errors
        List<string> lines = new List<string>();
        try 
        {
            using (StreamReader str = new StreamReader(filePath)) //"using" will automatically dispose of the StreamReader once we're done using it
            {
                while(!str.EndOfStream) 
                {
                    string line = str.ReadLine();
                    if(includeBlankLines || !string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"File not found: '{ex.FileName}'");
        }
        return lines;
    }
    /// <summary>
    /// Reads text assets from Unity resources
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="includeBlankLines"></param>
    /// <returns></returns>
   public static List<string> ReadTextAsset(string filePath, bool includeBlankLines = true)  
   {
        TextAsset asset = Resources.Load<TextAsset>(filePath);
        if(asset == null) 
        {
            Debug.LogError($"Asset not found: '{filePath}'");
            return null;
        }
       return ReadTextAsset(asset, includeBlankLines);
   }
   public static List<string>  ReadTextAsset(TextAsset asset, bool includeBlankLines = true) 
   {
        List<string> lines = new List<string>();
        using(StringReader str = new StringReader(asset.text)) //StringReader is used to read data that is already in memory as a string
        {
            while(str.Peek() > -1) //Checks the next character in the string 
            {
                string line = str.ReadLine();
                if(includeBlankLines || !string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            } 
        }
        return lines;
   }
}
