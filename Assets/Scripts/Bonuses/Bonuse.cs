using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bonuse : MonoBehaviour
{
    public UnityEvent BonuseHandler;

    public void ActivateBonuse()
    {
        BonuseHandler?.Invoke();
    }
}
