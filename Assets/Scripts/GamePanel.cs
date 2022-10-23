using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private RectTransform poemContent;
    [SerializeField] private Button submitLineButton;
    [SerializeField] private PlayerLine playerLinePrefab;
    [SerializeField] private Image hideStripePrefab;
    private RectTransform previousLine;
    private PlayerLine currentPlayerInputField;
    public GameManager.Player currentPlayer;
    public int currentPlayerTurn;
    public List<Image> hidePanels;
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        ClearContent();
        
        var firstLine = Instantiate(playerLinePrefab, poemContent);
        firstLine.text = GameManager.instance.CurrentPoemFirstLine;
        previousLine = firstLine.transform as RectTransform;
        currentPlayerTurn = 0;
    }

    public void InsertNewPlayerLine()
    {
        currentPlayerInputField = Instantiate(playerLinePrefab, poemContent);
    }

    public void HideLine(RectTransform fieldToHide)
    {
        var stripe = Instantiate(hideStripePrefab, poemContent);
        stripe.rectTransform.sizeDelta = fieldToHide.sizeDelta;
        stripe.rectTransform.localPosition = fieldToHide.localPosition;

        stripe.rectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(-5f, 5f));
    }

    public void ClearContent()
    {
        foreach(Transform t in poemContent)
        {
            Destroy(t.gameObject);
        }
    }
    
    public void SubmitLine_ButtonCallback()
    {
        currentPlayerTurn++;
        currentPlayerTurn %= GameManager.instance.PlayersCount;
        currentPlayer = GameManager.instance.players[currentPlayerTurn];

        previousLine = currentPlayerInputField.transform as RectTransform;
        currentPlayerInputField = null;
    }
    
}
