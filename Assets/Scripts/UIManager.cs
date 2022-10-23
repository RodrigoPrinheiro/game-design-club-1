using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform playerPickPanel;
    [SerializeField] private RectTransform playerCountPanel;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private RectTransform playersNamePanel;
    [SerializeField] private RectTransform gamePanel;
    [SerializeField] private RectTransform afterGame;
    [SerializeField] private TextMeshProUGUI finalPoem;

    private RectTransform activePanel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        BackToMenu();
    }


    public void PickPlayersPanel()
    {
        playerCountText.text = GameManager.instance.PlayersCount.ToString();
        
        activePanel.gameObject.SetActive(false);
        activePanel = playerPickPanel;
        activePanel.gameObject.SetActive(true);

        playerCountPanel.gameObject.SetActive(true);
        playersNamePanel.gameObject.SetActive(false);
    }

    public void ShowGamePanel()
    {
        activePanel.gameObject.SetActive(false);
        gamePanel.gameObject.SetActive(true);

    }

    public void PickPlayerNamesPanel_ButtonCallback()
    {
        playerCountPanel.gameObject.SetActive(false);
        playersNamePanel.gameObject.SetActive(true);
    }

    public void IncrementPlayerCount_ButtonCallback()
    {
        GameManager.instance.PlayersCount++;
        playerCountText.text = GameManager.instance.PlayersCount.ToString();
    }

    public void AfterGamePanel(string poem)
    {
        activePanel.gameObject.SetActive(false);
        afterGame.gameObject.SetActive(true);
        activePanel = afterGame;

        finalPoem.text = poem;
    }

    public void ReducePlayerCount_ButtonCallback()
    {
        GameManager.instance.PlayersCount--;
        playerCountText.text = GameManager.instance.PlayersCount.ToString();
    }

    public void BackToMenu()
    {
        playerCountText.text = "0";
        if (activePanel != null)
            activePanel.gameObject.SetActive(false);
        
        mainMenu.gameObject.SetActive(true);
        activePanel = mainMenu;
    }
}
