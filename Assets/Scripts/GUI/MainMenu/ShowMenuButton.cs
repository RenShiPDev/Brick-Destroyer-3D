using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenuButton : MonoBehaviour
{
    [SerializeField] private ObjectHidder _objectHidder;

    public void OnClick()
    {
        _objectHidder.ChangeVisibility();
    }
}
