using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetGameMode : MonoBehaviour
{
    [SerializeField] private GameManager.GameMode mode;
    [SerializeField] private bool addToButton = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (addToButton)
        {
            GetComponent<Button>()?.onClick.AddListener(Set);
        }
    }
    public void Set()
    {
        GameManager.instance.Mode = mode;
    }
}
