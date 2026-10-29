using UnityEngine;
using UnityEngine.SceneManagement;
public class PantallaFinal : MonoBehaviour
{
    public bool victoria;
    private GUIStyle titulo;
    private GUIStyle texto;
    private void OnGUI()
    {
        GUI.color = new Color(0.02f,0.025f,0.055f,1f);
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;
        if (titulo == null)
        {
            titulo = new GUIStyle(GUI.skin.label);
            titulo.alignment = TextAnchor.MiddleCenter;
            titulo.fontSize = Mathf.Clamp(Screen.width / 14,30,72);
            titulo.fontStyle = FontStyle.Bold;
            titulo.normal.textColor = victoria ? Color.yellow : new Color(1f,0.35f,0.29f);
            texto = new GUIStyle(titulo);
            texto.fontSize = Mathf.Clamp(Screen.width / 34,16,30);
            texto.normal.textColor = Color.white;
        }
        GUI.Label(new Rect(0,Screen.height*.22f,Screen.width,90), victoria ? "¡VICTORIA!" : "GAME OVER",titulo);
        GUI.Label(new Rect(0,Screen.height*.40f,Screen.width,75), victoria ? "¡Has derrotado a Bowser!" : "Mario se quedo sin vidas",texto);
        float w = Mathf.Min(330,Screen.width*.65f);
        if (GUI.Button(new Rect((Screen.width-w)/2,Screen.height*.64f,w,54),"REINICIAR JUEGO"))
        {
            if (GameManager.Instance != null) GameManager.Instance.NewGame();
            else SceneManager.LoadScene("1-1");
        }
        if (GUI.Button(new Rect((Screen.width-w)/2,Screen.height*.77f,w,45),"SALIR"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
