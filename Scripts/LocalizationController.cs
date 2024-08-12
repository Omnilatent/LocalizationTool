using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;


namespace Omnilatent.LocalizationTool
{
    public static class LocalizationController
    {
        public static bool
            enableAutoAddNotFoundEntry =
                false; //if true, when entry is not found (while playing in editor), it will be added to database

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
            LocalizeData
                data = SqlDataManager
                    .GetLocalizeData(
                        key); //GameDatabase.ServiceSQLConnection.Table<LocalizedData>().Where(x => x.key == key).FirstOrDefault();

            if (data != null)
            {
                ret = data.GetString(language);
                if (string.IsNullOrEmpty(ret))
                {
                    Debug.LogWarning($"No LocalizedData entry for [{key}] in [{language}]");
                    ret = data.GetString(SupportedLanguage.english);
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
                ret = key;
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
                    default:
                        language = SupportedLanguage.english;
                        break;
                    case SystemLanguage.Vietnamese:
                        language = SupportedLanguage.vietnamese;
                        break;
                    case SystemLanguage.Arabic:
                        language = SupportedLanguage.arabic;
                        break;
                    case SystemLanguage.ChineseSimplified:
                        language = SupportedLanguage.chinese_simplified;
                        break;
                    case SystemLanguage.ChineseTraditional:
                        language = SupportedLanguage.chinese_traditional;
                        break;
                    case SystemLanguage.French:
                        language = SupportedLanguage.french;
                        break;
                    case SystemLanguage.German:
                        language = SupportedLanguage.german;
                        break;
                    case SystemLanguage.Hebrew:
                        language = SupportedLanguage.hebrew;
                        break;
                    case SystemLanguage.Japanese:
                        language = SupportedLanguage.japanese;
                        break;
                    case SystemLanguage.Indonesian:
                        language = SupportedLanguage.indonesian;
                        break;
                    case SystemLanguage.Italian:
                        language = SupportedLanguage.italian;
                        break;
                    case SystemLanguage.Korean:
                        language = SupportedLanguage.korean;
                        break;
                    case SystemLanguage.Portuguese:
                        language = SupportedLanguage.portuguese;
                        break;
                    case SystemLanguage.Russian:
                        language = SupportedLanguage.russian;
                        break;
                    case SystemLanguage.Spanish:
                        language = SupportedLanguage.spanish;
                        break;
                    case SystemLanguage.Thai:
                        language = SupportedLanguage.thai;
                        break;
                    case SystemLanguage.Turkish:
                        language = SupportedLanguage.turkish;
                        break;
                }
                //SetLanguage(language);
            }

            return language;
        }

        public static void SetCustomFont(this TMPro.TMP_Text tmp)
        {
            SetCustomFont(tmp, LT_Setting.GetFontTMPCurrentLanguage());
        }

        public static void SetCustomFont(this TMPro.TMP_Text tmp, TMP_FontAsset adaptiveFont)
        {
            var currentFont = tmp.font;
            var mat = tmp.fontMaterial;
            if (adaptiveFont != null) tmp.font = adaptiveFont;
            var newMat = LT_Setting.GetCorrespondingMaterial(currentFont, adaptiveFont, mat);
            if (newMat != null)
            {
                tmp.fontMaterial = newMat;
                var subMeshesUI = tmp.GetComponentsInChildren<TMPro.TMP_SubMeshUI>();
                foreach (var item in subMeshesUI)
                {
                    item.material = newMat;
                }

                var subMeshes = tmp.GetComponentsInChildren<TMPro.TMP_SubMesh>();
                foreach (var item in subMeshes)
                {
                    item.material = newMat;
                }
            }

            bool isArabic = false;
            if (CurrentLanguage() == SupportedLanguage.arabic)
            {
                isArabic = ArabicUtils.IsArabic(tmp.text);
            }
            
            if (isArabic)
            {
                tmp.isRightToLeftText = true;
                tmp.text = ArabicUtils.FixArabicText(tmp.text);
            }
            else
            {
                tmp.isRightToLeftText = false;
            }
        }
    }

    public class ArabicUtils
    {
        // Define a set of characters to check against
        public static HashSet<char> Delimiters = new HashSet<char> { ' ' };

        public static bool IsArabic(string text)
        {
            foreach (char c in text)
            {
                if ((c >= 0x0600 && c <= 0x06FF) || // Arabic
                    (c >= 0x0750 && c <= 0x077F) || // Arabic Supplement
                    (c >= 0x08A0 && c <= 0x08FF) || // Arabic Extended-A
                    (c >= 0xFB50 && c <= 0xFDFF) || // Arabic Presentation Forms-A
                    (c >= 0xFE70 && c <= 0xFEFF)) // Arabic Presentation Forms-B
                {
                    return true;
                }
            }

            return false;
        }

        public static string FixArabicText(string text)
        {
            StringBuilder res = new StringBuilder(text.Length);
            int wordStartIndex = 0;

            for (int i = 0; i <= text.Length; i++)
            {
                // Check for the end of a word (if the character is in the delimiters set or end of string)
                if (i == text.Length || Delimiters.Contains(text[i]))
                {
                    int wordLength = i - wordStartIndex;
                    string word = text.Substring(wordStartIndex, wordLength);

                    // Check if the word is a number
                    if (int.TryParse(word, out _))
                    {
                        // Reverse the word
                        for (int j = wordLength - 1; j >= 0; j--)
                        {
                            res.Append(word[j]);
                        }
                    }
                    else
                    {
                        // Add the word as is
                        res.Append(word);
                    }

                    // Add the current character (space, quote) after the word
                    if (i < text.Length)
                    {
                        res.Append(text[i]);
                    }

                    wordStartIndex = i + 1;
                }
            }

            return res.ToString();
        }
    }
}