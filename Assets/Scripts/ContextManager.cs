using UnityEngine;

public class ContextManager : MonoBehaviour
{
    [SerializeField] private GameObject _contexMenuUi;
    [SerializeField] private ContextMenu _contextMenu;
    [SerializeField] private ShowContextMenu _showContextMenu;
    [SerializeField] private DataMenu _dataMenu;

    private DraggableItem _storageObject;

    private void Awake()
    {
        _contextMenu.ChangePressed += ChangeData;
        _contextMenu.PinItPressed += PinItObject;
        _contextMenu.UnPinItPressed += UnPinItObject;
        _contextMenu.DeletePressed += DeleteObject;

        _showContextMenu.DraggableObject += SetDraggableItem;
    }

    private void ChangeData()
    {
        _contexMenuUi.gameObject.SetActive(false);
        _dataMenu.gameObject.SetActive(true);
    }

    private void PinItObject()
    {
        _contexMenuUi.gameObject.SetActive(false);
        _storageObject.IsDragging = false;
        _storageObject.Data.IsDragging = false;
        _storageObject = null;
    }

    private void UnPinItObject()
    {
        _contexMenuUi.gameObject.SetActive(false);
        _storageObject.IsDragging = true;
        _storageObject.Data.IsDragging = true;
        _storageObject = null;
    }

    private void DeleteObject()
    {
        _contexMenuUi.gameObject.SetActive(false);
        Destroy(_storageObject.gameObject);
        _storageObject = null;
    }

    private void SetDraggableItem(DraggableItem draggableItem)
    {
        _storageObject = draggableItem;
        _dataMenu.ChagneData(draggableItem);
    }
}
