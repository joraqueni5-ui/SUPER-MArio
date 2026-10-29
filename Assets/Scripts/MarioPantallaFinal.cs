using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioPantallaFinal : MonoBehaviour
{
    public bool victoria;
    private GUIStyle titleStyle;
    private GUIStyle subStyle;

    private void OnGUI()
    {
        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;
        if (titleStyle == null)
        {
            titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.fontSize = Mathf.Clamp(Screen.width / 14, 30, 70);
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.normal.textColor = victoria ? Color.yellow : Color.red;
            subStyle = new GUIStyle(titleStyle);
            subStyle.fontSize = Mathf.Clamp(Screen.width / 35, 16, 30);
            subStyle.normal.textColor = Color.white;
        }
        GUI.Label(new Rect(0, Screen.height * .22f, Screen.width, Screen.height * .18f), victoria ? "¡GANASTE!" : "GAME OVER", titleStyle);
        GUI.Label(new Rect(0, Screen.height * .41f, Screen.width, 75), victoria ? "¡Bowser ha sido derrotado!" : "Mario se quedo sin vidas", subStyle);
        float bw = Mathf.Min(300, Screen.width * .55f);
        if (GUI.Button(new Rect((Screen.width-bw)/2, Screen.height*.67f, bw, 56), "REINICIAR JUEGO"))
        {
            if (GameManager.Instance != null) GameManager.Instance.NewGame();
            else SceneManager.LoadScene("1-1");
        }
        if (GUI.Button(new Rect((Screen.width-bw)/2, Screen.height*.78f, bw, 46), "SALIR"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
