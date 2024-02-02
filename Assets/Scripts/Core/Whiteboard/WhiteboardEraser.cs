using UnityEngine;

public class WhiteboardEraser : MonoBehaviour
{
    [SerializeField] private Whiteboard _whiteboard;
    [SerializeField] private int _eraserSize = 40;

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
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Whiteboard"))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, collision.contacts[0].point - transform.position);

            if (collision.collider.Raycast(ray, out hit, Mathf.Infinity))
            {
                ClearWhiteboardAt(hit.textureCoord);
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
