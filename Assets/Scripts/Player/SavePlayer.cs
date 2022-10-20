using UnityEngine;
using System.IO;
public class SavePlayer
{
    [System.Serializable]
    public class PlayerData
    {
        //fields
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 camRotation;

        public override string ToString() 
        {
            return $"Position: {position}, Rotation: {rotation}";
        }

        //constuctors
        public PlayerData()
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
            camRotation = Vector3.zero;
        }

        public PlayerData(Vector3 pos, Quaternion rot, Quaternion camRot)
        {
            position = pos;
            rotation = rot;
            camRotation = camRot.eulerAngles;
        }

        public PlayerData(Transform playerTrans, Transform camTrans)
        {
            position = playerTrans.position;
            rotation = playerTrans.rotation;
            camRotation = camTrans.rotation.eulerAngles;
        }
    }

    public static void SavePlayerData(PlayerData data, string fileName = "PlayerData.json")
    {
        //serialize to json string
        string json = JsonUtility.ToJson(data, true);

        string savePath = Path.Combine(Application.persistentDataPath, fileName);

        //check if file exists
        if (!File.Exists(savePath))
        {
            //if file not exists, create and close (omitting close leaves the file open)
            File.Create(savePath).Close();
        }

        using (StreamWriter sw = new StreamWriter(savePath))
        {
            sw.Write(json);
            Debug.Log($"Saved player data at {savePath}");
        }
    }

    public static void LoadPlayerData(out PlayerData data, string fileName = "PlayerData.json") 
    {
        //create null playerdata object
        data = null;

        string savePath = Path.Combine(Application.persistentDataPath, fileName);

        //check if file exists
        if (!File.Exists(savePath))
        {
            //there is no saved data
            Debug.Log($"No saved data found at {savePath}");
            return;
        }

        string json = "";
        using (StreamReader sr = new StreamReader(savePath))
        {
            json = sr.ReadToEnd();
        }

        data = JsonUtility.FromJson<PlayerData>(json);
    }
}
