using UnityEngine;

public class ChangeLangeage : MonoBehaviour
{
    public void ChangeLanguage(string language)
    {
        LanguageManager.Instance.SetLanguage(language);
        LanguageManager.Instance.TriggerAllLocalizedElements();
    }
}
