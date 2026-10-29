using UnityEngine;
public class ZonaPeligro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D c)
    {
        MarioJugador mario = c.GetComponent<MarioJugador>();
        if (mario != null) mario.Danar();
    }
}
