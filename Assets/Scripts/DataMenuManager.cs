using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataMenuManager : MonoBehaviour
{
    [SerializeField] private DataMenu _dataMenu;
    [SerializeField] private TMP_InputField _inputData;

    private DraggableItem _object;
    private StringData _dataString;

    private void Awake()
    {
        _dataMenu.OnSavePressed += SaveChanges;
        _dataMenu.OnCancelPressed += CancelChages;
        _dataMenu.OnChangeStorage += ChangeObjectData;
    }

    private void SaveChanges()
    {
        _dataMenu.gameObject.SetActive(false);
        _dataString.Data = _inputData.text;
    }

    private void CancelChages()
    {
        _dataMenu.gameObject.SetActive(false);
    }

    private void ChangeObjectData(DraggableItem objectData)
    {
        _object = objectData;
        _dataString = objectData.Data;

        _inputData.text = _dataString.Data;
    }
}
