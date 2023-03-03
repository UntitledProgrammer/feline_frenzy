using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable] public struct PlayerStatistics
{
    //Attributes:
    public int survivalScore;
    private const string name = "player_statistics.json";

    //Constructor:
    public PlayerStatistics(int survivalScore)
    {
        this.survivalScore = survivalScore;
    }

    //Properties:
    public static string JsonPath { get => Application.persistentDataPath + name; }

    //Methods:
    public static PlayerStatistics Load()
    {
        if (!File.Exists(JsonPath)) return default;

        string jsonData = File.ReadAllText(JsonPath);
        PlayerStatistics result = JsonUtility.FromJson<PlayerStatistics>(jsonData);

        return result;
    }

    public static void Save(PlayerStatistics statistics)
    {
        string data = JsonUtility.ToJson(statistics);
        File.WriteAllText(JsonPath, data);
    }
}