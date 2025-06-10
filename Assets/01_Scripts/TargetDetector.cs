using UnityEngine;

public class TargetDetector
{
    private readonly Transform origin;
    private readonly float range;
    private readonly LayerMask targetLayer;
    private Collider[] hits = new Collider [50];
    private int count = 0;

    public TargetDetector(Transform origin, float range, LayerMask targetLayer)
    {
        this.origin = origin;
        this.range = range;
        this.targetLayer = targetLayer;
    }

    public bool HasAnyTarget()
    {
        count = 0;
        count = Physics.OverlapSphereNonAlloc(origin.position, range, hits, targetLayer);
        if (count == hits.Length)
        {
            //Debug.LogWarning("[TargetDetector] 감지 배열이 가득 찼습니다! 범위가 너무 넓거나 몬스터가 너무 많을 수 있습니다.");
        }
        
        return count > 0;
    }

    public Health DetectClosestTarget()
    {
        if(!HasAnyTarget()) return null;
        
        float closetDistanceSqr = float.MaxValue;
        Health closest = null;

        for (int i = 0; i < hits.Length; i++)
        {
            Collider hit = hits[i];
            
            if(hit == null) continue;
            Health health = hit.GetComponent<Health>();
            if(health == null || health.IsDie) continue;
            
            float distanceSqr = (hit.transform.position - origin.position).sqrMagnitude;

            if (distanceSqr < closetDistanceSqr)
            {
                closetDistanceSqr = distanceSqr;
                closest = health;
            }
        }
        
        return closest;
    }
}