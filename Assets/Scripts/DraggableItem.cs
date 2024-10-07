using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool IsDragging = true;
    public Color SelectedColor;
    public Color DeselectedColor;
    public StringData Data;

    private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;
    private Vector2 _offset;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _parentRectTransform = (RectTransform)transform.parent;

        _rectTransform.anchoredPosition = new Vector2(Data.PosX, Data.PosY);
        _rectTransform.sizeDelta = new Vector2(Data.Width, Data.Height);
        IsDragging = Data.IsDragging;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsDragging == false) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out _offset);
        gameObject.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsDragging == false) return;

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            Vector2 newPosition = localPointerPosition - _offset;

            Vector3 clampedPosition = ClampPositionToParent(newPosition);
            _rectTransform.localPosition = clampedPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsDragging == false) return;

        Data.PosX = _rectTransform.anchoredPosition.x;
        Data.PosY = _rectTransform.anchoredPosition.y;
        Data.Width = _rectTransform.sizeDelta.x;
        Data.Height = _rectTransform.sizeDelta.y;
    }

    private Vector3 ClampPositionToParent(Vector2 newPosition)
    {
        Vector2 minPosition = _parentRectTransform.rect.min - _rectTransform.rect.min;
        Vector2 maxPosition = _parentRectTransform.rect.max - _rectTransform.rect.max;

        float clampedX = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

        return new Vector3(clampedX, clampedY, _rectTransform.localPosition.z);
    }

    public void SetSelected(bool isSelected = false)
    {
        Image image = gameObject.GetComponent<Image>();

        if (!isSelected)
        {
            image.color = SelectedColor;
        }
        else
        {
            image.color = DeselectedColor;
        }
    }
}
