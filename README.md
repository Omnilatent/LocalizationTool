# LocalizationTool
##Dependencies
- SQLite4Unity3d https://github.com/robertohuertasm/SQLite4Unity3d

## Setup
- To create default SQL database, click on Tools/Omnilatent/Localization Tool/Import Extra Package.

## Usage

- Use LocalizationController.GetString(string key) to get localized string in current language.

- You can also use shortcut call: [key string].Localize(). E.g. "Hello_world".Localize() will return localized string of "Hello_world"

- To localize text of a TMP_Text component using its text as key, add LocalizedTMproDirect component to the same gameObject with a TMP_Text component.

