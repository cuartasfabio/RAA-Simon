using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    private Renderer _myRenderer;
    private GameObject _player;
    private Image _puntero;

    private float _timeToTP = 100;
    private float _timeGazing = 0;
    private bool _gazing = false;

    public AudioSource audioSource;
    public AudioClip audioClip1;

    // Start is called before the first frame update
    void Start()
    {
        
        _puntero = GameObject.Find("Puntero").GetComponent<Image>();
        _player = GameObject.Find("Player");
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
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
        SetMaterial(true);
        _gazing = true;
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        SetMaterial(false);
        _gazing = false;
        _timeGazing = 0;
        _puntero.fillAmount = 0;
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        //TeleportPlayer();
    }

    /// <summary>
    /// Teleports this instance randomly when triggered by a pointer click.
    /// </summary>
    public virtual void TeleportPlayer()
    {
        Vector3 nuevaPos = new Vector3( transform.position.x, transform.position.y + 1.8f, transform.position.z);
        audioSource.PlayOneShot(audioClip1, 0.1F);
        _player.transform.position = nuevaPos;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
