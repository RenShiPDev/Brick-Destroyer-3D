using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotater : MonoBehaviour
{
    [SerializeField] private GameObject _cannon;
    [SerializeField] private GameObject _limitObject;

    private Quaternion _startRotation;

    private void Start()
    {
        _startRotation = _cannon.transform.rotation;
    }

    private void Update()
    {
        //OnTouch();
        OnMouseTouch();
    }

    private void OnMouseTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.collider.gameObject.GetComponent<SafeZone>() == null)
                {
                    if (mouseHit.point.y <= _limitObject.transform.position.y)
                    {
                        mouseHit.point = new Vector3(mouseHit.point.x, _limitObject.transform.position.y, mouseHit.point.z);
                    }
                    Vector3 lookV3 = new Vector3(mouseHit.point.x, mouseHit.point.y, 0);
                    _cannon.transform.LookAt(lookV3);
                }
                else
                {
                    _cannon.transform.rotation = _startRotation;
                }
            }
        }
    }

    private void OnTouch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[i].position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Touch touch = Input.GetTouch(i);

                    Debug.Log(hit.collider.gameObject.name);

                    /*switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            break;

                        case TouchPhase.Moved:
                            break;

                        case TouchPhase.Ended:
                            break;
                    }*/
                }
            }
        }
    }

}
