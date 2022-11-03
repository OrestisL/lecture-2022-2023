using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;

public class Util
{
    [Serializable]
    public class Highscore
    {
        public string name;
        public int score;

        public Highscore(string n, int s)
        {
            name = n;
            score = s;
        }
    }

    [Serializable]
    public class HighscoreArray
    {
        public List<Highscore> highscoreList = new List<Highscore>();

        public HighscoreArray(List<Highscore> list)
        {
            highscoreList = list;
        }
    }

    [Serializable]
    public class AudioSettings 
    {
        public float bgVolume;
        public float sfxVolume;

        public static AudioSettings defaultSettings = new AudioSettings(1, 1);
        public AudioSettings(float bg, float sfx)
        {
            bgVolume = bg;
            sfxVolume = sfx;
        }
    }

    public static List<Highscore> LoadHighscores()
    {
        string path = Path.Combine(Application.persistentDataPath, "Highscores.json");
        //check if file exists
        if (!File.Exists(path))
        {
            Debug.Log("No highscores found");
            return null;
        }

        //read file
        string json = "";
        using (StreamReader sr = new StreamReader(path))
        {
            json = sr.ReadToEnd();
        }

        //parse json to list
        HighscoreArray hsa = JsonUtility.FromJson<HighscoreArray>(json);

        if (hsa == null)
        {
            return null;
        }

        //sort list descending with score
        hsa.highscoreList = SortListTDescendingDistinct(hsa.highscoreList);

        return hsa.highscoreList;
    }

    public static void SaveScore(string name, int score)
    {
        string path = Path.Combine(Application.persistentDataPath, "Highscores.json");
        //check if file exists
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        List<Highscore> list = LoadHighscores();
        if (list == null) //the file was created, but it was empty
        {
            list = new List<Highscore>();
        }

        list.Add(new Highscore(name, score));

        list = SortListTDescendingDistinct(list);

        HighscoreArray hsa = new HighscoreArray(list);

        string json = JsonUtility.ToJson(hsa, true);

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(json);
        }

    }

    private static List<Highscore> SortListTDescendingDistinct(List<Highscore> list)
    {
        return list.OrderByDescending(x => x.score)
            .GroupBy(x => x.name).Select(x => x.FirstOrDefault()).ToList(); 
    }

    public static void SaveSoundSettings(AudioSettings AS)   
    {
        string path = Path.Combine(Application.persistentDataPath, "AudioSettings.json");
        //check if file exists
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        string json = "";
        json = JsonUtility.ToJson(AS, true);

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(json);
        }
    }

    public static AudioSettings LoadSoundSettings() 
    {
        string path = Path.Combine(Application.persistentDataPath, "AudioSettings.json");
        //check if file exists
        if (!File.Exists(path))
        {
            return AudioSettings.defaultSettings;
        }

        string json = "";
        using (StreamReader sr = new StreamReader(path))
        {
            json = sr.ReadToEnd();
        }

        return JsonUtility.FromJson<AudioSettings>(json);

    }

    public static T LoadFromJson<T>(string fileName) 
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        //check if file exists
        if (!File.Exists(path))
        {
            return default(T);
        }

        string json = "";
        using (StreamReader sr = new StreamReader(path))
        {
            json = sr.ReadToEnd();
        }

        return JsonUtility.FromJson<T>(json);
    }

    public static void SaveToJson<T>(T data, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        //check if file exists
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        string json = JsonUtility.ToJson(data, true);

        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(json);
        }
    }

    public static IEnumerator LoadSceneAsync(int index, Button b)
    {
        //disable button
        b.interactable = false;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        //disable auto activation
        asyncOp.allowSceneActivation = false;

        //check if loading is done
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                //load the scene
                asyncOp.allowSceneActivation = true;
                yield return null;
            }

            //avoid infinite loop
            yield return null;
        }
    }

    public static IEnumerator LoadSceneAsync(string name, Button b)
    {
        //disable button
        b.interactable = false;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(name);
        //disable auto activation
        asyncOp.allowSceneActivation = false;

        //check if loading is done
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                //load the scene
                asyncOp.allowSceneActivation = true;
                yield return null;
            }

            //avoid infinite loop
            yield return null;
        }
    }
}
