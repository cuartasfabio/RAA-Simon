//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Combinacion {
    public int[] combinacion = {};

    public Combinacion(int[] lista) {
        this.combinacion = lista;
    }
}

public class SimonGame : MonoBehaviour
{
    [SerializeField] public List<BotonSimon> botones;
    [SerializeField] List<Teleport> TPs;
    [SerializeField] Image pantalla;
    //[SerializeField] Image nivelIcono;
    [SerializeField] GameObject player;
    [SerializeField] List<Sprite> iconosNiveles;

    public AudioSource audioSource;
    public AudioClip audioClipWin;
    public AudioClip audioClipLose;
    public AudioClip audioAmarillo;
    public AudioClip audioAzul;
    public AudioClip audioVerde;
    public AudioClip audioRojo;
    public AudioClip audioFallo;
    public AudioClip audioCuentaAtras;
    public AudioClip audioNivel;
    public AudioClip audioDisparo;
    public AudioClip audioFoco;
    public List<AudioClip> audioClipsNiveles;    
    public List<AudioClip> AudioColores;

    static Color32 amarillo = new Color32(255,236,0,255);   // 0
    static Color32 azul = new Color32(0,81,176,255);        // 1
    static Color32 verde = new Color32(2,164,55,255);       // 2
    static Color32 rojo = new Color32(255,0,0,255);         // 3
    static Color32 blanco = new Color32(255,255,255,255);   // 4
    static Color32 negro = new Color32(0,0,0,255);          // 5
    Color32[] colores = {amarillo,azul,verde,rojo,blanco,negro};

    static int[] nivel1 = {3};
    static int[] nivel2 = {3,2};
    static int[] nivel3 = {3,2,2};
    static int[] nivel4 = {3,2,2,0,3};
    static int[] nivel5 = {3,2,2,0,3,1,0};
    Combinacion[] niveles = 
                    {new Combinacion(nivel1),
                    new Combinacion(nivel2),
                    new Combinacion(nivel3),
                    new Combinacion(nivel4),
                    new Combinacion(nivel5)};

    int coloresMostrados = 0;
    public float velocidadCombinacion;
    public float tiempoEntreColores;
    bool colorONegro = false;
    float duracionColores;
    float duracionNegro;
    public int countdownTime;
    public TextMeshProUGUI tmpCD;
    public GameObject luzYTriggerFinal;
    int botonesPulsados = 0;
    int nivelActual = 0;
    int estado = 0; // 0 cuanta atras    1 mostrando     2 jugando
    

    void Start()
    {
        duracionColores = 0;
        duracionNegro = 0;

        AudioColores.Add(audioAmarillo);
        AudioColores.Add(audioAzul);
        AudioColores.Add(audioVerde);
        AudioColores.Add(audioRojo);
    }

