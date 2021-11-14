using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartJuegoBoton : MonoBehaviour
{
    private Image _puntero;
    public SimonGame simon;
    public Gameover gameover;
    private float _timeToTP = 100;
    private float _timeGazing = 0;
    private bool _gazing = false;

    // Start is called before the first frame update
    void Start()
    {
        _puntero = GameObject.Find("Puntero").GetComponent<Image>();
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(_gazing) {
            if(_timeGazing >= _timeToTP) {
                TeleportPlayer();
            } else {
                _timeGazing++;
                _puntero.fillAmount = _timeGazing / _timeToTP;
                //Debug.Log(_puntero.fillAmount);
            }
            
        }
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
    
        _gazing = true;
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
       
        _gazing = false;
        _timeGazing = 0;
        _puntero.fillAmount = 0;
    }

    /// <summary>
    /// Teleports this instance randomly when triggered by a pointer click.
    /// </summary>
    public virtual void TeleportPlayer()
    {
       simon.ResetearSalaSimon();
       gameover.CheckpointPJ();
    }

    
}
