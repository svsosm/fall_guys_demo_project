using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Opponents.Character;

public class RankingManager : Singleton<RankingManager>
{

    //Gameobjects
    [SerializeField] private GameObject prefabRankLabel;
    [SerializeField] private GameObject rankingArea;
    private Characters[] characters;
    private GameObject[] rankRows;

    //Lists
    public Dictionary<string, float> keyValues;
    private IOrderedEnumerable<KeyValuePair<string, float>> items;

    //variables
    private int increment = 0;
    private int finishedPlayerCount = 0;

    //tags and layers
    private const string boyTag = "Boy";

    void Start()
    {
        Initialize();

        CreaterRankRowsListAndKeyValues();

    }

    private void Initialize()
    {
        characters = Object.FindObjectsOfType<Characters>();
        rankRows = new GameObject[characters.Length];
        keyValues = new Dictionary<string, float>();
    }

    private void Update()
    {
        UpdateCharacterRank();
    }
    
    
    void CreaterRankRowsListAndKeyValues()
    {

        for(int i = 0; i < characters.Length; i++)
        {
            //Create Rank Row Text
            GameObject newText = Instantiate(prefabRankLabel);
            TMP_Text characterName = newText.GetComponent<TMP_Text>();
            characterName.text = $"{i + 1}.{characters[i].GetCharacterName()}";
            newText.transform.SetParent(rankingArea.transform);
            newText.transform.localScale = Vector3.one;
            //Create Keyvalues dictionary for ranking
            keyValues.Add(characters[i].GetCharacterName(), characters[i].GetPositionZ());
            //add all text in list
            rankRows[i] = newText;
        }

    }

    void UpdateCharacterRank()
    {
        items = from pair in keyValues orderby pair.Value descending select pair; //sort list by value(position Z)
        increment = 0 + finishedPlayerCount;
        foreach (KeyValuePair<string, float> pair in items)
        {
            rankRows[increment].GetComponent<TMP_Text>().text = $"{increment + 1}.{pair.Key}";

            if(pair.Key.Equals(boyTag))
            {
                rankRows[increment].GetComponent<TMP_Text>().color = Color.red; //change player text color
            }
            else
            {
                rankRows[increment].GetComponent<TMP_Text>().color = Color.white;
            }
            increment++;
        }

    }

    public int IncrementFinishedPlayerCount()
    {
        return finishedPlayerCount++;
    }
    
}
