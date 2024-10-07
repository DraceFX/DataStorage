using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RightPanelMenu : MonoBehaviour
{
    [SerializeField] private Button _createObjectButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_InputField _search;
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private Slider _heightSlider;
    [SerializeField] private Slider _sizeSlider;

    public event Action OnCreateObjectClicked;
    public event Action OnExitClicked;
    public event Action<string> OnChangeString;
    public event Action<float> OnChangeWidthValue;
    public event Action<float> OnChangeHeightValue;
    public event Action<float> OnChangeSizeValue;

    private void Awake()
    {
        _createObjectButton.onClick.AddListener(() => OnCreateObjectClicked?.Invoke());
        _exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
        _search.onValueChanged.AddListener((o) => OnChangeString?.Invoke(o));
        _widthSlider.onValueChanged.AddListener((o) => OnChangeWidthValue?.Invoke(o));
        _heightSlider.onValueChanged.AddListener((o) => OnChangeHeightValue?.Invoke(o));
        _sizeSlider.onValueChanged.AddListener((o) => OnChangeSizeValue?.Invoke(o));
    }
}
