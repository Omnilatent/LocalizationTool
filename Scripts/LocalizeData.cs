using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Omnilatent.LocalizationTool
{
    public class SupportedLanguage
    {
        public const string English = "English";
        public const string Vietnamese = "Vietnamese";
        public const string Arabic = "Arabic";
        public const string ChineseSimplified = "Chinese_Simplified";
        public const string ChineseTraditional = "Chinese_Traditional";
        public const string French = "French";
        public const string German = "German";
        public const string Hebrew = "Hebrew";
        public const string Indonesian = "Indonesian";
        public const string Italian = "Italian";
        public const string Japanese = "Japanese";
        public const string Korean = "Korean";
        public const string Malay = "Malay";
        public const string Portuguese = "Portuguese";
        public const string Russian = "Russian";
        public const string Spanish = "Spanish";
        public const string Thai = "Thai";
        public const string Turkish = "Turkish";
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
                case SupportedLanguage.English:
                    result = english;
                    break;
                case SupportedLanguage.Vietnamese:
                    result = vietnamese;
                    break;
                case SupportedLanguage.Arabic:
                    result = arabic;
                    break;
                case SupportedLanguage.ChineseSimplified:
                    result = chinese_simplified;
                    break;
                case SupportedLanguage.ChineseTraditional:
                    result = chinese_traditional;
                    break;
                case SupportedLanguage.French:
                    result = french;
                    break;
                case SupportedLanguage.German:
                    result = german;
                    break;
                case SupportedLanguage.Hebrew:
                    result = hebrew;
                    break;
                case SupportedLanguage.Indonesian:
                    result = indonesian;
                    break;
                case SupportedLanguage.Italian:
                    result = italian;
                    break;
                case SupportedLanguage.Japanese:
                    result = japanese;
                    break;
                case SupportedLanguage.Korean:
                    result = korean;
                    break;
                case SupportedLanguage.Malay:
                    result = malay;
                    break;
                case SupportedLanguage.Portuguese:
                    result = portuguese;
                    break;
                case SupportedLanguage.Russian:
                    result = russian;
                    break;
                case SupportedLanguage.Spanish:
                    result = spanish;
                    break;
                case SupportedLanguage.Thai:
                    result = thai;
                    break;
                case SupportedLanguage.Turkish:
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
                        case SupportedLanguage.Vietnamese:
                            ret = datas[i].vietnamese.Replace("\\n", "\n");
                            break;
                        case SupportedLanguage.English:
                            ret = datas[i].english.Replace("\\n", "\n");
                            break;
                        case SupportedLanguage.Japanese:
                            ret = datas[i].japanese.Replace("\\n", "\n");
                            break;
                        case SupportedLanguage.Korean:
                            ret = datas[i].korean.Replace("\\n", "\n");
                            break;
                        case SupportedLanguage.Hebrew:
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