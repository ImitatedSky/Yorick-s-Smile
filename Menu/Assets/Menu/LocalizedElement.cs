using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalizedElement : MonoBehaviour
{
    public enum ElementType
    {
        Text,
        Sprite
    }

    public ElementType elementType;
    public string key;

    void Start()
    {
        if (key == null || key == "")
        {
            key = gameObject.name.ToLower();
        }
        {
            key = gameObject.name.ToLower();
        }
        UpdateLocalizedText(); // 初始調用更新方法
    }

    /// <summary>
    /// 更新本地化文本或圖片的方法
    /// </summary>
    public void UpdateLocalizedText()
    {
        switch (elementType)
        {
            case ElementType.Text:
                TextMeshProUGUI textComponent = GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = LanguageManager.Instance.GetText(key);
                    textComponent.font = LanguageManager.Instance.GetFont();
                }
                else
                {
                    Debug.LogError("No TextMeshProUGUI component found on element!");
                }
                break;

            case ElementType.Sprite:
                Image imageComponent = GetComponentInChildren<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = LanguageManager.Instance.GetSprite(key);
                }
                else
                {
                    Debug.LogError("No Image component found on element!");
                }
                break;

            default:
                Debug.LogWarning("Unsupported element type!");
                break;
        }
    }
}
