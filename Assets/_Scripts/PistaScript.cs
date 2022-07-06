using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistaScript : MonoBehaviour
{
    public Pista pista;
    public Image portrait;
    public Image genero;
    public Text titulo;
    public Text subtitulo;
    public Text temperamento;
    public Text description;

    public void UpdateInfosDiario()
    {

        if(pista.tipo == "suspeito")
        {
            portrait.sprite = pista.sprite;
            genero.sprite = pista.genero;
            var tempColor = Color.white;
            tempColor.a = 1f;
            portrait.color = tempColor;
            genero.color = tempColor;
            titulo.text = pista.titulo;
            subtitulo.text = pista.subtitulo;
            temperamento.text = pista.temperamento;
            description.text = pista.description + "\n\n" + pista.assassinTip + "\n\n" + pista.weaponTip + "\n\n" + pista.roomTip;
        }
        else
        {
            portrait.sprite = pista.sprite;
            var tempColor = Color.white;
            tempColor.a = 1f;
            portrait.color = tempColor;
            titulo.text = pista.titulo;
            subtitulo.text = pista.subtitulo;
            description.text = pista.description + "\n\n" + pista.assassinTip + "\n\n" + pista.weaponTip + "\n\n" + pista.roomTip;
        }
    }
}
