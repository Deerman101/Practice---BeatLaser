using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SpawnerScript;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] float beats = (60 / 125) * 2;
    [SerializeField] float timer;
    [SerializeField] GameObject[] cubes;
    [SerializeField] Transform[] dots;

    public bool isRandomSpawn = false;
    public AudioSource audioSource;
    public TMP_Dropdown musicChoiсe;
    public string jsonFilePath;
    public string jsonText;
    public ButtonManager buttonManager;

    [System.Serializable]
    public class CubeData
    {
        public int _dot;
        public float _timeToSpawn;
    }

    public List<CubeData> cubeDataList;

    IEnumerator SpawnCubes()
    {
        foreach (CubeData cubeData in cubeDataList)
        {
            //yield return new WaitForSeconds(cubeData._timeToSpawn);
            while (audioSource.time < cubeData._timeToSpawn)
            {
                yield return null;
            }
            int placeOfCube = cubeData._dot;
            int colorOfCube = placeOfCube > 1 ? 1 : 0;
            GameObject cube = Instantiate(cubes[colorOfCube], dots[placeOfCube]);
            cube.transform.localPosition = Vector3.zero;
            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
        }
    }

    void Update()
    {
        Debug.Log("Выбрана музыка # " + musicChoiсe.value);

        if (isRandomSpawn)
        {
            Debug.Log("Рандом!");

            if (timer > beats)
            {
                int colorOfCube = Random.Range(0, 2);
                int placeOfCube = colorOfCube == 0 ? Random.Range(0, 2) : Random.Range(2, 4);
                GameObject cube = Instantiate(cubes[colorOfCube], dots[placeOfCube]);

                cube.transform.localPosition = Vector3.zero;
                cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
                timer -= beats;
            }
            timer += Time.deltaTime;
        }

        if (!isRandomSpawn && buttonManager.isSpawn)
        {
            Time.timeScale = 1.0f;

            Debug.Log("Не рандом!");

            switch (musicChoiсe.value)
            {
                case 0:
                    //jsonFilePath = "Assets/DataSongs/theFatRat.json";
#if UNITY_EDITOR
                    jsonFilePath = Application.dataPath + "/DataSongs/theFatRat.json";
#elif UNITY_ANDROID
                    jsonFilePath = Path.Combine(Application.persistentDataPath, "theFatRat.json");
#endif
                    jsonText = File.ReadAllText(jsonFilePath);
                    cubeDataList = JsonConvert.DeserializeObject<List<CubeData>>(jsonText);
                    StartCoroutine(SpawnCubes());
                    break;
                case 1:
                    //jsonFilePath = "Assets/DataSongs/beatSaber.json";
#if UNITY_EDITOR
                    jsonFilePath = Application.dataPath + "/DataSongs/beatSaber.json";
#elif UNITY_ANDROID
                    jsonFilePath = Path.Combine(Application.persistentDataPath, "beatSaber.json");
#endif
                    jsonText = File.ReadAllText(jsonFilePath);
                    cubeDataList = JsonConvert.DeserializeObject<List<CubeData>>(jsonText);
                    StartCoroutine(SpawnCubes());
                    break;
                default:
                    Debug.Log("Что-то пошло не так, эм, ладно...");
                    break;
            }
            buttonManager.isSpawn = false;
        }
    }
}
