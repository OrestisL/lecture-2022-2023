using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Util
{
    [System.Serializable]
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

    [System.Serializable]
    public class HighscoreArray
    {
        public List<Highscore> highscoreList = new List<Highscore>();

        public HighscoreArray(List<Highscore> list)
        {
            highscoreList = list;
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
        hsa.highscoreList = hsa.highscoreList.OrderByDescending(x => x.score)
            .GroupBy(x => x.name).Select(x => x.FirstOrDefault()).ToList();

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

        HighscoreArray hsa = new HighscoreArray(list);

        string json = JsonUtility.ToJson(hsa);

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
