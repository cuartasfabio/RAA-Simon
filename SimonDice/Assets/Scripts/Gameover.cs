using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject triggerStart;
    public AudioSource audioSource;
    public AudioClip audioClip1;
    public Canvas pantallazo;
    public Light luzSimon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        StartCoroutine(MuereYResucita());
    }

    IEnumerator MuereYResucita() {
        // Luz simon en rojo
        luzSimon.color = Color.red;
        // Sonido disparo
        audioSource.PlayOneShot(audioClip1, 0.5F);
        // Delay de 3 segundos
        yield return new WaitForSeconds(3.5f); 
        // Pantallazo en negro
        pantallazo.gameObject.SetActive(true);
        // Delay de 1 segundo
        yield return new WaitForSeconds(2.5f); 
        // Reiniciar escena y mover PJ al principio de la sala
        CheckpointPJ();
        // Reset a luz simon
        luzSimon.color = Color.cyan;
        // Rehabilitar trigger juego
        pantallazo.gameObject.SetActive(false);
    }

    public void CheckpointPJ() {
        this.gameObject.transform.position = new Vector3(-4.5f,1.9f,62.2f);
        triggerStart.SetActive(true);
        //GameObject.Find("StartGameTrigger").SetActive(true);
    }
}
