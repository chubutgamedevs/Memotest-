using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject PanelAdelante;


    Sprite imagenAdelante;




    void OnEnable()
    {
        EventManager.SeSeleccionaPar += ConGelarClickTemporalmente;
    }

    void OnDisable()
    {
        EventManager.SeSeleccionaPar -= ConGelarClickTemporalmente;
    }
    private void Start()
    {
        PanelAdelante = gameObject.transform.GetChild(1).gameObject;

    }

    private void OnMouseDown()
    {
        if (this.tag != "Deshabilitado")
        {
            CongelarClickIndefinidamente();
            Rotar();
        }
    }

    void ConGelarClickTemporalmente()
    {
        // congelamos click
        this.tag = "Deshabilitado";
        // habilitamosClick
        Invoke("HabilitarClick", Globales.TiempoDeMuestraDeCartas);
    }

    void CongelarClickIndefinidamente()
    {
        // congelamos click
        this.tag = "Deshabilitado";
    }

    void HabilitarClick()
    {
        this.tag = "Habilitado";
    }


    public Sprite getImagenAdelante()
    {
        return PanelAdelante.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetearImagenAdelante(Sprite sprite)
    {
        PanelAdelante.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private GameObject getCarta()
    {
        return gameObject.transform.parent.gameObject;
    }

    private Rotate ScriptDeRotacion()
    {
        return gameObject.GetComponent<Rotate>();
    }

    public void Rotar()
    {
        StartCoroutine(RotarAntesDeTiempo());
    }

    public void Ocultar()
    {
        StartCoroutine(RotarDespuesDeTiempo());
    }

    private IEnumerator RotarAntesDeTiempo()
    {
        ScriptDeRotacion().StartRotating(Globales.TiempoDeRotacion);
        yield return new WaitForSeconds(Globales.TiempoDeRotacion);
        EventManager.OnCartaDescubierta(this);
        yield return new WaitForSeconds(Globales.TiempoDeMuestraDeCartas - Globales.TiempoDeRotacion);

    }

    private IEnumerator RotarDespuesDeTiempo()
    {
        yield return new WaitForSeconds(Globales.TiempoDeMuestraDeCartas);
        ScriptDeRotacion().StartRotatingBackwards(Globales.TiempoDeRotacion);
        yield return new WaitForSeconds(Globales.TiempoDeRotacion);
        EventManager.OnCartaOcultada(this);
        this.HabilitarClick();
    }

    public void destruirse()
    {
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(Globales.TiempoDeMuestraDeCartas);
        Destroy(gameObject);
        EventManager.OnCartaDestruida();
    }


}