    public void EmpezarNivel() {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart() {
        CambiarColorPantalla(4);
        //nivelIcono.gameObject.SetActive(false);
        tmpCD.gameObject.SetActive(true);
        while(countdownTime > 0) {
            tmpCD.text = countdownTime.ToString();
            audioSource.PlayOneShot(audioCuentaAtras, 0.7F);
            yield return new WaitForSeconds(0.7f);

            countdownTime--;
        }

        //Cambiar de estado cuenta atras a mostrando
        estado = 1;
        tmpCD.gameObject.SetActive(false);
        countdownTime = 3;
    }

    public void EmpiezaJuego() {
        StartCoroutine(EmpiezaJuegoDelay());
    }

    IEnumerator EmpiezaJuegoDelay() {
        audioSource.PlayOneShot(audioClipsNiveles[nivelActual], 0.7F);
        yield return new WaitForSeconds(1.5f);
        EmpezarNivel();
    }

    IEnumerator HasGanado() {
        yield return new WaitForSeconds(5f);
        luzYTriggerFinal.SetActive(true);
        audioSource.PlayOneShot(audioFoco, 0.7F);
    }

    
    void ReproducirSonidoColor() {
        if (niveles[nivelActual].combinacion[coloresMostrados] == 0)
        {
            audioSource.PlayOneShot(audioAmarillo, 0.7F);
        } else if (niveles[nivelActual].combinacion[coloresMostrados] == 1)
        {
            audioSource.PlayOneShot(audioAzul, 0.7F);
        } else if (niveles[nivelActual].combinacion[coloresMostrados] == 2)
        {
            audioSource.PlayOneShot(audioVerde, 0.7F);
        } else if (niveles[nivelActual].combinacion[coloresMostrados] == 3)
        {
            audioSource.PlayOneShot(audioRojo, 0.7F);
        }
        
    }
    void Update()
    {
        // Etado 0 = cuenta atras comienzo
        if(estado == 0) {
            // Nivel nivelActual comienza en... 3... 2... 1...
        }
        // Estado 1 = mostrando combinacion
        else if(estado == 1) {

            // Dependiendo del nivel actual
            if (coloresMostrados < niveles[nivelActual].combinacion.Length)
            {
                // Mostrar el color que toque y luego esperar (aumentar el indice)  
                if(colorONegro) {
                    duracionColores += Time.deltaTime;
                    while(duracionColores >= velocidadCombinacion) {
                        // Se muestra color
                        CambiarColorPantalla(niveles[nivelActual].combinacion[coloresMostrados]);
                        ReproducirSonidoColor();
                        coloresMostrados++; // Se pasa al siguiente color
                        duracionColores -= velocidadCombinacion;
                        colorONegro = false;
                    }
                } else {
                    duracionNegro += Time.deltaTime;
                    while(duracionNegro >= tiempoEntreColores) {
                        CambiarColorPantalla(5);
                        duracionNegro -= tiempoEntreColores;
                        colorONegro = true;
                    }
                }
 
            } else if (!colorONegro){
                duracionNegro += Time.deltaTime;
                while(duracionNegro >= tiempoEntreColores) {
                    CambiarColorPantalla(5);
                    duracionNegro -= tiempoEntreColores;
                    colorONegro = true;
                    // Cambio de estado = El jugador puede empezar...
                    AcabaMostrarCombinacion();
                }
            }

        }
        // Estado 2 = pulsando botones
        else if(estado == 2) {

            // Repetir hasta que se pulsen tantos botones como haya 
                // Esperar a que el jugador pulse un boton 
            // Si los acierta todos:
                // Subir el nivel
                // Empezar cuenta atras de nuevo

            if (botonesPulsados >= niveles[nivelActual].combinacion.Length)
            {
                if (nivelActual == niveles.Length -1) 
                {
                    // Se acaba el juego, el jugador gana
                    // Delay
                    // Activar foco y puerta final
                    audioSource.PlayOneShot(audioClipWin, 0.7F);
                    StartCoroutine(HasGanado());
                    estado = 3;
                    DesactivarBotones();
                } else {
                    audioSource.PlayOneShot(audioNivel, 0.7F);
                    botonesPulsados = 0;
                    nivelActual++;
                    estado = 0;
                    EmpiezaJuego();
                }
                
            }
        }
    }

    public void PulsarBoton(int colorPulsado) {
        // Comprobar si ha pulsado el que toca:
            // Si --> Siguiente iteracion
            // No --> GameOver
        if (niveles[nivelActual].combinacion[botonesPulsados] == colorPulsado)
        {
            //Debug.Log("Boton correcto!");
            botonesPulsados++;
        } else {
            audioSource.PlayOneShot(audioClipLose, 0.3F);
            audioSource.PlayOneShot(audioClipLose, 0.7F);
            ResetearSalaSimon();
            player.GetComponent<Gameover>().GameOver();
        }
    }

    void AcabaMostrarCombinacion() {
        // Reset contadores
        duracionColores = 0;
        duracionNegro = 0;
        colorONegro = false;
        coloresMostrados = 0;
        // Pasa a estado jugando
        estado = 2;
        // Empieza estado jugando
        EmpiezaFaseJugando();
    }

    void EmpiezaFaseJugando() {
        // Se habilitan los botones
        foreach (var boton in botones)
        {
            boton.enabled = true;
            boton.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        // Muestra el nivel actual
        //nivelIcono.gameObject.SetActive(true);
        //nivelIcono.sprite = iconosNiveles[nivelActual];
    }

    void DesactivarBotones() {
        // Se habilitan los botones
        foreach (var boton in botones)
        {
            boton.enabled = false;
            boton.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void CambiarColorPantalla(int color) {
        pantalla.color = colores[color];
    }

    public void ResetearSalaSimon() {
        // Vuelve la sala al estado inicial
            // Rehabilitar todos los TPs
            RehabilitaTPs();
            // Reset variables Simon
            coloresMostrados = 0;
            colorONegro = false;
            botonesPulsados = 0;
            nivelActual = 0;
            estado = 0;
            duracionColores = 0;
            duracionNegro = 0;
            countdownTime = 3;
            // Pantalla en blanco
            CambiarColorPantalla(4);
            // DeshabilitarBotones
            DesactivarBotones();
            // Reset audios subida nivel
            ResetAudiosNivel();
    }

    void RehabilitaTPs() {
        foreach (var tp in TPs)
        {
            tp.gameObject.SetActive(true);
        }
    }

    public void DesactivarTPs() {
        foreach (var tp in TPs)
        {
            tp.gameObject.SetActive(false);
        }
    }

    void ResetAudiosNivel() {

    }
}
