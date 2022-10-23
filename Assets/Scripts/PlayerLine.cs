using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLine : MonoBehaviour
{
    public string text
    {
        get => inputField.text;
        set => inputField.text = value;
    }
    public TMP_InputField inputField;
    public GameManager.Player owner;
}
