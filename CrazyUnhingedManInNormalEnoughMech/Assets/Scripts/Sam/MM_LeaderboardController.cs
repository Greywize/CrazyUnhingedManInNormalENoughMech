using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_LeaderboardController : MonoBehaviour
{
    [SerializeField]
    private string testName;
    [SerializeField]
    private int testPlacement;
    [SerializeField]
    private int testScore;

    [SerializeField]
    private GameObject entryPrefab;
    // The object containing all our entry objects
    [SerializeField]
    private GameObject entryContainer;

    // If zero, don't limit entries
    [SerializeField]
    private int maxEntries;

    // List containing all current leaderboard entries
    [SerializeField]
    private List<MM_LeaderboardEntry> entryList;
    private List<GameObject> objectyList;

    private void Start()
    {
        if (!entryContainer.GetComponent<HorizontalLayoutGroup>())
        {
            Debug.LogWarning("GameoBject " + entryContainer.name + "should contain a horizontal layout group.");
        }
    }

    public MM_LeaderboardEntry CreateEntry(string playerName, int score)
    {
        MM_LeaderboardEntry entry = new MM_LeaderboardEntry();

        // Set name and score - placement is handled when the list is sorted
        entry.playerName = playerName;
        entry.score = score;

        return entry;
    }

    public void AddEntry(MM_LeaderboardEntry entry)
    {
        entryList.Add(entry);
    }
    public void SortByScore()
    {
        entryList.Sort(
            delegate (MM_LeaderboardEntry entry1, MM_LeaderboardEntry entry2)
            {
                return entry1.score.CompareTo(entry2.score);
            }
        );
    }
    public void DrawListObjects()
    {
        foreach (MM_LeaderboardEntry e in entryList)
        {
            GameObject entry = Instantiate(entryPrefab, entryContainer.transform);

            
        }
    }
}