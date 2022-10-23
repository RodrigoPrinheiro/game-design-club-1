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

    [SerializeField] private PoemGenerator generator;
    [SerializeField] private int linesPerPlayer = 4;
    [SerializeField] private float maxHotPenTime = 140;
    [SerializeField] private UIManager ui;
    public bool IsHotPen => GameManager.instance.Mode == GameManager.GameMode.HotPen;
    public static event System.Action onFinishGame;
    public float MaxHotPenTime => maxHotPenTime;
    private StringBuilder currentFullPoem;
    public static GameManager instance;
    public List<Player> players;
    private int playerCount;
    public string CurrentPoemFirstLine {get; private set;}
    public GameMode Mode {get; set;}
    public int TotalLines {get; private set;}
    public bool AllPlayersReady => playerCount == players.Count;
    public static bool GameRunning {get; private set;}
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

    public void SetPoemLine(string line)
    {
        currentFullPoem.Append(line + "\n");
    }

    private void PickPoemStart()
    {
        Poem p = generator.SetNewPoem();
        CurrentPoemFirstLine = generator.FirstLine;
        currentFullPoem.Append(CurrentPoemFirstLine + "\n");
    }

    private void ShowGamePanel()
    {
        ui.ShowGamePanel();
    }

    public static void StartGame()
    {
        GameRunning = true;
        instance.TotalLines = instance.playerCount * instance.linesPerPlayer;
        instance.currentFullPoem.Clear();

        instance.PickPoemStart();
        instance.ShowGamePanel();
    }

    public static void FinishGame()
    {
        GameRunning = false;
        instance.ui.AfterGamePanel(instance.currentFullPoem.ToString());
        onFinishGame?.Invoke();
    }
    
}
