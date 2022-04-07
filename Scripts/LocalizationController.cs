using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Omnilatent.LocalizationTool
{
    public static class LocalizationController
    {
        public static bool enableAutoAddNotFoundEntry = false; //if true, when entry is not found (while playing in editor), it will be added to database
        public const string PREF_LANGUAGE = "PP_KEY_LANGUAGE";

        static ILocalizeDataManager sqlDataManager;
        public static ILocalizeDataManager SqlDataManager
        {
            get
            {
                if (sqlDataManager == null)
                {
                    if (SQLDataManager.Instance != null)
                    {
                        sqlDataManager = SQLDataManager.Instance;
                    }
                    else
                    {
                        //Warning: localize database initialization is an async function, database may not be ready and return null before initialization is complete
                        GameObject prefab = Resources.Load<GameObject>("LocalizeDataManager");
                        sqlDataManager = Object.Instantiate(prefab).GetComponent<ILocalizeDataManager>();
                    }
                }
                return sqlDataManager;
            }
        }

        public static string Localize(this string key)
        {
            return GetString(key);
        }

        public static string GetString(string key, string language)
        {
            string ret = string.Empty;
            LocalizeData data = SqlDataManager.GetLocalizeData(key); //GameDatabase.ServiceSQLConnection.Table<LocalizedData>().Where(x => x.key == key).FirstOrDefault();

            if (data != null)
            {
                ret = data.GetString(language);
                if (string.IsNullOrEmpty(ret))
                {
                    Debug.LogWarning($"No LocalizedData entry for [{key}] in [{language}]");
                    ret = data.GetString(SupportedLanguage.English);
                    if (string.IsNullOrEmpty(ret))
                    {
                        ret = key;
                    }
                }
            }
            else
            {
                string msg = $"No LocalizedData entry for [{key}] in [{language}].";
#if UNITY_EDITOR
                if (data == null && enableAutoAddNotFoundEntry)
                {
                    var newData = new LocalizeData
                    {
                        key = key
                    };
                    SqlDataManager.AddLocalizeData(newData);
                    msg += " Adding new entry to database.";
                }
#endif
                Debug.LogWarning(msg);
            }

            if (string.IsNullOrEmpty(ret))
            {
                return key;
            }
            return ret;
        }

        public static string GetString(string id)
        {
            return GetString(id, CurrentLanguage());
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
                        language = SupportedLanguage.Japanese;
                        break;
                    case SystemLanguage.Vietnamese:
                        language = SupportedLanguage.Vietnamese;
                        break;
                    case SystemLanguage.Korean:
                        language = SupportedLanguage.Korean;
                        break;
                    case SystemLanguage.Hebrew:
                        language = SupportedLanguage.Hebrew;
                        break;
                    case SystemLanguage.Arabic:
                        language = SupportedLanguage.Arabic;
                        break;
                    case SystemLanguage.French:
                        language = SupportedLanguage.French;
                        break;
                    case SystemLanguage.German:
                        language = SupportedLanguage.German;
                        break;
                    case SystemLanguage.Portuguese:
                        language = SupportedLanguage.Portuguese;
                        break;
                    case SystemLanguage.Spanish:
                        language = SupportedLanguage.Spanish;
                        break;
                    case SystemLanguage.Turkish:
                        language = SupportedLanguage.Turkish;
                        break;
                    case SystemLanguage.ChineseSimplified:
                        language = SupportedLanguage.ChineseSimplified;
                        break;
                    case SystemLanguage.ChineseTraditional:
                        language = SupportedLanguage.ChineseTraditional;
                        break;
                    case SystemLanguage.Indonesian:
                        language = SupportedLanguage.Indonesian;
                        break;
                    default:
                        language = SupportedLanguage.English;
                        break;
                }
                //SetLanguage(language);
            }

            return language;
        }
    }
}