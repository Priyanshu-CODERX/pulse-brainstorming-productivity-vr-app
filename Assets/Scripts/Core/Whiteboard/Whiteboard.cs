using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        // Fill the texture with white color
        Color[] whitePixels = new Color[texture.width * texture.height];
        for (int i = 0; i < whitePixels.Length; i++)
        {
            whitePixels[i] = Color.white;
        }
        texture.SetPixels(whitePixels);
        texture.Apply();

        r.material.mainTexture = texture;
    }
}
