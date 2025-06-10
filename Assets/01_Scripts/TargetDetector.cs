using UnityEngine;

public class TargetDetector
{
    private readonly Transform origin;
    private readonly float range;
    private readonly LayerMask targetLayer;

    public TargetDetector(Transform origin, float range, LayerMask targetLayer)
    {
        this.origin = origin;
        this.range = range;
        this.targetLayer = targetLayer;
    }

    public Health DetectClosestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(origin.position, range, targetLayer);
        
        float closetDistanceSqr = float.MaxValue;
        Health closest = null;

        foreach (Collider hit in hits)
        {
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