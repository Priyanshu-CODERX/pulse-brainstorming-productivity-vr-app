using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class EnvDrawingController : MonoBehaviour
{
    [Header("Controller Configuration")]
    [SerializeField] private InputData _mainController;

    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _initiationTransform;
    [SerializeField] private Transform _deinitializationTransform;

    [Header("3D Pen Visual Configuration")]
    [SerializeField] private MeshRenderer _sphereMeshRenderer;
    [SerializeField] private Material _sphereMeshDeleteMaterial;
    [SerializeField] private Material _sphereMeshDrawMaterial;

    [Header("Trail Renderer Configuration")]
    [SerializeField] private Material _drawingTrailsMaterial;
    [SerializeField] private float _trailStartWidth = 0.002f;
    [SerializeField] private float _trailEndWidth = 0.002f;
    [SerializeField] private float _minimumVertexDistance = 0.1f;
    [SerializeField] private int _cornerVertices = 90;
    [SerializeField] private int _endCapVertices = 90;
    [SerializeField] private UnityEngine.Rendering.ShadowCastingMode _trailShadowCastingMode;
    [SerializeField] private bool canDraw = true;
    [SerializeField] private bool canBakeMesh = false;
    [SerializeField] private bool addPhysics = false;

    private GameObject _drawingTrails;
    private bool _isLeftButtonPressed;
    private bool _isRightButtonPressed;

    private void Start()
    {
        _mainController = FindObjectOfType<InputData>();
    }

    private void Update()
    {
        DeleteDrawing();
    }

    private void ConfigureTrailRenderer(TrailRenderer _trailRenderer)
    {
        _trailRenderer.time = Mathf.Infinity;
        _trailRenderer.startWidth = _trailStartWidth;
        _trailRenderer.endWidth = _trailEndWidth;
        _trailRenderer.material = _drawingTrailsMaterial;
        _trailRenderer.minVertexDistance = _minimumVertexDistance;
        _trailRenderer.numCornerVertices = _cornerVertices;
        _trailRenderer.numCapVertices = _endCapVertices;
        _trailRenderer.shadowCastingMode = _trailShadowCastingMode;
    }

    [ContextMenu("Initialize Drawing Trails")]
    public void StartDrawing()
    {
        if (canDraw == true)
        {
            _drawingTrails = new GameObject("DrawingTrailRenderer");
            _drawingTrails.transform.parent = _initiationTransform;
            _drawingTrails.transform.position = _initiationTransform.position;
            _drawingTrails.AddComponent<TrailRenderer>();

            ConfigureTrailRenderer(_drawingTrails.GetComponent<TrailRenderer>());
        }
    }

    [ContextMenu("End Drawing Trails")]
    public void EndDrawing()
    {
        if (canBakeMesh == true)
        {
            TrailRenderer trailRenderer = _drawingTrails.GetComponent<TrailRenderer>();
            if (trailRenderer != null)
            {
                // Create MeshFilter and MeshRenderer components
                MeshFilter mFilter = _drawingTrails.AddComponent<MeshFilter>();
                MeshRenderer mRenderer = _drawingTrails.AddComponent<MeshRenderer>();

                // Bake the trail mesh
                Mesh trailMesh = new Mesh();
                trailRenderer.BakeMesh(trailMesh, _camera, true);
                mFilter.mesh = trailMesh;
                mRenderer.material = trailRenderer.material;

                // Calculate the center of the mesh
                Vector3 meshCenter = trailMesh.bounds.center;
                _drawingTrails.transform.position = _drawingTrails.transform.position - meshCenter;
            }
            trailRenderer.enabled = false;
        }
        
        if(addPhysics == true && canDraw == true)
        {
            BoxCollider _trailCollider = _drawingTrails?.AddComponent<BoxCollider>();
            Rigidbody _trailRigidbody = _drawingTrails?.AddComponent<Rigidbody>();
            //XRGrabInteractable _trailGrabInteractable = _drawingTrails.AddComponent<XRGrabInteractable>();
            //_trailGrabInteractable.useDynamicAttach = true;

            if(_trailRigidbody != null)
            {
                _trailRigidbody.isKinematic = true;
                _trailRigidbody.useGravity = false;
            }

            if(_trailCollider != null)
            {
                _trailCollider.isTrigger = true;
            }
        }

        if(canDraw == true)
        {
            if(_drawingTrails != null)
            {
                _drawingTrails.transform.parent = _deinitializationTransform;
                _drawingTrails = null;
            }
        }
    }

    public void DeleteDrawing()
    {
        if ((_mainController._leftController.TryGetFeatureValue(CommonUsages.primaryButton, out _isLeftButtonPressed) && _isLeftButtonPressed == true) || (_mainController._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out _isRightButtonPressed) && _isRightButtonPressed == true))
        {
            _sphereMeshRenderer.GetComponent<MeshRenderer>().material = _sphereMeshDeleteMaterial;
            canDraw = false;
        }
        else
        {
            _sphereMeshRenderer.GetComponent<MeshRenderer>().material = _sphereMeshDrawMaterial;
            canDraw = true;
        }
    }
}
