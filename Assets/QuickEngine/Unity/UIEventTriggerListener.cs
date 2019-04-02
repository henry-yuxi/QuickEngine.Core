using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventTriggerListener : EventTrigger
{
    public delegate void UIVoidEventProxy(GameObject go);

    public delegate void UIBoolEventProxy(GameObject go, bool value);

    public delegate void UIPointerEventProxy(GameObject go, PointerEventData eventData);

    public delegate void UIAxisEventProxy(GameObject go, AxisEventData eventData);

    public delegate void UIBaseEventProxy(GameObject go, BaseEventData eventData);

    public UIVoidEventProxy onClick;
    public UIVoidEventProxy onDown;
    public UIVoidEventProxy onEnter;
    public UIVoidEventProxy onExit;
    public UIVoidEventProxy onUp;
    public UIBoolEventProxy onHover;

    public UIBaseEventProxy onSelect;
    public UIBaseEventProxy onUpdateSelect;
    public UIBaseEventProxy onDeselect;

    public UIPointerEventProxy onBeginDrag;
    public UIPointerEventProxy onDrag;
    public UIPointerEventProxy onDrop;
    public UIPointerEventProxy onEndDrag;
    public UIPointerEventProxy onInitializePotentialDrag;
    public UIAxisEventProxy onMove;

    public UIPointerEventProxy onScroll;
    public UIBaseEventProxy onSubmit;
    public UIBaseEventProxy onCancel;

    static public UIEventTriggerListener Get(GameObject go)
    {
        if (go == null) { return null; }
        PhysicsRaycaster raycaster = Camera.main.gameObject.GetComponent<PhysicsRaycaster>();
        if (raycaster == null) { raycaster = Camera.main.gameObject.AddComponent<PhysicsRaycaster>(); }
        UIEventTriggerListener listener = go.GetComponent<UIEventTriggerListener>();
        if (listener == null) { listener = go.AddComponent<UIEventTriggerListener>(); }
        return listener;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) { onBeginDrag.Invoke(gameObject, eventData); }
    }

    public override void OnCancel(BaseEventData eventData)
    {
        if (onCancel != null) { onCancel.Invoke(gameObject, eventData); }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if (onDeselect != null) { onDeselect.Invoke(gameObject, eventData); }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) { onDrag.Invoke(gameObject, eventData); }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null) { onDrop.Invoke(gameObject, eventData); }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) { onEndDrag.Invoke(gameObject, eventData); }
    }

    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (onInitializePotentialDrag != null) { onInitializePotentialDrag.Invoke(gameObject, eventData); }
    }

    public override void OnMove(AxisEventData eventData)
    {
        if (onMove != null) { onMove.Invoke(gameObject, eventData); }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) { onClick.Invoke(gameObject); }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) { onDown.Invoke(gameObject); }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) { onEnter.Invoke(gameObject); }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) { onExit.Invoke(gameObject); }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) { onUp.Invoke(gameObject); }
    }

    public override void OnScroll(PointerEventData eventData)
    {
        if (onScroll != null) { onScroll.Invoke(gameObject, eventData); }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) { onSelect.Invoke(gameObject, eventData); }
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (onSubmit != null) { onSubmit.Invoke(gameObject, eventData); }
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) { onUpdateSelect.Invoke(gameObject, eventData); }
    }
}