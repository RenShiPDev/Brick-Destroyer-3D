using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _cannon;
    [SerializeField] private GameObject _lineSphere;
    [SerializeField] private GameObject _poolObject;
    [SerializeField] private GameObject _limitObject;

    [SerializeField] private int _lineSpheresCount;
    [SerializeField] private float _maxTrackLength;
    [SerializeField] private List<string> _ignoredRaycastLayers = new List<string>();

    private List<GameObject> _lineSpheres = new List<GameObject>();

    private float _spheresDistance;
    private float _spheresStartDistance;
    private int _layerMask;

    private void Start()
    {
        _spheresDistance = _maxTrackLength / _lineSpheresCount;
        _spheresStartDistance = 1f;

        foreach(var layerName in _ignoredRaycastLayers)
        {
            int layerIndex = LayerMask.NameToLayer(layerName);
            if (layerIndex != -1)
            {
                _layerMask |= (1 << layerIndex);
            }
        }
        _layerMask = ~_layerMask;

        for (int i = 0; i < _lineSpheresCount; i++)
        {
            var clone = Instantiate(_lineSphere, _poolObject.transform);
            clone.SetActive(false);
            _lineSpheres.Add(clone);
        }
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

            if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity, _layerMask))
            {
                if(mouseHit.collider.gameObject.GetComponent<SafeZone>() == null)
                {
                    if (mouseHit.point.y <= _limitObject.transform.position.y)
                    {
                        mouseHit.point = new Vector3(mouseHit.point.x, _limitObject.transform.position.y, mouseHit.point.z);
                    }
                    Vector3 mouseDirectionV3 = (mouseHit.point - _cannon.transform.position).normalized;
                    mouseDirectionV3.z = 0;

                    Ray cannonRay = new Ray(_cannon.transform.position, mouseDirectionV3);
                    RaycastHit cannonHit;

                    if (Physics.Raycast(cannonRay, out cannonHit, Mathf.Infinity, _layerMask))
                    {
                        float trackLength = (cannonRay.origin - cannonHit.point).magnitude;
                        if (trackLength > _maxTrackLength)
                            trackLength = _maxTrackLength;

                        Vector3 trackDirectionV3 = (cannonHit.point - cannonRay.origin).normalized;
                        Vector3 trackTipV3 = cannonRay.origin + trackDirectionV3 * trackLength;
                        Debug.DrawLine(cannonRay.origin, trackTipV3, Color.white);

                        int sphereId = 0;

                        _spheresStartDistance += Time.deltaTime;
                        if (_spheresStartDistance > 2)
                            _spheresStartDistance = 1f;

                        for (int i = 0; i < trackLength / _spheresDistance; i++)
                        {
                            float startDistance = sphereId + _spheresStartDistance - 1f;
                            SetSphere(cannonRay.origin, trackDirectionV3, startDistance, sphereId);
                            sphereId++;
                        }

                        mouseDirectionV3.x = -mouseDirectionV3.x;
                        cannonRay = new Ray(cannonHit.point, mouseDirectionV3);

                        if (Physics.Raycast(cannonRay, out cannonHit, Mathf.Infinity, _layerMask))
                        {
                            trackDirectionV3 = (cannonHit.point - cannonRay.origin).normalized;
                            trackTipV3 = cannonRay.origin + trackDirectionV3 * (_maxTrackLength - trackLength);
                            Debug.DrawLine(cannonRay.origin, trackTipV3, Color.white);


                            int lastIndex = sphereId;
                            for (int i = lastIndex; i < (_maxTrackLength - trackLength) / _spheresDistance + lastIndex - 1; i++)
                            {
                                float startDistance = i - lastIndex + _spheresStartDistance - 1f;
                                SetSphere(cannonRay.origin, trackDirectionV3, startDistance, sphereId);
                                sphereId++;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _lineSpheres.Count; i++)
                        _lineSpheres[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < _lineSpheres.Count; i++)
                _lineSpheres[i].SetActive(false);
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

    private void OnDisable()
    {
        for (int i = 0; i < _lineSpheres.Count; i++)
            if(_lineSpheres[i])
                _lineSpheres[i].SetActive(false);
    }

    private void SetSphere(Vector3 origin, Vector3 direction, float distance, int index)
    {
        var clone = _lineSpheres[index];
        clone.SetActive(true);
        clone.transform.position = origin + direction * _spheresDistance * distance;
    }
}
