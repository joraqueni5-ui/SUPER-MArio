using UnityEngine;

// Lava, pez o trampa: objeto con BoxCollider2D y Is Trigger.
public class MarioZonaPeligro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null && !player.dead) player.Hit();
    }
}
