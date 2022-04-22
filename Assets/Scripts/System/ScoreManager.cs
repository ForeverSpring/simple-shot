using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : PersistentSingleton<ScoreManager>
{
    [System.Serializable]
    public class PlayerScore {
        public int score;
        public string playerName;
        public string datetime;
        public PlayerScore(int score, string playerName,string datetime) {
            this.score = score;
            this.playerName = playerName;
            this.datetime = datetime;
        }
    };
    [System.Serializable]
    public class PlayerScoreData {
        public List<PlayerScore> list = new List<PlayerScore>();
    }
    public static readonly string SaveFileName = "player_score.json";
    public void SaveNewPlayerScoreData(int score,string name) {
        var playerScoreData = LoadPlayerScoreData();
        playerScoreData.list.Add(new PlayerScore(score, name, System.DateTime.Now.ToString("yy/MM/dd HH:mm")));
        playerScoreData.list.Sort((s1, s2) => s2.score.CompareTo(s1.score));
        SaveSystem.SaveByJson(SaveFileName, playerScoreData);
    }

    public PlayerScoreData LoadPlayerScoreData() {
        var playerScoreData = new PlayerScoreData();
        if (SaveSystem.SaveFileExists(SaveFileName)) {
            playerScoreData = SaveSystem.LoadFromJson<PlayerScoreData>(SaveFileName);
        }
        else {
            while (playerScoreData.list.Count < 10) {
                playerScoreData.list.Add(new PlayerScore(0, "NoName", "00/00/00 00:00"));
            }
            SaveSystem.SaveByJson(SaveFileName, playerScoreData);
        }
        return playerScoreData;
    }
}
