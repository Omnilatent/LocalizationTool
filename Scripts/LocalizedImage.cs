using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedImage : MonoBehaviour
{
    [SerializeField] Sprite m_VietnameseSprite;
    [SerializeField] Sprite m_EnglishSprite;

    Image m_Image;

    void OnEnable()
    {
        if (m_Image == null)
        {
            m_Image = GetComponent<Image>();
        }

        string language = Localizes.CurrentLanguage();

        switch (language)
        {
            case SupportedLanguage.vietnamese:
                m_Image.sprite = m_VietnameseSprite;
                break;

            case SupportedLanguage.english:
                m_Image.sprite = m_EnglishSprite;
                break;
        }

        m_Image.SetNativeSize();
    }
}
