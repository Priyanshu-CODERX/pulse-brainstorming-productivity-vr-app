using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvDrawingController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _drawingTrails;
    [SerializeField] private Material _drawingTrailsMaterial;

    [SerializeField] private Transform _initiationTransform;
    [SerializeField] private Transform _deinitializationTransform;

    [Header("Trail Renderer Configuration")]
    public float trailStartWidth = 0.002f;   
    public float trailEndWidth = 0.002f;
    public float minimumVertexDistance = 0;
    public int cornerVertices = 0;
    public int endCapVertices = 0;
    public UnityEngine.Rendering.ShadowCastingMode trailShadowCastingMode;

    private void ConfigureTrailRenderer(TrailRenderer _trailRenderer)
    {
        _trailRenderer.time = Mathf.Infinity;
        _trailRenderer.startWidth = trailStartWidth;
        _trailRenderer.endWidth = trailEndWidth;
        _trailRenderer.material = _drawingTrailsMaterial;
        _trailRenderer.minVertexDistance = minimumVertexDistance;
        _trailRenderer.numCornerVertices = cornerVertices;
        _trailRenderer.numCapVertices = endCapVertices;
        _trailRenderer.shadowCastingMode = trailShadowCastingMode;
    }

    [ContextMenu("Initialize Drawing Trails")]
    public void StartDrawing()
    {
        _drawingTrails = new GameObject("DrawingTrailRenderer");
        _drawingTrails.transform.parent = _initiationTransform;
        _drawingTrails.transform.position = _initiationTransform.position;
        _drawingTrails.AddComponent<TrailRenderer>();

        ConfigureTrailRenderer(_drawingTrails.GetComponent<TrailRenderer>());
    }

    [ContextMenu("End Drawing Trails")]
    public void EndDrawing()
    {
        TrailRenderer trailRenderer = _drawingTrails.GetComponent<TrailRenderer>();
        if(trailRenderer != null)
        {
            MeshFilter mFilter = _drawingTrails.AddComponent<MeshFilter>();
            MeshRenderer mRenderer = _drawingTrails.AddComponent<MeshRenderer>();

            Mesh trailMesh = new Mesh();
            trailRenderer.BakeMesh(trailMesh, _camera, true);
            mFilter.mesh = trailMesh;

            mRenderer.material = trailRenderer.material;
        }

        _drawingTrails.transform.parent = _deinitializationTransform;
        _drawingTrails = null;
    }

}
