using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
    public Barrera puerta;
    public float tiempoEspera;

    public AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;

    void OnTriggerEnter(Collider other) {
        StartCoroutine(PuertaDelay());
    }

    void OnTriggerExit(Collider other) {
        puerta.CloseDoor();
    }

    IEnumerator PuertaDelay() {
        audioSource.PlayOneShot(audioClip1, 0.7F);
        yield return new WaitForSeconds(tiempoEspera);
        audioSource.PlayOneShot(audioClip2, 0.7F);
        puerta.OpenDoor();
    }
}
