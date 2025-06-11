using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public Enemy enemy;

    public void OnDeathAnimationEnd()
    {
        if (enemy != null)
        {
            enemy.OnDeathAnimationEnd();
        }
    }
}
