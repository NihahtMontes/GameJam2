using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic; 

public class ControladorMenu : MonoBehaviour
{
    [Header("Paneles Principales")]
    public GameObject panelDePausa;
    public GameObject panelDeAjustes; 

    [Header("Componentes de Ajustes")]
    public TMPro.TMP_Dropdown dropdownResoluciones;
    public TMPro.TMP_Dropdown dropdownCalidad;
    public Toggle togglePantallaCompleta;
    public Slider sliderAudio;

    [Header("Configuración de Audio")]
    public AudioSource altavozSFX;
    public AudioClip sonidoClic;

    Resolution[] resolucionesDisponibles;

    void Start()
    {
        if (panelDePausa != null) panelDePausa.SetActive(false);
        if (panelDeAjustes != null) panelDeAjustes.SetActive(false);

        SetupDropdownResoluciones();

        if (dropdownCalidad != null)
        {
            dropdownCalidad.value = QualitySettings.GetQualityLevel();
            dropdownCalidad.RefreshShownValue();
        }

        if (togglePantallaCompleta != null)
        {
            togglePantallaCompleta.isOn = Screen.fullScreen;
        }

        if (sliderAudio != null)
        {
            sliderAudio.value = AudioListener.volume;
        }
    }


    public void AlternarPausa()
    {
        if (panelDePausa != null)
        {
            if (panelDeAjustes.activeSelf)
            {
                AbrirPuntoPausaDesdeAjustes();
            }
            else
            {
                bool estadoActualPausa = !panelDePausa.activeSelf;
                panelDePausa.SetActive(estadoActualPausa);
                Time.timeScale = estadoActualPausa ? 0f : 1f;
            }
        }
    }

    public void AbrirPuntoPausaDesdeAjustes()
    {
        if (panelDeAjustes != null) panelDeAjustes.SetActive(false);
        if (panelDePausa != null) panelDePausa.SetActive(true);
    }

    public void AbrirPanelAjustes()
    {
        if (panelDePausa != null) panelDePausa.SetActive(false);
        if (panelDeAjustes != null) panelDeAjustes.SetActive(true);
    }

    // --- Funciones de Lógica de Ajustes (Las de tu imagen) ---

    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
        Debug.Log("Volumen cambiado a: " + volumen);
    }

    public void ReproducirSonidoBoton()
    {
        if (altavozSFX != null && sonidoClic != null)
        {
            altavozSFX.PlayOneShot(sonidoClic);
        }
    }

    // 2. Calidad (Alto, Medio, Bajo)
    public void CambiarCalidad(int indexCalidad)
    {
        QualitySettings.SetQualityLevel(indexCalidad);
        Debug.Log("Calidad cambiada a index: " + indexCalidad);
    }

    // 3. Pantalla Completa
    public void CambiarPantallaCompleta(bool esPantallaCompleta)
    {
        Screen.fullScreen = esPantallaCompleta;
        Debug.Log("Pantalla completa: " + esPantallaCompleta);
    }

    // 4. Resolución
    public void CambiarResolucion(int indexResolucion)
    {
        Resolution resolucion = resolucionesDisponibles[indexResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
        Debug.Log("Resolución cambiada a: " + resolucion.width + "x" + resolucion.height);
    }


    void SetupDropdownResoluciones()
    {
        if (dropdownResoluciones == null) return;

        resolucionesDisponibles = Screen.resolutions;
        dropdownResoluciones.ClearOptions();
        List<string> opciones = new List<string>();
        int indexActualResolucion = 0;

        for (int i = 0; i < resolucionesDisponibles.Length; i++)
        {
            string opcion = resolucionesDisponibles[i].width + " x " + resolucionesDisponibles[i].height;
            opciones.Add(opcion);

            // Marcamos la resolución actual como seleccionada
            if (resolucionesDisponibles[i].width == Screen.currentResolution.width &&
                resolucionesDisponibles[i].height == Screen.currentResolution.height)
            {
                indexActualResolucion = i;
            }
        }

        dropdownResoluciones.AddOptions(opciones);
        dropdownResoluciones.value = indexActualResolucion;
        dropdownResoluciones.RefreshShownValue();
    }

    // --- Funciones de Escenas (Las que ya tenías) ---
    public void IrAlJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}