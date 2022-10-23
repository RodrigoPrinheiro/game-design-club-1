using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerNextText;
    [SerializeField] private TMP_FontAsset playerFontAsset;
    [SerializeField] private RectTransform hotpenGraphicsHeader;
    [SerializeField] private Image hotpenModeTimer;
    [SerializeField] private RectTransform poemContent;
    [SerializeField] private Button submitLineButton;
    [SerializeField] private PlayerLine playerLinePrefab;
    [SerializeField] private Image hideStripePrefab;
    private RectTransform previousLine;
    private PlayerLine currentPlayerInputField;
    public GameManager.Player currentPlayer;
    public int currentPlayerTurn;
    public List<Image> hidePanels;
    private int createdLines = 0;
    private float playerTimer;
    public int NextPlayer
    {
        get
        {
            int nextPlayer = currentPlayerTurn + 1;
            nextPlayer %= GameManager.instance.PlayersCount;
            return nextPlayer;
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        hotpenModeTimer.fillAmount = 1f;
        if (GameManager.instance.Mode != GameManager.GameMode.HotPen)
        {
            hotpenGraphicsHeader.gameObject.SetActive(true);
        }
        else
        {
            hotpenGraphicsHeader.gameObject.SetActive(false);
        }

        ClearContent();

        var firstLine = Instantiate(playerLinePrefab, poemContent);
        firstLine.inputField.readOnly = true;
        firstLine.text = GameManager.instance.CurrentPoemFirstLine;
        previousLine = firstLine.transform as RectTransform;
        currentPlayerTurn = 0;
        createdLines = 0;

        currentPlayer = GameManager.instance.players[currentPlayerTurn];
        SetPlayerNames();

        InsertNewPlayerLine();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (!GameManager.GameRunning) return;
        
        if (GameManager.instance.IsHotPen)
        {
            playerTimer += Time.deltaTime;

            float percent = playerTimer / GameManager.instance.MaxHotPenTime;
            hotpenModeTimer.fillAmount = 1 - percent;

            if (playerTimer >= GameManager.instance.MaxHotPenTime)
            {
                FinishGame();
            }
        }
    }

    public void InsertNewPlayerLine()
    {
        currentPlayerInputField = Instantiate(playerLinePrefab, poemContent);
        currentPlayerInputField.inputField.textComponent.color = currentPlayer.color;
        currentPlayerInputField.inputField.fontAsset = playerFontAsset;

        currentPlayerInputField.inputField.Select();
    }

    public void HideLine(RectTransform fieldToHide)
    {
        TMP_InputField inputField = fieldToHide.GetComponent<TMP_InputField>();
        var stripe = Instantiate(hideStripePrefab, poemContent);
        stripe.rectTransform.sizeDelta = fieldToHide.sizeDelta;
        stripe.rectTransform.localPosition = fieldToHide.localPosition;

        stripe.color = inputField.textComponent.color;
        stripe.rectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));

        hidePanels.Add(stripe);
    }

    public void ClearContent()
    {
        foreach (Transform t in poemContent)
        {
            Destroy(t.gameObject);
        }
    }

    public void SubmitLine_ButtonCallback()
    {
        if (createdLines == GameManager.instance.TotalLines - 1)
        {
            FinishGame();
            return;
        }

        HideLine(previousLine);

        currentPlayerTurn = NextPlayer;
        currentPlayer = GameManager.instance.players[currentPlayerTurn];
        
        previousLine = currentPlayerInputField.transform as RectTransform;
        GameManager.instance.SetPoemLine(currentPlayerInputField.text);
        currentPlayerInputField = null;
        SetPlayerNames();
        InsertNewPlayerLine();
        createdLines++;
    }

    public void FinishGame()
    {
        GameManager.FinishGame();
        Debug.Log("game ended");
    }

    public void SetPlayerNames()
    {
        playerNameText.text = currentPlayer.name;
        playerNameText.color = currentPlayer.color;
        playerNextText.text = GameManager.instance.players[NextPlayer].name;
        playerNextText.color = GameManager.instance.players[NextPlayer].color;

        hotpenModeTimer.color = currentPlayer.color;
    }

}
