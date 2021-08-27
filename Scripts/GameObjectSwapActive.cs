using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.LocalizationTool
{
    public class GameObjectSwapActive : MonoBehaviour
    {
        [SerializeField] GameObject defaultObject;
        [SerializeField] GameObject vietnameseObject;
        [SerializeField] GameObject chineseSimplifiedObject;
        [SerializeField] GameObject chineseTraditionalObject;
        [SerializeField] GameObject koreanObject;
        [SerializeField] GameObject japaneseObject;

        private void OnEnable()
        {
            string language = LocalizationController.CurrentLanguage();
            GameObject objectToShow = defaultObject;
            switch (language)
            {
                case SupportedLanguage.vietnamese:
                    objectToShow = vietnameseObject;
                    break;
                case SupportedLanguage.chinese_simplified:
                    objectToShow = chineseSimplifiedObject;
                    break;
                case SupportedLanguage.chinese_traditional:
                    objectToShow = chineseTraditionalObject;
                    break;
                case SupportedLanguage.korean:
                    objectToShow = koreanObject;
                    break;
                case SupportedLanguage.japanese:
                    objectToShow = japaneseObject;
                    break;
            }

            defaultObject.SetActive(false);
            vietnameseObject.SetActive(false);
            chineseSimplifiedObject.SetActive(false);
            chineseTraditionalObject.SetActive(false);
            koreanObject.SetActive(false);
            japaneseObject.SetActive(false);

            objectToShow.SetActive(true);
        }
    }
}