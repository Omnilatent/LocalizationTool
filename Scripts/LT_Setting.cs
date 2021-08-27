using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Omnilatent.LocalizationTool
{
    [CreateAssetMenu(fileName = "LocalizationTool Settings", menuName = "Omnilatent/LocalizationTool Settings")]
    public class LT_Setting : ScriptableObject
    {
        private static LT_Setting s_Instance;

        /// <summary>
        /// Get a singleton instance of the settings class.
        /// </summary>
        public static LT_Setting Instance
        {
            get
            {
                if (LT_Setting.s_Instance == null)
                {
                    LT_Setting.s_Instance = Resources.Load<LT_Setting>("LocalizationTool Settings");

#if UNITY_EDITOR
                    // Make sure TextMesh Pro UPM packages resources have been added to the user project
                    if (LT_Setting.s_Instance == null)
                    {
                        // Open TMP Resources Importer
                        //TMP_PackageResourceImporterWindow.ShowPackageImporterWindow();
                        Debug.LogError("Please import LocalizationTool extra package");
                    }
#endif
                }
                return LT_Setting.s_Instance;
            }
        }

        [SerializeField] TMP_FontAsset vietnameseFont;
        [SerializeField] TMP_FontAsset chineseSimplifiedFont;
        [SerializeField] TMP_FontAsset chineseTraditionalFont;
        [SerializeField] TMP_FontAsset koreanFont;
        [SerializeField] TMP_FontAsset japaneseFont;

        public static TMP_FontAsset GetFontTMPCurrentLanguage()
        {
            string language = LocalizationController.CurrentLanguage();
            TMP_FontAsset fontAsset = null;
            switch (language)
            {
                case SupportedLanguage.vietnamese:
                    fontAsset = Instance.vietnameseFont;
                    break;
                case SupportedLanguage.chinese_simplified:
                    fontAsset = Instance.chineseSimplifiedFont;
                    break;
                case SupportedLanguage.chinese_traditional:
                    fontAsset = Instance.chineseTraditionalFont;
                    break;
                case SupportedLanguage.korean:
                    fontAsset = Instance.koreanFont;
                    break;
                case SupportedLanguage.japanese:
                    fontAsset = Instance.japaneseFont;
                    break;
            }
            if (fontAsset == null)
                fontAsset = TMP_Settings.defaultFontAsset;
            return fontAsset;
        }

        [SerializeField] List<TextStylePreset> stylePresets;

        public static TextStylePreset GetStylePresetTMPCurrentLanguage(string tag)
        {
            string language = LocalizationController.CurrentLanguage();
            for (int i = 0; i < Instance.stylePresets.Count; i++)
            {
                if (Instance.stylePresets[i].language == language && Instance.stylePresets[i].tag == tag)
                {
                    return Instance.stylePresets[i];
                }
            }
            return null;
        }
    }
}