using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHighscore : MonoBehaviour
{
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    
    public void AddHighscoreEntry(int score, string name, int year, int month, int day)
    {
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry{score = score, name = name, year = year, month = month, day = day};

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscoreEntryList = highscores.highscoreEntryList;

        //Add new entry to highscore
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
        public int year;
        public int month;
        public int day;
    }
}
