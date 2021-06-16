using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGelements;
using UnityEngine.SceneManagement;
using AI;

[ExecuteAlways]
public class GameManager : MonoBehaviour
{
    #region PUBLIC MEMBERS 
    public PlayerMech player;
  // [SerializeField] public List<AgentBehaviour> agents;
    public AgentBehaviour[] AllAgents;

    #endregion

    #region MONOBEHAVIOURS 
    private void OnEnable()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, player.transform.position);

        foreach (AgentBehaviour agent in AllAgents)
            Gizmos.DrawLine(transform.position, agent.transform.position);
    }

    private void Awake()
    {
       AllAgents = GameObject.FindObjectsOfType<AgentBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // GameOver();
    }

    #endregion

    #region FUNCTIONS

    /// <summary>
    /// Players health has reached 0
    /// Load gameover scene
    /// </summary>
    public void GameOver()
    {
        if(player.currentHealth <= 0)
        {
            // SceneManager.LoadSceneAsync(2);
            Debug.Log($"Player has died : Method not implemented");
        }
    }

    #endregion
}
