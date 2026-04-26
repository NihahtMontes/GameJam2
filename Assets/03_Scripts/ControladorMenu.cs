using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    // Arrastra aquí tu panel de pausa desde el Inspector
    public GameObject panelDePausa;

    // --- Funciones de Escenas ---
    public void IrAlJuego()
    {
        Time.timeScale = 1f; // Asegura que el tiempo corra al iniciar
        SceneManager.LoadScene("Main");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Reanuda el tiempo antes de cambiar de escena
        SceneManager.LoadScene("MainMenu");
    }

    // --- Función para el Engranaje ---
    public void AlternarPausa()
    {
        if (panelDePausa != null)
        {
            bool estadoActual = !panelDePausa.activeSelf;
            panelDePausa.SetActive(estadoActual);
            Time.timeScale = estadoActual ? 0f : 1f;
        }
    }

    // 2. Ir a la escena de Ajustes (La haremos luego)
    public void AbrirAjustes()
    {
        Debug.Log("Abriendo ajustes... (Aquí pondremos la lógica después)");
    }

    // 3. Salir al Menú Principal
    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // ¡MUY IMPORTANTE! Si no reseteas el tiempo, el menú principal estará congelado.
        SceneManager.LoadScene("MainMenu"); // Asegúrate de que el nombre sea exacto
    }
}