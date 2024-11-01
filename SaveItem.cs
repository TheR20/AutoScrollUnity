using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class SaveItem : MonoBehaviour, ISelectHandler, IPointerClickHandler, ISubmitHandler
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private ItemButtonEvent _onSelectEvent;
    [SerializeField] private ItemButtonEvent _onSumbitEvent;
    [SerializeField] private ItemButtonEvent _onClickEvent;

    public ItemButtonEvent OnSlectEvent
    {
        get => _onSelectEvent;
        set => _onSelectEvent = value;
    }
     public ItemButtonEvent OnSubmitEvent
    {
        get => _onSumbitEvent;
        set => _onSumbitEvent = value;
    }

    public ItemButtonEvent OnClickEvent
    {
        get => _onClickEvent;
        set => _onClickEvent = value;
    }

    public string ItemNameValue
    {
        get => _itemName.text;
        set => _itemName.text = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       _onClickEvent.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _onSelectEvent.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        _onSumbitEvent.Invoke(this);
    }

    public void ObtainSelectionFocus()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        _onSelectEvent.Invoke(this);
    }

}

[System.Serializable]
public class ItemButtonEvent : UnityEvent<SaveItem>{}