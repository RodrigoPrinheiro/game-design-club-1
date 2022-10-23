using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class GameManager : MonoBehaviour
{
    public struct Player
    {
        public string name;
        public Color color;
    }
    public enum GameMode
    {
        Freestyle,
        HotPen
    }
    private StringBuilder currentFullPoem;

    public static GameManager instance;
    public List<Player> players;
    private int playerCount;
    
    public GameMode Mode {get; set;}
    public bool AllPlayersReady => playerCount == players.Count;
    public int PlayersCount
    {
        get
        {
            return playerCount;
        }
        set
        {
            playerCount = Mathf.Clamp(value, 1, 4);
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        players = new List<Player>();
        playerCount = 1;
        currentFullPoem = new StringBuilder();
    }

    public void AddPlayer(string name, Color color)
    {
        players.Add(new Player() {name = name, color = color});
    }
    
}
