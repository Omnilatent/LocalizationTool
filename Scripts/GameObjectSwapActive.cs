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
                case SupportedLanguage.Vietnamese:
                    objectToShow = vietnameseObject;
                    break;
                case SupportedLanguage.ChineseSimplified:
                    objectToShow = chineseSimplifiedObject;
                    break;
                case SupportedLanguage.ChineseTraditional:
                    objectToShow = chineseTraditionalObject;
                    break;
                case SupportedLanguage.Korean:
                    objectToShow = koreanObject;
                    break;
                case SupportedLanguage.Japanese:
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