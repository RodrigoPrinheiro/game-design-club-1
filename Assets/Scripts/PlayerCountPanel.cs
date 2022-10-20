using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCountPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI count;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        leftButton.onClick.AddListener(PlayCountAnim_ButtonCallback);
        rightButton.onClick.AddListener(PlayCountAnim_ButtonCallback);
    }

    private void PlayCountAnim_ButtonCallback()
    {
        animator.SetTrigger("Pop");
    }
}
