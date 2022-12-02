using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUIController : MonoBehaviour
{
    private UIDocument _doc;
    private VisualElement _root;

    private void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _root = _doc.rootVisualElement;
        _root.RegisterCallback<ClickEvent>((evt => gameObject.SetActive(false)));
    }

    private void OnDisable()
    {
        _root.UnregisterCallback<ClickEvent>((evt => gameObject.SetActive(false)));
    }
}
