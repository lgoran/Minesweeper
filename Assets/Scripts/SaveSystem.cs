using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveHighScores(double[] easy, double[] medium, double[] hard)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "scores.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        HighScoresData data = new HighScoresData(easy,medium,hard);
        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static HighScoresData LoadHighScores()
    {
        string path = Application.persistentDataPath + "scores.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            HighScoresData data = formatter.Deserialize(stream) as HighScoresData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found: " + path);
            return null;
        }
    }
}
