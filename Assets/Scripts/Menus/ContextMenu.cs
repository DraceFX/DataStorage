using System;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private Button _changeButton;
    [SerializeField] private Button _pinItButton;
    [SerializeField] private Button _unPinItButton;
    [SerializeField] private Button _deleteButton;

    public event Action ChangePressed;
    public event Action PinItPressed;
    public event Action UnPinItPressed;
    public event Action DeletePressed;

    private void Awake()
    {
        _changeButton.onClick.AddListener(() => ChangePressed?.Invoke());
        _pinItButton.onClick.AddListener(() => PinItPressed?.Invoke());
        _deleteButton.onClick.AddListener(() => DeletePressed?.Invoke());
        _unPinItButton.onClick.AddListener(() => UnPinItPressed?.Invoke());
    }
}
