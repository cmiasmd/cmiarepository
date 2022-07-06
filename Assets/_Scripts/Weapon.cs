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
                description = "É uma adaga. Comumente usada para matar.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Adaga");
                break;
            case WeaponType.CHAVEFENDA:
                name = "Chave de Fenda";
                type = "Pancada";
                description = "Parece que alguem ficou com os parafusos frouxos...";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_ChaveFenda");
                break;
            case WeaponType.COGUMELO:
                name = "Cogumelo Tóxico";
                type = "Envenenamento";
                description = "Desse aqui só se come uma vez antes de morrer.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_CoguToxico");
                break;
            case WeaponType.FACA:
                name = "Facão";
                type = "Corte";
                description = "Morte por mil cortes? Bem menos que isso, na verdade.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Faca");
                break;
            case WeaponType.MACHADO:
                name = "Machado";
                type = "Corte";
                description = "É, não tem porta - ou pessoa - que sobreviva um golpe disso aqui.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Machado");
                break;
            case WeaponType.MARTELO:
                name = "Martelo";
                type = "Pancada";
                description = "Assassinato por concussão é definitivamente algo a se considerar.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Martelo");
                break;
            case WeaponType.PISTOLA:
                name = "Pistola";
                type = "Perfuração.";
                description = "Bem a moda antiga em...";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Pistola");
                break;
            case WeaponType.PRESA:
                name = "Presa de Cobra Venenosa";
                type = "Envenenamento";
                description = "De alguma maneira, isso aqui ainda é letal. Devo ter cuidado.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_DenteCobra");
                break;
            case WeaponType.REVOLVER:
                name = "Revólver";
                type = "Perfuração";
                description = "Armas de fogo são diretas ao ponto e só possuem um propósito.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Revolver");
                break;
            case WeaponType.SACOLA:
                name = "Sacola Plástica";
                type = "Asfixia";
                description = "De todas as formas de se matar alguém, essa definitivamente é uma das mais cruéis.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Sacola");
                break;
            case WeaponType.SERROTE:
                name = "Serrote";
                type = "Dilaceração.";
                description = "Um clássico dos filmes de horror ou uma ferramenta bem afiada?";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Serrote");
                break;
            case WeaponType.SILENCIADOR:
                name = "Pistola com silenciador";
                type = "Perfuração.";
                description = "Um silenciador pode ser encontrado na pistola. Definitivamente uma arma.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Silenciador");
                break;
            case WeaponType.TESOURA:
                name = "Tesoura";
                type = "Dilaceração.";
                description = "Uma ferramenta de corte e puntura. Arma clichê.";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Tesoura");
                break;
            case WeaponType.TRAVESSEIRO:
                name = "Travesseiro";
                type = "Asfixia.";
                description = "Nada como um travesseiro fofo e bem acolchoado para um assassinato não é?";
                sprite = Resources.Load<Sprite>("Sprites/Armas/spr_Travesseiro");
                break;
            case WeaponType.VENENO:
                name = "Veneno Líquido";
                type = "Envenenamento";
                description = "O cheiro definitivamente entrega o propósito disso aqui: homicídio.";
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
