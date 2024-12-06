using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    private Transform scrollableList;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        scrollableList = transform.Find("ScrollableList");
        entryContainer = scrollableList.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //Test Entry: AddHighscoreEntry(1000, "DOC", 2000, 01, 01);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        highscoreEntryList = highscores.highscoreEntryList;

        //Sorting List
        for (int i = 0; i < highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < highscoreEntryList.Count; j++)
            {
                if (highscoreEntryList[j].score < highscoreEntryList[i].score)
                {  
                    HighscoreEntry tmp = highscoreEntryList[i];
                    highscoreEntryList[i] = highscoreEntryList[j];
                    highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
         }
    }
 
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        int rank = transformList.Count + 1;
        int score = highscoreEntry.score;
        string name = highscoreEntry.name;
        int year = highscoreEntry.year;
        int month = highscoreEntry.month;
        int day = highscoreEntry.day;
        string dateTrans;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryTransform.gameObject.SetActive(true);

        //Change date array into string
        dateTrans = year + "/";
        if (month > 9) {
            dateTrans += (month.ToString()) + "/";
        }
        else {
            dateTrans += "0" + (month.ToString()) + "/";
        }

        if (day > 9) {
            dateTrans += (day.ToString());
        }
        else {
            dateTrans += "0" + (day.ToString());
        }

        entryTransform.Find("rankLabel").GetComponent<UnityEngine.UI.Text>().text = (rank.ToString());
        entryTransform.Find("scoreLabel").GetComponent<UnityEngine.UI.Text>().text = (score.ToString());
        entryTransform.Find("nameLabel").GetComponent<UnityEngine.UI.Text>().text = name;
        entryTransform.Find("dateLabel").GetComponent<UnityEngine.UI.Text>().text = dateTrans;

        transformList.Add(entryTransform);
    }

    /* HARD INPUT SYSTEM
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
    */

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
