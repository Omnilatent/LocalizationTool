using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Omnilatent.LocalizationTool
{
    public class SQLDataManager : MonoBehaviour, ILocalizeDataManager
    {
        public static string databaseName = "localize.db";

        public static DataService Service
        {
            get
            {
                if (Instance.service == null)
                {
                    InitService();
                }
                return Instance.service;
            }
        }

        public static SQLiteConnection ServiceSQLConnection { get => Service.Connection; }
        DataService service;

        public static SQLDataManager Instance
        {
            get
            {
                return instance;
            }
        }

        static SQLDataManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public static DataService GetDataServiceInEditMode()
        {
            if (Application.isPlaying)
                Debug.LogError("Do not call this in play mode");
            return new DataService(databaseName);
        }

        public static void InitService()
        {
            Instance.service = new DataService(databaseName);
        }

        private void OnApplicationQuit()
        {
            service.CloseConnection();
        }

        public LocalizeData GetLocalizeData(string key)
        {
            return ServiceSQLConnection.Table<LocalizeData>().Where(x => x.key == key).FirstOrDefault();
        }

        public void AddLocalizeData(LocalizeData newData)
        {
            ServiceSQLConnection.Insert(newData);
        }
    }

    public partial class DataService
    {
        private SQLiteConnection _connection;
        public const string PREF_EXIST_DATABASE_VERSION_CODE = "GameDatabaseVersion";

        public SQLiteConnection Connection { get => _connection; }

        public DataService(string DatabaseName)
        {
            InitConnection(DatabaseName);
        }

        async void InitConnection(string DatabaseName)
        {
            string currentDatabaseVersion = PlayerPrefs.GetString(PREF_EXIST_DATABASE_VERSION_CODE, "");
            bool requireUpdateDatabase = !currentDatabaseVersion.Equals(Application.version);
            Debug.Log($"database:{currentDatabaseVersion} app:{Application.version} update:{requireUpdateDatabase}");
#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
            string filePath = Path.Combine(Application.streamingAssetsPath, DatabaseName);
            if (!File.Exists(filePath))
            {
                Debug.LogError($"Please create default database first by Tools/Omnilatent/Localization Tool/Import Extra Package. \n\nDatabase 'localize.db' does not exist in {filePath}.");
                return;
            }
#else
            // check if file exists in Application.persistentDataPath
            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

            if (requireUpdateDatabase || !File.Exists(filepath))
            {
                Debug.Log("Database not in Persistent path");
                // if it doesn't ->
                // open StreamingAssets directory and load the db ->
#if UNITY_ANDROID
                var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
                float timeOut = 10f;
                while (!loadDb.isDone && timeOut > 0f)
                {
                    await Task.Delay(50);
                    timeOut -= 0.05f;
                }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                   // then save to Application.persistentDataPath
                File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath, true);

#endif
                PlayerPrefs.SetString(PREF_EXIST_DATABASE_VERSION_CODE, Application.version);
                PlayerPrefs.Save();
                Debug.Log("Database written");
            }

            var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create, true);
            Debug.Log("Final PATH: " + dbPath);
        }

        public void UpdateData(object obj)
        {
            _connection.Update(obj);
        }

        public void CloseConnection()
        {
            Debug.Log("Closing connection");
            _connection.Close();
            GC.Collect();
        }
    }
}