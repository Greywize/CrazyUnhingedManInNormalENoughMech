using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_LeaderboardController : MonoBehaviour
{
    public struct Entry
    {
        public Entry(string name, float score)
        {
            Name =          name;
            Score =         score;
            Placement =     0;
        }

        public string Name      { get; set; }
        public float Score      { get; set; }
        public int Placement    { get; set; }
    }

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
    private List<Entry> entryList;

    private void Start()
    {
        if (!entryContainer.GetComponent<HorizontalLayoutGroup>())
        {
            Debug.LogWarning("GameoBject " + entryContainer.name + "should contain a horizontal layout group.");
        }
    }
    public void CreateEntry(string name, float score)
    {
        Entry entry = new Entry(name, score);

        AddEntry(entry);
    }
    public void CreateTestEntry()
    {
        Entry entry = new Entry(testName, testScore);

        AddEntry(entry);
    }
    public void AddEntry(Entry entry)
    {
        if (entryList.Count < maxEntries)
        {
            entryList.Add(entry);
        }
        else
        {
            Debug.LogWarning("Cannot add entry " + entry.Name + " to " + gameObject.name + "; Leaderboard is full.");
        }
    }
    public void RemoveEntry(Entry entry)
    {
        entryList.Remove(entry);
    }
    public void SortList()
    {
        entryList.Sort(
            delegate(Entry entry1, Entry entry2)
            {
                return entry1.Score.CompareTo(entry2.Score);
            });
    }
    public void InstantiateListObjects()
    {
        // Delete old leaderboard objects, if any
        foreach (Transform child in entryContainer.transform)
        {
            Destroy(child.transform);
        }
        // Create new objects
        foreach (Entry entry in entryList)
        {
            GameObject entryObject = Instantiate(entryPrefab, entryContainer.transform);

            entryObject.transform.Find("Placement").GetComponent<TMPro.TMP_Text>().text = entry.Placement.ToString();
            entryObject.transform.Find("Name").GetComponent<TMPro.TMP_Text>().text = entry.Name;
            entryObject.transform.Find("Score").GetComponent<TMPro.TMP_Text>().text = entry.Score.ToString();
        }
    }
    public void ClearList()
    {
        foreach (Transform child in entryContainer.transform)
        {
            Destroy(child.transform);
        }
        entryList.Clear();
    }
}