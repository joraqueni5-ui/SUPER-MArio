using UnityEngine;

// Poner en un objeto con BoxCollider2D e Is Trigger activado.
public class MarioPortalNivel : MonoBehaviour
{
    private bool used;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used || other.GetComponentInParent<Player>() == null) return;
        used = true;
        if (GameManager.Instance != null) GameManager.Instance.NextLevel();
    }
}
