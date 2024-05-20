using UnityEngine;

[RequireComponent(typeof(Fliper))]
public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _ignorRaycastLayer;

    private Fliper _fliper;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookAreaOrigin(), _seeAreaSize);
    }

    public bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookAreaOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _seeAreaSize.x, ~_ignorRaycastLayer);

            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }

        return false;
    }

    private Vector2 GetLookAreaOrigin()
    {
        float halfCoefficient = 2;
        int directionCoefficient = _fliper?.IsTurnRight ?? true ? 1 : -1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoefficient * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }
}
