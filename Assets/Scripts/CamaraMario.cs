using UnityEngine;
public class CamaraMario : MonoBehaviour
{
    public Transform jugador;
    public float minimoX = 7f;
    public float maximoX = 85f;
    private void LateUpdate()
    {
        if (jugador == null) return;
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(jugador.position.x + 5f, minimoX, maximoX);
        transform.position = Vector3.Lerp(transform.position, p, Time.deltaTime * 8f);
    }
}
