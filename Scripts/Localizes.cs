using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class SupportedLanguage
{
    public const string english = "English";
    public const string vietnamese = "Vietnamese";
    public const string japanese = "Japanese";
    public const string korean = "Korean";
    public const string hebrew = "Hebrew";
    public const string arabic = "Arabic";
    public const string french = "French";
    public const string german = "German";
    public const string portuguese = "Portuguese";
    public const string spanish = "Spanish";
    public const string turkish = "Turkish";
}

public class LocalizedData
{
    public string key { get; set; }
    public string vietnamese { get; set; }
    public string english { get; set; }
    public string japanese { get; set; }
    public string korean { get; set; }
    public string hebrew { get; set; }
    public string arabic { get; set; }
    public string french { get; set; }
    public string german { get; set; }
    public string portuguese { get; set; }
    public string spanish { get; set; }
    public string turkish { get; set; }

    public string GetString(string language)
    {
        string ret = null;
        switch (language)
        {
            case SupportedLanguage.vietnamese:
                ret = vietnamese?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.english:
                ret = english?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.japanese:
                ret = japanese?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.korean:
                ret = korean?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.hebrew:
                ret = hebrew?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.arabic:
                ret = arabic?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.french:
                ret = french?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.german:
                ret = german?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.portuguese:
                ret = portuguese?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.spanish:
                ret = spanish?.Replace("\\n", "\n");
                break;
            case SupportedLanguage.turkish:
                ret = turkish?.Replace("\\n", "\n");
                break;
        }
        return ret;
    }
}

public class Localizes
{
    public const string PREF_LANGUAGE = "PP_KEY_LANGUAGE";
    static LocalizedData[] datas;

    static Localizes()
    {
        //datas = JsonMapper.ToObject<LocalizedData[]>(Resources.Load<TextAsset>("Data/LocalizedData.json").text);
    }

    public static string GetString(string id, string language)
    {
        string ret = string.Empty;
        for (int i = 0; i < datas.Length; i++)
        {
            if (string.Compare(id, datas[i].key) == 0)
            {
                switch (language)
                {
                    case SupportedLanguage.vietnamese:
                        ret = datas[i].vietnamese.Replace("\\n", "\n");
                        break;
                    case SupportedLanguage.english:
                        ret = datas[i].english.Replace("\\n", "\n");
                        break;
                    case SupportedLanguage.japanese:
                        ret = datas[i].japanese.Replace("\\n", "\n");
                        break;
                    case SupportedLanguage.korean:
                        ret = datas[i].korean.Replace("\\n", "\n");
                        break;
                    case SupportedLanguage.hebrew:
                        ret = datas[i].hebrew.Replace("\\n", "\n");
                        break;
                }
            }
        }
        //Debug.LogWarning(string.Format("Localize missing {0} {1}", language, id));
        return string.IsNullOrEmpty(ret) ? id : ret;
    }

    public static string GetString(string id)
    {
        return id;
        //return GetString(id, CurrentLanguage());
    }

    public static void SetLanguage(string language)
    {
        PlayerPrefs.SetString(PREF_LANGUAGE, language); //"PP_KEY_LANGUAGE"
        PlayerPrefs.Save();
        //.Log(CurrentLanguage());
    }

    public static string CurrentLanguage()
    {
        string language = PlayerPrefs.GetString(PREF_LANGUAGE, string.Empty);

        if (string.IsNullOrEmpty(language))
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Japanese:
                    language = SupportedLanguage.japanese;
                    break;
                case SystemLanguage.Vietnamese:
                    language = SupportedLanguage.vietnamese;
                    break;
                case SystemLanguage.Korean:
                    language = SupportedLanguage.korean;
                    break;
                case SystemLanguage.Hebrew:
                    language = SupportedLanguage.hebrew;
                    break;
                case SystemLanguage.Arabic:
                    language = SupportedLanguage.arabic;
                    break;
                case SystemLanguage.French:
                    language = SupportedLanguage.french;
                    break;
                case SystemLanguage.German:
                    language = SupportedLanguage.german;
                    break;
                case SystemLanguage.Portuguese:
                    language = SupportedLanguage.portuguese;
                    break;
                case SystemLanguage.Spanish:
                    language = SupportedLanguage.spanish;
                    break;
                case SystemLanguage.Turkish:
                    language = SupportedLanguage.turkish;
                    break;
                default:
                    language = SupportedLanguage.english;
                    break;
            }
            //SetLanguage(language);
        }

        return language;
    }
}
