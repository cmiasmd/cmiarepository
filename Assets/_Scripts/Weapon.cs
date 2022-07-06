using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public WeaponType weapon;
    public new string name;
    public string type;
    public string tipo = "arma";
    public string description;

    public Sprite sprite;
    public Pista pista;

    public void NewWeapon(WeaponType newWeapon)
    {
        pista = ScriptableObject.CreateInstance<Pista>();

        weapon = newWeapon;
        switch (weapon)
        {
            case WeaponType.ADAGA:
                name = "Adaga";
                type = "Corte";
                description = "� uma adaga. Comumente usada para matar.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Adaga");
                break;
            case WeaponType.CHAVEFENDA:
                name = "Chave de Fenda";
                type = "Pancada";
                description = "Parece que alguem ficou com os parafusos frouxos...";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_ChaveFenda");
                break;
            case WeaponType.COGUMELO:
                name = "Cogumelo T�xico";
                type = "Envenenamento";
                description = "Desse aqui s� se come uma vez antes de morrer.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_CoguToxico");
                break;
            case WeaponType.FACA:
                name = "Fac�o";
                type = "Corte";
                description = "Morte por mil cortes? Bem menos que isso, na verdade.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Faca");
                break;
            case WeaponType.MACHADO:
                name = "Machado";
                type = "Corte";
                description = "�, n�o tem porta - ou pessoa - que sobreviva um golpe disso aqui.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Machado");
                break;
            case WeaponType.MARTELO:
                name = "Martelo";
                type = "Pancada";
                description = "Assassinato por concuss�o � definitivamente algo a se considerar.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Martelo");
                break;
            case WeaponType.PISTOLA:
                name = "Pistola";
                type = "Perfura��o.";
                description = "Bem a moda antiga em...";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Pistola");
                break;
            case WeaponType.PRESA:
                name = "Presa de Cobra Venenosa";
                type = "Envenenamento";
                description = "De alguma maneira, isso aqui ainda � letal. Devo ter cuidado.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_DenteCobra");
                break;
            case WeaponType.REVOLVER:
                name = "Rev�lver";
                type = "Perfura��o";
                description = "Armas de fogo s�o diretas ao ponto e s� possuem um prop�sito.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Revolver");
                break;
            case WeaponType.SACOLA:
                name = "Sacola Pl�stica";
                type = "Asfixia";
                description = "De todas as formas de se matar algu�m, essa definitivamente � uma das mais cru�is.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Sacola");
                break;
            case WeaponType.SERROTE:
                name = "Serrote";
                type = "Dilacera��o.";
                description = "Um cl�ssico dos filmes de horror ou uma ferramenta bem afiada?";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Serrote");
                break;
            case WeaponType.SILENCIADOR:
                name = "Pistola com silenciador";
                type = "Perfura��o.";
                description = "Um silenciador pode ser encontrado na pistola. Definitivamente uma arma.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Silenciador");
                break;
            case WeaponType.TESOURA:
                name = "Tesoura";
                type = "Dilacera��o.";
                description = "Uma ferramenta de corte e puntura. Arma clich�.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Tesoura");
                break;
            case WeaponType.TRAVESSEIRO:
                name = "Travesseiro";
                type = "Asfixia.";
                description = "Nada como um travesseiro fofo e bem acolchoado para um assassinato n�o �?";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Travesseiro");
                break;
            case WeaponType.VENENO:
                name = "Veneno L�quido";
                type = "Envenenamento";
                description = "O cheiro definitivamente entrega o prop�sito disso aqui: homic�dio.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Veneno");
                break;
            default:
                name = "Vazio";
                type = "Vazio";
                description = "Nada aqui.";
                sprite = null;
                break;
        }

        pista.tipo = tipo;
        pista.titulo = name;
        pista.subtitulo = type;
        pista.sprite = sprite;
        pista.description = description;
    }

    public enum WeaponType
    {
        ADAGA,
        CHAVEFENDA,
        COGUMELO,
        FACA,
        MACHADO,
        MARTELO,
        PISTOLA,
        PRESA,
        REVOLVER,
        SACOLA,
        SERROTE,
        SILENCIADOR,
        TESOURA,
        TRAVESSEIRO,
        VENENO
    }
}
