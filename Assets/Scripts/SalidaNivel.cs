using UnityEngine;
using UnityEngine.SceneManagement;
public class SalidaNivel : MonoBehaviour
{
    public string siguienteEscena;
    private bool activada;
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (activada || c.GetComponent<MarioJugador>() == null) return;
        activada = true;
        AudioMario.Efecto("moneda");
        if (!string.IsNullOrEmpty(siguienteEscena)) SceneManager.LoadScene(siguienteEscena);
    }
}
