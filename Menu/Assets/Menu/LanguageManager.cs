/*
* 流程:
* Awake -> LoadConfig -> LoadLanguages -> LoadUserLanguagePreference
* SetLanguage -> SaveUserLanguagePreference
* GetText -> 根據當前語言和鍵返回文本
* GetSprite -> 根據當前語言和鍵返回圖片
* LoadSystemLanguage -> 根據系統語言設置當前語言並保存
* LoadUserLanguagePreference -> 如果有保存的用戶語言，則加載，否則加載系統語言
* SaveUserLanguagePreference -> 保存用戶選擇的語言
*/
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }
    private Dictionary<string, JSONNode> languageDictionary = new Dictionary<string, JSONNode>();
    private Dictionary<string, string> languageConfig = new Dictionary<string, string>();

    private string defaultLanguage = "en"; // 默認語言
    public string currentLanguage = "en"; 
    private const string LanguagePreferenceKey = "LanguagePreference";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadConfig(); // 
            LoadLanguages(); // 加載語言資料
            LoadUserLanguagePreference(); // 載入用戶語言偏好
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 從 Resources 讀取 config.json，設置languageConfig[entry.Key]
    /// </summary>
    private void LoadConfig()
    {
        TextAsset configFile = Resources.Load<TextAsset>("config");
        if (configFile != null)
        {
            JSONNode configData = JSON.Parse(configFile.text);
            foreach (KeyValuePair<string, JSONNode> entry in configData["languages"].AsObject)
            {
                languageConfig[entry.Key] = entry.Value;
            }
        }
        else
        {
            Debug.LogError("config.json not found in Resources!");
        }
    }

    /// <summary>
    /// 從 Resources/MultiLangSupport 資料夾中讀取所有 JSON 文件
    /// </summary>
    public void LoadLanguages()
    {
        TextAsset[] languageFiles = Resources.LoadAll<TextAsset>("MultiLangSupport");
        foreach (TextAsset file in languageFiles)
        {
            JSONNode languageData = JSON.Parse(file.text);
            string languageKey = Path.GetFileNameWithoutExtension(file.name);
            languageDictionary[languageKey] = languageData;
        }
    }

    /// <summary>
    /// 載入用戶語言偏好的方法，如果沒有抓取系統語言
    /// </summary>
    private void LoadUserLanguagePreference()
    {
        if (PlayerPrefs.HasKey(LanguagePreferenceKey))
        {
            currentLanguage = PlayerPrefs.GetString(LanguagePreferenceKey);
        }
        else
        {
            LoadSystemLanguage();
        }
    }    
    
    /// <summary>
    /// 加載系統語言，如果config中沒有對應的語言配置，使用默認語言defaultLanguage
    /// </summary>
    public void LoadSystemLanguage()
    {
        string systemLanguage = Application.systemLanguage.ToString();
        bool languageFound = false;

        // 檢查 config.json 中是否有對應的語言配置
        foreach (var entry in languageConfig)
        {
            if (entry.Value == systemLanguage)
            {
                currentLanguage = entry.Key;
                languageFound = true;
                break;
            }
        }

        // 如果沒有對應語言，使用默認語言
        if (!languageFound)
        {
            currentLanguage = defaultLanguage; // 默認語言
        }

        SaveUserLanguagePreference(currentLanguage); // 保存系統語言到用戶設置
    }

    /// <summary>
    /// 設定當前語言的方法
    /// </summary>
    /// <param name="lang"></param>
    public void SetLanguage(string lang)
    {
        if (languageDictionary.ContainsKey(lang))
        {
            currentLanguage = lang;
            SaveUserLanguagePreference(lang); // 保存用戶語言偏好
        }
        else
        {
            Debug.LogWarning("Language not found: " + lang);
        }
    }

    /// <summary>
    /// 保存用戶語言偏好
    /// </summary>
    /// <param name="lang"></param>
    public void SaveUserLanguagePreference(string lang)
    {
        PlayerPrefs.SetString(LanguagePreferenceKey, lang);
        PlayerPrefs.Save();

    }

    /// <summary>
    /// 更新所有有LocalizedElement
    /// </summary>
    public void TriggerAllLocalizedElements()
    {
        LocalizedElement[] elements = FindObjectsByType<LocalizedElement>(FindObjectsSortMode.None);
        foreach (LocalizedElement element in elements)
        {
            element.UpdateLocalizedText();
        }
    }

    

    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }


    /// <summary>
    /// 獲取指定鍵的文本
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetText(string key)
    {
        if (languageDictionary.ContainsKey(currentLanguage) && languageDictionary[currentLanguage].HasKey(key))
        {
            return languageDictionary[currentLanguage][key];
        }
        return "Key not found: " + key;
    }

    /// <summary>
    /// 獲取指定鍵的圖片
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Sprite GetSprite(string key)
    {
        string spritePath = GetText(key);
        return Resources.Load<Sprite>(spritePath);
    }

    /// <summary>
    /// 獲取當前語言的字體,放在MultiLangSupport/lang.json -> font_asset
    /// </summary>
    /// <returns></returns>
    public TMP_FontAsset GetFont()
    {
        if(languageDictionary == null)
        {
            LoadConfig();
        }
        // TODO: 要做一個default 如果那個語言沒有 font
        string font_asset = languageDictionary[currentLanguage]["font_asset"];

        TMP_FontAsset font = Resources.Load<TMP_FontAsset>(font_asset);


        return font;
    }

}
