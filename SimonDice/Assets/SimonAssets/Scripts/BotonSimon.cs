using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSimon : MonoBehaviour
{
    [SerializeField] Material materialApagado;
    [SerializeField] Material materialPulsado;
    [SerializeField] Material gazedAtMaterial;
    private Renderer _myRenderer;
    private Image _puntero;
    private float _timeToTP = 100;
    private float _timeGazing = 0;
    private bool _gazing = false;
    public SimonGame simon;
    public AudioSource audioSource;
    
    
    
    // Start is called before the first frame update
    void OnEnable()
    {
        _puntero = GameObject.Find("Puntero").GetComponent<Image>();
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_gazing) {
            if(_timeGazing >= _timeToTP) {
                // Pulsar el boton
                PulsarBoton();
                _gazing = false;
            } else {
                _timeGazing++;
                _puntero.fillAmount = _timeGazing / _timeToTP;
                //Debug.Log(_puntero.fillAmount);
            }
            
        }
    }

    void PulsarBoton() {
        _myRenderer.material = materialPulsado;
        simon.PulsarBoton(simon.botones.IndexOf(this));
        audioSource.PlayOneShot(simon.AudioColores[simon.botones.IndexOf(this)], 0.7F);
        StartCoroutine(ApagarBoton());
    }

    IEnumerator ApagarBoton() {

        yield return new WaitForSeconds(1f);

         _myRenderer.material = materialApagado;
    }

    public void OnPointerEnter()
    {
        SetMaterial(true);
        _gazing = true;
    }

    public void OnPointerExit()
    {
        SetMaterial(false);
        _gazing = false;
        _timeGazing = 0;
        _puntero.fillAmount = 0;
    }

    private void SetMaterial(bool gazedAt)
    {
        if (materialApagado != null && gazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? gazedAtMaterial : materialApagado;
        }
    }
}
