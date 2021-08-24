using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.LocalizationTool
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] string id;

        Text m_Text;

        void OnEnable()
        {
            if (m_Text == null)
            {
                m_Text = GetComponent<Text>();
            }

            m_Text.text = LocalizationController.GetString(id);
        }
    }
}