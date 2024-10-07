using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateMenu : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    public event Action OnCreatePressed;

    private void Awake()
    {
        _createButton.onClick.AddListener(() => OnCreatePressed?.Invoke());
    }
}
