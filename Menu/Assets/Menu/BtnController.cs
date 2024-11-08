using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using SimpleJSON;

public class BtnController : MonoBehaviour
{
    public string systemLanguage = "English"; // 系統語言
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite clickSprite;

    private JSONNode languageData; // 儲存解析後的 JSON 資料
    private TextMeshProUGUI buttonText;

    void Start()
    {
        // 載入語言資料
        LoadLanguageData();

        // 載入系統語言
        LoadSystemLanguage();

        // 設定按鈕文字
        SetButtonText();
    }

    // 載入 JSON 中的語言資料
    void LoadLanguageData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("language");


        if (jsonData != null)
        {
            // 使用 SimpleJSON 解析 JSON
            languageData = JSON.Parse(jsonData.text); // 修正：使用成員變數 languageData
            // Debug.Log("languageData: " + languageData.ToString());
        }
        else
        {
            Debug.LogError("language.json not found in Resources!");
        }
    }

    void LoadSystemLanguage()
    {
        // 抓取系統語言
        systemLanguage = Application.systemLanguage.ToString();

        switch (systemLanguage)
        {
            case "ChineseSimplified":
                systemLanguage = "ChineseSimplified";
                break;
            case "ChineseTraditional":
                systemLanguage = "zh_TW";
                break;
            case "English":
                systemLanguage = "en";
                break;

            default:
                systemLanguage = "en";
                break;
        }
    }

    // 設定按鈕的文字
    void SetButtonText()
    {
        Debug.Log("languageData: " + languageData.ToString());
        Debug.Log("systemLanguage: " + systemLanguage);

        // 找到按鈕的 TextMeshProUGUI 元件
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (buttonText != null && languageData != null)
        {
            string buttonKey = gameObject.name.ToLower(); // 取按鈕名稱作為鍵

            // 設定按鈕的文字
            if (languageData.HasKey(systemLanguage) && languageData[systemLanguage].HasKey(buttonKey))
            {
                buttonText.text = languageData[systemLanguage][buttonKey];
            }
            else
            {
                Debug.LogWarning($"Text for '{buttonKey}' not found in '{systemLanguage}' language.");
            }
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI component found on button!");
        }
    }
}
