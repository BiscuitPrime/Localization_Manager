using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// All localization options
/// </summary>
public enum LOCALIZATION_OPTIONS
{
    FR,
    ENG,
}

/// <summary>
/// Class containing all the tags defined and used by the game.
/// </summary>
[Serializable]
public class JSONLocalizationTextClass
{
    public string T_TEST_T1;
    public string T_TEST_T2;
}

/// <summary>
/// Struct used to associate JSON files' paths to a localization language.
/// </summary>
[Serializable]
public struct SerializePathDictionary
{
    [Tooltip("Path towards the element in the Resources folder (the name of the file will suffice)")]
    public string path;
    public LOCALIZATION_OPTIONS localizationLanguage;
}

public class LocalizationManager : MonoBehaviour
{
    #region SINGLETON DESIGN PATTERN
    public static LocalizationManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(this); //OPTIONAL : it might be recommended to put this script, and the associated gameobject with it, on an object present at game start and keep it around for the entire game.

        PostAwakeLocalizationSetup();
    }
    #endregion

    [Header("Localization Data")]
    [SerializeField, Tooltip("Dictionary associating paths to a given localization language.")]
    private SerializePathDictionary[] _pathDict;

    [Header("Localization Parameters")]
    [SerializeField, Tooltip("Currently selected language.")] private LOCALIZATION_OPTIONS _option;

    [Header("Localization Text Data")]
    [SerializeField, Tooltip("Default text displayed in case of localization failure.")]
    private string _defaultLocalNotFoundText = "TEXT_NOT_FOUND_IN_LOCALIZATION";

    private Dictionary<LOCALIZATION_OPTIONS, Dictionary<string,string>> localizationDict;
    
    /// <summary>
    /// Function that will setup the various dictionaries containing the localization at startup.
    /// </summary>
    private void PostAwakeLocalizationSetup()
    {
        //We create the main localization Dictionary :
        localizationDict = new Dictionary<LOCALIZATION_OPTIONS, Dictionary<string, string>>();
        //We recuperate the fields of the class, useful for the modularization of the class
        FieldInfo[] fields = Type.GetType("JSONLocalizationTextClass").GetFields();
        //For each language defined in the system, at startup, the dictionary
        foreach(var lang in _pathDict)
        {
            //We create a dictionary that will contain all the associations between tags and associated data read from resources
            JSONLocalizationTextClass jsonObj = JsonUtility.FromJson<JSONLocalizationTextClass>(((TextAsset)Resources.Load(lang.path)).text);
            Dictionary<string, string> jsonDict = new Dictionary<string, string>();
            for (int i = 0; i < fields.Length; i++)
            {
                jsonDict[fields[i].ToString().Remove(0, 14)] = fields[i].GetValue(jsonObj).ToString(); //we need to remove a "System.String " that appears
            }
            //Finally, we had this dict to the larger dict, associating it to a given language
            localizationDict[lang.localizationLanguage] = jsonDict;
        }
    }

    /// <summary>
    /// Function called everywhere that will replace inputted tag by the associated localized text as defined by Resources-JSON files.
    /// </summary>
    /// <param name="textTag">Tag of the desired text.</param>
    /// <returns>The localized string</returns>
    public string LocalText(string textTag)
    {
        string outText = _defaultLocalNotFoundText;
        if ((localizationDict[_option]).ContainsKey(textTag))
        {
            outText = (localizationDict[_option])[textTag];
        }
        return outText;
    }

    public void SetLocalizationOption(LOCALIZATION_OPTIONS option)
    {
        _option = option;
    }
    public LOCALIZATION_OPTIONS GetLocalizationOption()
    {
        return _option;
    }
}
