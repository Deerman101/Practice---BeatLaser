using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;

public class SaveData : MonoBehaviour
{
    public Slider musicSlider;
    public TMP_Dropdown countOfCubesDropdown;
    public TMP_Dropdown musicChoiñe;
    public Button saveButton;

    [SerializeField] List<Record> recordList = new List<Record>();

    [System.Serializable]
    public class Record
    {
        public int _dot;
        public float _timeToSpawn;
    }

    private void Start()
    {
        saveButton.onClick.AddListener(SaveToJSON);
    }

    public void SaveToJSON()
    {
        int numRecords = countOfCubesDropdown.value;
        Debug.Log("Êîë-âî çàïèñåé: " + numRecords);
        if (numRecords != 0)
        {
            for (int i = 0; i < numRecords; i++)
            {
                Record newRecord = new Record
                {
                    _dot = Random.Range(0, 4),
                    _timeToSpawn = musicSlider.value
                };
                Debug.Log($"Ñîäåðæèìîå ñòðîêè {i}: " + newRecord._dot + "; " + newRecord._timeToSpawn);
                recordList.Add(newRecord);

                string filePath = "";


                string json = Newtonsoft.Json.JsonConvert.SerializeObject(recordList);
                if (musicChoiñe.value == 0)
                {
#if UNITY_EDITOR
                    filePath = Application.dataPath + "/DataSongs/theFatRat.json";
#elif UNITY_ANDROID
                    filePath = Path.Combine(Application.persistentDataPath, "theFatRat.json");
#endif

                    File.WriteAllText(filePath, json);
                }
                else if(musicChoiñe.value == 1)
                {
#if UNITY_EDITOR
                    filePath = Application.dataPath + "/DataSongs/beatSaber.json";
#elif UNITY_ANDROID
                    filePath = Path.Combine(Application.persistentDataPath, "beatSaber.json");
#endif

                    File.WriteAllText(filePath, json);
                }
            }
        }
    }
}