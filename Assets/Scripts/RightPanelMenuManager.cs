using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightPanelMenuManager : MonoBehaviour
{
    [SerializeField] private RightPanelMenu _rightPanel;
    [SerializeField] private GameObject _object;
    [SerializeField] private Transform _parentForObject;

    [SerializeField] private TMP_Text _widthCountText;
    [SerializeField] private TMP_Text _heightCountText;
    [SerializeField] private TMP_Text _sizeCountText;

    private List<GameObject> _objectsToSave = new List<GameObject>();
    private SaveLoadProgram _saveLoadData = new SaveLoadProgram();

    private float _widthSliderValue = 2f;
    private float _heightSliderValue = 2f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (_saveLoadData.LoadProgramsData() != null)
        {
            foreach (var item in _saveLoadData.LoadProgramsData())
            {
                DraggableItem draggableItem = _object.GetComponent<DraggableItem>();
                draggableItem.Data = item;
                GameObject gm = Instantiate(_object, _parentForObject);
            }
        }

        _rightPanel.OnExitClicked += ExitProgram;
        _rightPanel.OnCreateObjectClicked += CreateObject;
        _rightPanel.OnChangeString += SearchStringInObject;

        _rightPanel.OnChangeWidthValue += SliderWidthValue;
        _rightPanel.OnChangeHeightValue += SliderHeightValue;
        _rightPanel.OnChangeSizeValue += SliderSizeValue;
    }

    private void CreateObject()
    {
        DraggableItem draggableItem = _object.GetComponent<DraggableItem>();

        draggableItem.Data.Data = "Кто здесь?";
        draggableItem.Data.PosX = 0;
        draggableItem.Data.PosY = 0;
        draggableItem.Data.Width = _widthSliderValue * 10;
        draggableItem.Data.Height = _heightSliderValue * 10;
        draggableItem.Data.IsDragging = true;

        Instantiate(_object, _parentForObject);
    }

    private void ExitProgram()
    {
        for (int i = 0; i < _parentForObject.childCount; i++)
        {
            _objectsToSave.Add(_parentForObject.GetChild(i).gameObject);
        }

        _saveLoadData.SaveProgramsData(_objectsToSave);
        Application.Quit();
    }

    private void SearchStringInObject(string s)
    {
        if (String.IsNullOrEmpty(s))
        {
            for (int i = 0; i < _parentForObject.childCount; i++)
            {
                GameObject childObject = _parentForObject.GetChild(i).gameObject;
                DraggableItem draggableItem = childObject.GetComponent<DraggableItem>();
                if (draggableItem != null)
                {
                    draggableItem.SetSelected(false);
                }
            }
            return;
        }

        for (int i = 0; i < _parentForObject.childCount; i++)
        {
            GameObject childObject = _parentForObject.GetChild(i).gameObject;
            DraggableItem draggableItem = childObject.GetComponent<DraggableItem>();
            if (draggableItem != null && draggableItem.Data != null)
            {
                if (draggableItem.Data.Data.ToLower().Contains(s.ToLower()))
                {
                    draggableItem.SetSelected(true);
                }
                else
                {
                    draggableItem.SetSelected(false);
                }
            }
        }
    }

    private void SliderWidthValue(float width)
    {
        _widthSliderValue = width;
        _widthCountText.text = width.ToString();
    }

    private void SliderHeightValue(float height)
    {
        _heightSliderValue = height;
        _heightCountText.text = height.ToString();
    }

    private void SliderSizeValue(float size)
    {
        _parentForObject.transform.localScale = new Vector3(size, size, size);
    }
}
