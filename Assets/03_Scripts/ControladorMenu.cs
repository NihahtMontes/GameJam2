using UnityEngine;
using UnityEngine.SceneManagement;
// IMPORTANTE: Necesario para controlar los elementos de la interfaz (Dropdowns, Sliders, Toggle)
using UnityEngine.UI;
using System.Collections.Generic; // Para las listas de resoluciones

public class ControladorMenu : MonoBehaviour
{
    [Header("Paneles Principales")]
    public GameObject panelDePausa;
    public GameObject panelDeAjustes; // ARRASTRA AQUÍ EL NUEVO PANEL

    [Header("Componentes de Ajustes")]
    public TMPro.TMP_Dropdown dropdownResoluciones;
    public TMPro.TMP_Dropdown dropdownCalidad;
    public Toggle togglePantallaCompleta;
    public Slider sliderAudio;

    // Almacena las resoluciones disponibles
    Resolution[] resolucionesDisponibles;

    void Start()
    {
        // Al iniciar, ocultamos ambos paneles
        if (panelDePausa != null) panelDePausa.SetActive(false);
        if (panelDeAjustes != null) panelDeAjustes.SetActive(false);

        // --- Configuración Inicial de Ajustes ---
        // 1. Cargar Resoluciones del sistema
        SetupDropdownResoluciones();

        // 2. Cargar Calidad Actual
        if (dropdownCalidad != null)
        {
            dropdownCalidad.value = QualitySettings.GetQualityLevel();
            dropdownCalidad.RefreshShownValue();
        }

        // 3. Cargar Estado de Pantalla Completa
        if (togglePantallaCompleta != null)
        {
            togglePantallaCompleta.isOn = Screen.fullScreen;
        }

        // 4. Cargar Volumen (Podemos usar PlayerPrefs más adelante)
        if (sliderAudio != null)
        {
            sliderAudio.value = AudioListener.volume;
        }
    }

    // --- Lógica de Pausa y Ajustes ---

    public void AlternarPausa()
    {
        if (panelDePausa != null)
        {
            // Si el panel de ajustes está abierto, lo cerramos primero
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
        // Cierra ajustes y vuelve a abrir pausa
        if (panelDeAjustes != null) panelDeAjustes.SetActive(false);
        if (panelDePausa != null) panelDePausa.SetActive(true);
    }

    public void AbrirPanelAjustes()
    {
        // Cierra pausa y abre ajustes
        if (panelDePausa != null) panelDePausa.SetActive(false);
        if (panelDeAjustes != null) panelDeAjustes.SetActive(true);
    }

    // --- Funciones de Lógica de Ajustes (Las de tu imagen) ---

    // 1. Audio
    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
        Debug.Log("Volumen cambiado a: " + volumen);
        // Aquí podrías guardar el volumen en PlayerPrefs
    }

    // 2. Calidad (Alto, Medio, Bajo)
    public void CambiarCalidad(int indexCalidad)
    {
        QualitySettings.SetQualityLevel(indexCalidad);
        Debug.Log("Calidad cambiada a index: " + indexCalidad);
        // En los gráficos del Quality Settings de Unity debes tener los mismos indices.
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

    // Función interna para setup de dropdown de resoluciones
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