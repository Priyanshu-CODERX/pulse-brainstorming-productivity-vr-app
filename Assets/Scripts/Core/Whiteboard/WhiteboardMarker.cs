using System;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private float _tipHeight = 2f;
    [SerializeField] private Renderer _markerColorRenderer;
    [SerializeField] private string _ignoreTag = "";

    //private Renderer _renderer;
    private Color[] _colors;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    private Color[] _previousColors;

    void Start()
    {
        //_markerColorRenderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_markerColorRenderer.material.color, _penSize * _penSize).ToArray();
        _previousColors = _colors;
        //_tipHeight = _tip.localScale.y;
    }

    void Update()
    {
        Draw();
        UpdateMarkerInk();
    }

    private void UpdateMarkerInk()
    {
        if (_markerColorRenderer != null && _previousColors[0] != _markerColorRenderer.material.color)
        {
            //_markerColorRenderer = _tip.GetComponent<Renderer>();
            _colors = Enumerable.Repeat(_markerColorRenderer.material.color, _penSize * _penSize).ToArray();
            Array.Copy(_colors, _previousColors, _colors.Length);
        }
    }


    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            Collider[] allColliders = GameObject.FindObjectsOfType<Collider>();

            foreach (Collider collider in allColliders)
            {
                if (collider.CompareTag(_ignoreTag))
                {
                    // Ignore collisions with Whiteboard
                    continue;
                }

                Physics.IgnoreCollision(GetComponent<Collider>(), collider, true);
            }

            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }

                    transform.rotation = _lastTouchRot;

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
