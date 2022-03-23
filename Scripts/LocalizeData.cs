using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Omnilatent.LocalizationTool
{
    public class SupportedLanguage
    {
        public const string english = "English";
        public const string vietnamese = "Vietnamese";
        public const string arabic = "Arabic";
        public const string chinese_simplified = "Chinese_Simplified";
        public const string chinese_traditional = "Chinese_Traditional";
        public const string french = "French";
        public const string german = "German";
        public const string hebrew = "Hebrew";
        public const string indonesian = "Indonesian";
        public const string italian = "Italian";
        public const string japanese = "Japanese";
        public const string korean = "Korean";
        public const string malay = "Malay";
        public const string portuguese = "Portuguese";
        public const string russian = "Russian";
        public const string spanish = "Spanish";
        public const string thai = "Thai";
        public const string turkish = "Turkish";
    }

    public class LocalizeData
    {
        public string key { get; set; }
        public string english { get; set; }
        public string vietnamese { get; set; }
        public string arabic { get; set; }
        public string chinese_simplified { get; set; }
        public string chinese_traditional { get; set; }
        public string french { get; set; }
        public string german { get; set; }
        public string hebrew { get; set; }
        public string indonesian { get; set; }
        public string italian { get; set; }
        public string japanese { get; set; }
        public string korean { get; set; }
        public string malay { get; set; }
        public string portuguese { get; set; }
        public string russian { get; set; }
        public string spanish { get; set; }
        public string thai { get; set; }
        public string turkish { get; set; }

        public string GetString(string language)
        {
            string result = null;
            switch (language)
            {
                case SupportedLanguage.english:
                    result = english;
                    break;
                case SupportedLanguage.vietnamese:
                    result = vietnamese;
                    break;
                case SupportedLanguage.arabic:
                    result = arabic;
                    break;
                case SupportedLanguage.chinese_simplified:
                    result = chinese_simplified;
                    break;
                case SupportedLanguage.chinese_traditional:
                    result = chinese_traditional;
                    break;
                case SupportedLanguage.french:
                    result = french;
                    break;
                case SupportedLanguage.german:
                    result = german;
                    break;
                case SupportedLanguage.hebrew:
                    result = hebrew;
                    break;
                case SupportedLanguage.indonesian:
                    result = indonesian;
                    break;
                case SupportedLanguage.italian:
                    result = italian;
                    break;
                case SupportedLanguage.japanese:
                    result = japanese;
                    break;
                case SupportedLanguage.korean:
                    result = korean;
                    break;
                case SupportedLanguage.malay:
                    result = malay;
                    break;
                case SupportedLanguage.portuguese:
                    result = portuguese;
                    break;
                case SupportedLanguage.russian:
                    result = russian;
                    break;
                case SupportedLanguage.spanish:
                    result = spanish;
                    break;
                case SupportedLanguage.thai:
                    result = thai;
                    break;
                case SupportedLanguage.turkish:
                    result = turkish;
                    break;
            }
            result = result?.Replace("\\n", "\n");
            return result;
        }
    }

    [System.Obsolete("Use SQLDataManager instead")]
    public class LocalizesJsonDataManager
    {
        static LocalizeData[] datas;

        static LocalizesJsonDataManager()
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
    }
}