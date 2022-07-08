using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEngine.UI;
//#if UNITY_EDITOR
using UnityEditor;
//#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string username;
    public int score;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        LoadDataFunction();
    }

    public void StartFunction()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitFunction()
    {
        SaveDataFunction();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void UsernameFunction()
    {
        TMP_InputField field = FindObjectOfType<TMP_InputField>();
        username = field.text;
        SaveDataFunction();
        UpdateText();
    }

    [Serializable]
    class SaveData
    {
        public string username;
        public int score;
    }

    public void SaveDataFunction()
    {
        SaveData data = new SaveData();
        data.username = username;
        data.score = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);
    }
    public void LoadDataFunction()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            username = data.username;
            score = data.score;

            TMP_InputField field = FindObjectOfType<TMP_InputField>();
            field.text = username;

            UpdateText();
        }

    }

    public void UpdateText()
    {
        GameObject.Find("Best Score").GetComponent<Text>().text = "Best Score: " + username + " : " + score;
    }

}
