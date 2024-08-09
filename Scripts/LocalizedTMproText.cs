using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Omnilatent.LocalizationTool
{
    public class LocalizedTMproText : MonoBehaviour
    {
        [SerializeField] string id;
        [SerializeField] bool hasParam;
        [SerializeField] int param;
        [Tooltip("If true, search for supported font on active. If false, use font set by game locale setting")]
        [SerializeField] private bool _adaptiveFont;

        [SerializeField] private bool _loadTextOnEnable = true;

        TMP_Text m_Text;

        public TMP_Text TmpText
        {
            get => m_Text;
            set => m_Text = value;
        }

        protected virtual void OnEnable()
        {
            if (TmpText == null)
            {
                TmpText = GetComponent<TMP_Text>();
            }

            if (_loadTextOnEnable)
            {
                UpdateText();
            }
        }

        public virtual void SetText(string key)
        {
            id = key;
            UpdateText();
        }

        public virtual void UpdateText()
        {
            //m_Text.text = (!hasParam) ? Localizes.GetString(id) : string.Format(Localizes.GetString(id), param);
            
            TmpText.text = (!hasParam)
                ? LocalizationController.GetString(id)
                : string.Format(LocalizationController.GetString(id), param); //Use SQL
            UpdateFont();
        }

        public virtual void UpdateFont()
        {
            if (!_adaptiveFont)
            {
                TmpText.SetCustomFont();
            }
            else
            {
                CheckSupportedFont(TmpText.text);
            }
        }

        protected void CheckSupportedFont(string text)
        {
            TMP_FontAsset currentFont = TmpText.font;

            // Check if the current font supports all characters
            if (!CanDisplayAllCharacters(currentFont, text))
            {
                // Iterate through fallback fonts
                foreach (var fontData in LT_Setting.Instance.fontDatas)
                {
                    if (CanDisplayAllCharacters(fontData.fontAsset, text))
                    {
                        TmpText.font = fontData.fontAsset;
                        return;
                    }
                }
            }
            
            //failed to find supported font
            TmpText.SetCustomFont();
        }

        protected bool CanDisplayAllCharacters(TMP_FontAsset font, string text)
        {
            foreach (char c in text)
            {
                if (!font.HasCharacter(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}