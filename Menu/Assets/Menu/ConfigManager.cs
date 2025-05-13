using UnityEngine;
using System.IO;

public class ConfigManager : MonoBehaviour
{
    private string configPath;

    void Start()
    {
        configPath = Path.Combine(Application.persistentDataPath, "config.json");
        string defaultConfigPath = Path.Combine(Application.streamingAssetsPath, "config.json");

        if (!File.Exists(configPath))
        {
            // 如果玩家配置文件不存在，從默認位置拷貝一份
            if (File.Exists(defaultConfigPath))
            {
                // 只有當目標文件不存在時才拷貝
                if (!File.Exists(configPath))
                {
                    File.Copy(defaultConfigPath, configPath);
                }
            }
            else
            {
                Debug.LogError("默認配置文件不存在: " + defaultConfigPath);
                return;
            }
        }
        else
        {
            // 如果玩家配置文件存在，合併新配置
            string defaultJsonContent = File.ReadAllText(defaultConfigPath);
            string userJsonContent = File.ReadAllText(configPath);

            Config defaultConfig = JsonUtility.FromJson<Config>(defaultJsonContent);
            Config userConfig = JsonUtility.FromJson<Config>(userJsonContent);

            Config mergedConfig = MergeConfigs(defaultConfig, userConfig);
            string mergedJsonContent = JsonUtility.ToJson(mergedConfig);

            File.WriteAllText(configPath, mergedJsonContent);
        }

        // 讀取和應用合併後的配置
        string finalJsonContent = File.ReadAllText(configPath);
        ApplyConfig(finalJsonContent);
    }

    void ApplyConfig(string jsonContent)
    {
        Config config = JsonUtility.FromJson<Config>(jsonContent);
        // 根據配置應用參數
    }

    Config MergeConfigs(Config defaultConfig, Config userConfig)
    {
        // 合併邏輯：如果玩家配置中有值，則使用玩家配置中的值；否則使用默認配置中的值
        Config mergedConfig = new Config();
        mergedConfig.someParameter = userConfig.someParameter != 0 ? userConfig.someParameter : defaultConfig.someParameter;
        mergedConfig.anotherParameter = !string.IsNullOrEmpty(userConfig.anotherParameter) ? userConfig.anotherParameter : defaultConfig.anotherParameter;
        return mergedConfig;
    }

    public void SaveConfig(Config config)
    {
        string jsonContent = JsonUtility.ToJson(config);
        File.WriteAllText(configPath, jsonContent);
    }
}

[System.Serializable]
public class Config
{
    public int someParameter;
    public string anotherParameter;
}
