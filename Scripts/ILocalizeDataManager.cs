using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.LocalizationTool
{
    public interface ILocalizeDataManager
    {
        LocalizeData GetLocalizeData(string key);
        void AddLocalizeData(LocalizeData localizedData);
    }
}