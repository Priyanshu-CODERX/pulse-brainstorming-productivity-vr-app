using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color[] _colors;

    public void UpdateColor(int index)
    {
        if (index >= 0 && index < _colors.Length)
        {
            _renderer.material.color = _colors[index];

            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_EmissionColor", _colors[index]);

            _renderer.SetPropertyBlock(propertyBlock);
        }
        else
        {
            Debug.LogWarning("Index out of range");
        }
    }
}
