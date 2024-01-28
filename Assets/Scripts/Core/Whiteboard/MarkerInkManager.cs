using UnityEngine;

public class MarkerInkManager : MonoBehaviour
{
    [SerializeField] Color _markerInkColor;
    [SerializeField] string _markerObjectTag = "MarkerTip";

    private void OnTriggerEnter(Collider other)
    {
        if (IsMarkerTip(other))
        {
            SetMarkerInkColor(other.gameObject);
        }
    }

    private bool IsMarkerTip(Collider other)
    {
        return other.CompareTag(_markerObjectTag);
    }

    private void SetMarkerInkColor(GameObject _markerTip)
    {
        MeshRenderer _tipRenderer = _markerTip.GetComponent<MeshRenderer>();
        if (_tipRenderer != null)
        {
            _tipRenderer.material.color = _markerInkColor;
        }
    }
}
