using UnityEngine;
using System.Collections;

public class SimpleRotateSphere : MonoBehaviour
{
    private const int RMB_ID = 0;

    private Transform _cachedTransform;
    private Transform _cachedTransform2;
    private Vector2? _rmbPrevPos;
    private float _x;
    private float _y;

    private float _xself;
    private float _yself;

    [Range(1, 100)]
    [SerializeField]
    private float _rotationSpeed = 4;

    [SerializeField]
    private Camera Cam4K;

    void Awake ()
    {
        _cachedTransform = Camera.main.transform;
        _cachedTransform2 = Cam4K.transform;
    }
	
	void Update () {
        TrackRotation();
        _yself += 0.1f;
        _cachedTransform.rotation = Quaternion.Euler(0, _yself, 0);
        _cachedTransform2.rotation = Quaternion.Euler(0, _yself, 0);
    }

    private void TrackRotation()
    {
        if (_rmbPrevPos.HasValue)
        {
            if (Input.GetMouseButton(RMB_ID))
            {
                if (((int)_rmbPrevPos.Value.x != (int)Input.mousePosition.x)
                    || ((int)_rmbPrevPos.Value.y != (int)Input.mousePosition.y))
                {
                    _x += (_rmbPrevPos.Value.y - Input.mousePosition.y) * Time.deltaTime * _rotationSpeed;
                    _y -= (_rmbPrevPos.Value.x - Input.mousePosition.x) * Time.deltaTime * _rotationSpeed;
                    _cachedTransform.rotation = Quaternion.Euler(_x, _y, 0);
                    _cachedTransform2.rotation = Quaternion.Euler(_x, _y, 0);
                    _rmbPrevPos = Input.mousePosition;
                }
            }
            else
            {
                _rmbPrevPos = null;
            }
        }
        else if (Input.GetMouseButtonDown(RMB_ID))
        {
            _rmbPrevPos = Input.mousePosition;
        }
    }
}
