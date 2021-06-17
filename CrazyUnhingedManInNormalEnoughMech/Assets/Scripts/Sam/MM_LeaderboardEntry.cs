using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_LeaderboardEntry : ScriptableObject
{
    public int placement;
    public string playerName;
    public float score;

    [SerializeField]
    private TMPro.TMP_Text playerNameText;
    [SerializeField]
    private TMPro.TMP_Text placementText;
    [SerializeField]
    private TMPro.TMP_Text scoreText;

    public void SetValues()
    {
        playerNameText.text = playerName;
        placementText.text = placement.ToString();
        scoreText.text = score.ToString();
    }
}