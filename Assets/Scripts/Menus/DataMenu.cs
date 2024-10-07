using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMenu : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _cancelButton;

    public event Action OnSavePressed;
    public event Action OnCancelPressed;
    public event Action<DraggableItem> OnChangeStorage;

    private void Awake()
    {
        _saveButton.onClick.AddListener(() => OnSavePressed?.Invoke());
        _cancelButton.onClick.AddListener(() => OnCancelPressed?.Invoke());
    }

    public void ChagneData(DraggableItem storageObject)
    {
        OnChangeStorage?.Invoke(storageObject);
    }
}
