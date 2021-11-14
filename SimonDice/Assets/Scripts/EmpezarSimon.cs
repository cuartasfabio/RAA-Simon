using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EmpezarSimon : MonoBehaviour
{
    public SimonGame simon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        simon.EmpiezaJuego();
        simon.DesactivarTPs();
        this.gameObject.SetActive(false);
    }
}
