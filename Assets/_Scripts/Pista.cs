using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPista", menuName = "Pista")] 
public class Pista : ScriptableObject
{
    public string tipo;
    public string titulo;
    public string subtitulo;
    public string temperamento;
    public string description;
    public string assassinTip;
    public string weaponTip;
    public string roomTip;

    public Sprite sprite;
    public Sprite genero;
}
