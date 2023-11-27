using System.Collections;
using UnityEngine;

public class AITargetDetector : MonoBehaviour
{
    public bool IsPlayerDetected => _isPlayerDetected;

    [Header("OverlapBox Parameters")] 
    [SerializeField] private Transform detectionOrigin;
    [SerializeField] private float detectionSize = 1;
    [SerializeField] private Vector2 detectionOriginOffset = Vector2.zero;
    [SerializeField] private float detectionDelay = 0.3f;
    
    [Header("Target Layer")] 
    [SerializeField] private LayerMask targetLayer;

    [Header("Gizmo Parameters")] 
    [SerializeField] private Color gizmoIdleColor = Color.green;
    [SerializeField] private Color gizmoDetectedColor = Color.red;
    [SerializeField] private bool showGizmos;

    private bool _isPlayerDetected;

    private void Start()
    {
        detectionOrigin = transform;
        StartCoroutine(DetectionCoroutine());
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    private void PerformDetection()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(
            detectionOrigin.position + (Vector3)detectionOriginOffset,
            detectionSize / 2f,
            targetLayer);

        SetPlayerDetection(playerCollider != null);
    }

    private void SetPlayerDetection(bool detected)
    {
        _isPlayerDetected = detected;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectionOrigin != null)
        {
            Gizmos.color = _isPlayerDetected ? gizmoDetectedColor : gizmoIdleColor;
            Gizmos.DrawWireSphere(detectionOrigin.position + (Vector3)detectionOriginOffset, detectionSize / 2f);
        }
    }
}