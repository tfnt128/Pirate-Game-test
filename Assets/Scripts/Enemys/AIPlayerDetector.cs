using System.Collections;
using UnityEngine;

public class AIPlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }

    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;
    
    [Header("OverlapBox parameters")]
    
    [SerializeField] private Transform detectorOrigin;

    [SerializeField] private float detectorSize = 1;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;

    [SerializeField] private float detectionDelay = .3f;

    [SerializeField] private LayerMask playerLayer;

    [Header("Gizmo parameters")]
    [SerializeField] private Color gizmoIdleColor = Color.green;
    [SerializeField] private Color gizmoDetectedColor = Color.red;
    [SerializeField] private bool showGizmos;
    
    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void Start()
    {
        detectorOrigin = gameObject.transform;
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
        Collider2D collider = Physics2D.OverlapCircle(
            (Vector2)detectorOrigin.position + detectorOriginOffset,
            detectorSize / 2f,
            playerLayer);

        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if(PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawWireSphere((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize / 2f);
        }
    }
}
