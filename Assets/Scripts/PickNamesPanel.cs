using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class PickNamesPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button[] colorButtons;
    [SerializeField] private TextMeshProUGUI currentPoet;
    private Button selectedColor;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        currentPoet.text = "Poet " + (GameManager.instance.players.Count + 1).ToString();
    }

    public void SelectColor_ButtonCallback(Button colorButton)
    {
        selectedColor = colorButton;
        submitButton.image.color = selectedColor.GetComponentInChildren<ProceduralImage>().color;
    }

    public void SubmitPlayer_ButtonCalllback()
    {
        if (selectedColor == null)
        {
            return;
        }

        GameManager.instance.AddPlayer(string.IsNullOrEmpty(inputField.text) ? "Jeremiah" : inputField.text, selectedColor.GetComponentInChildren<ProceduralImage>().color);
        selectedColor.transform.Find("Lock")?.gameObject.SetActive(true);
        selectedColor.interactable = false;

        NextPoet();
    }

    private void NextPoet()
    {
        if (GameManager.instance.AllPlayersReady)
        {
            // Continue to game panel

            return;
        }
        else
        {
            currentPoet.text = "Poet " + (GameManager.instance.players.Count + 1).ToString();
        }

        submitButton.image.color = Color.black;
        inputField.text = "";
    }
}
