using UnityEngine;

public class WhiteboardEraser : MonoBehaviour
{
    [SerializeField] private Whiteboard _whiteboard;
    [SerializeField] private int _eraserSize = 40;

    [SerializeField] private Transform _eraserTip = null;
    [SerializeField] private float _eraserHeight = 0;

    private void Awake()
    {
        if (_whiteboard == null)
            _whiteboard = FindObjectOfType<Whiteboard>().GetComponent<Whiteboard>();
    }
    private void Start()
    {
        if (_eraserHeight == 0)
            _eraserHeight = _eraserTip.localScale.y;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Whiteboard"))
                {
                    ClearWhiteboardAt(hit.textureCoord);
                }
            }
        }

        HandleEraserRaycastTouch();
    }

    private void HandleEraserRaycastTouch()
    {
        RaycastHit _touch;
        if (Physics.Raycast(_eraserTip.position, transform.up, out _touch, _eraserHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                ClearWhiteboardAt(_touch.textureCoord);
            }
        }
    }

    private void ClearWhiteboardAt(Vector2 textureCoord)
    {
        if (_whiteboard == null)
        {
            Debug.LogError("Whiteboard reference not set!");
            return;
        }

        var x = (int)(textureCoord.x * _whiteboard.textureSize.x);
        var y = (int)(textureCoord.y * _whiteboard.textureSize.y);

        if (x < 0 || x >= _whiteboard.textureSize.x || y < 0 || y >= _whiteboard.textureSize.y)
            return;

        Color white = Color.white;

        for (int i = x - _eraserSize / 2; i < x + _eraserSize / 2; i++)
        {
            for (int j = y - _eraserSize / 2; j < y + _eraserSize / 2; j++)
            {
                if (i >= 0 && i < _whiteboard.textureSize.x && j >= 0 && j < _whiteboard.textureSize.y)
                {
                    _whiteboard.texture.SetPixel(i, j, white);
                }
            }
        }

        _whiteboard.texture.Apply();
    }
}
