using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGhost : MonoBehaviour
{
    public new string name;
    public string type;
    public bool assasin = false;
    public string tipo = "suspeito";
    public string genre;
    public string profession;
    public string[] historyDialogue;
    public string[] commomDialogue;
    public string[] aboutAssassin;
    public string[] aboutWeapon;
    public string[] aboutRoom;
    public string weapon;
    public string assassinType;
    public string conjunto;
    public string temperamento;
    public Sprite spr_fantasma1, spr_fantasma2, spr_fantasma3, spr_fantasma4, spr_fantasma5;
    public Sprite male, female, interrogate;
    public Sprite gender;
    public Sprite sprite;

    public Pista pista;

    public string fraseArma;
    public string fraseRoom;

    public int typeDialogue = 0;
    public DialogueTrigger dt;

    public Animator anim;

    public bool getHistory = false;

    // Start is called before the first frame update
    void Start()
    {
        pista = ScriptableObject.CreateInstance<Pista>();
        typeDialogue = 0;

        switch (type)
        {
            case "fantasma1":
                name = "Diogo Amarante";
                genre = "Masculino";
                profession = "Camareiro";
                temperamento = "Calmo";
                anim.SetBool("fantasma1", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "Ol�, me chamo Diogo, Diogo Amarante. Eu fui camareiro deste hotel, eu passei 20 anos da minha vida por aqui limpando as fronhas sujas...";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                gender = male;
                sprite = spr_fantasma1;


                if (assasin) //Se assassino fantasma 1
                {
                    aboutAssassin = new string[1];
                    aboutAssassin[0] = "O assassino parecia estar muito calmo.";
                }
                else //Se n�o fantasma 1
                {
                    switch (assassinType)
                    {
                        case "fantasma2":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino estava usando uma farda.";
                            break;
                        case "fantasma3":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino com certeza n�o trabalhava no hotel.";
                            break;
                        case "fantasma4":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "Pelo que eu sei, o assassino � uma mulher.";
                            break;
                        case "fantasma5":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia estar muito calmo.";
                            break;
                        default:
                            break;
                    }
                }

                break;
            case "fantasma2":
                name = "Silvio Luiz";
                genre = "Masculino";
                profession = "Policial";
                temperamento = "Raivoso";
                anim.SetBool("fantasma2", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "QUEM EU SOU?! Se voc� olhar bem meu distintivo� ah n�o, n�o t� mais de farda� Sou Silvio Luiz, quem manda no peda�o, ou mandava�";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                sprite = spr_fantasma2;
                gender = male;


                if (assasin) //Se assassino fantasma 2
                {
                    aboutAssassin = new string[1];
                    aboutAssassin[0] = "O assassino fazia parte do quadro de funcion�rios do hotel.";
                }
                else //Se n�o fantasma 2
                {
                    switch (assassinType)
                    {
                        case "fantasma1":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino estava usando uma farda.";
                            break;
                        case "fantasma3":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia trabalhar com algo que mexe com a vida.";
                            break;
                        case "fantasma4":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia estar muito calmo.";
                            break;
                        case "fantasma5":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "N�o sei com qual g�nero o assassino se identifica.";
                            break;
                        default:
                            break;
                    }
                }

                break;
            case "fantasma3":
                name = "Cris Valentin";
                genre = "Feminino";
                profession = "Enfermeira";
                temperamento = "Calma";
                anim.SetBool("fantasma3", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "Ol�, me chamo Cris, sou enfermeira e estava de f�rias neste hotel. Que p�ssima ideia eu diria, foi minha �ltima p�ssima escolha...";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                sprite = spr_fantasma3;
                gender = female;

                if (assasin) //Se assassino fantasma 3
                {
                    aboutAssassin = new string[1];
                    aboutAssassin[0] = "Pelo que eu sei, o assassino � um homem.";
                }
                else //Se n�o fantasma 3
                {
                    switch (assassinType)
                    {
                        case "fantasma1":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino fazia parte do quadro de funcion�rios do hotel.";
                            break;
                        case "fantasma2":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia trabalhar com algo que mexe com a vida.";
                            break;
                        case "fantasma4":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino estava usando uma roupa de camuflagem.";
                            break;
                        case "fantasma5":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino fazia parte do quadro de funcion�rios do hotel.";
                            break;
                        default:
                            break;
                    }
                }

                break;
            case "fantasma4":
                name = "Raquel Moreira";
                genre = "Feminino";
                profession = "Ca�adora";
                temperamento = "Calma";
                anim.SetBool("fantasma4", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "OOpa! Pensei que ningu�m tinha me visto aqui. Eu me chamo Raquel, mas pode me chamar de Raquel, A ca�adora. Que saudade de ser a ca�adora e n�o a ca�a�";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                sprite = spr_fantasma4;
                gender = female;

                if (assasin) //Se assassino fantasma 4
                {
                    aboutAssassin = new string[1];
                    aboutAssassin[0] = "O assassino fazia parte do quadro de funcion�rios do hotel.";
                }
                else //Se n�o fantasma 4
                {
                    switch (assassinType)
                    {
                        case "fantasma1":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia trabalhar com manuten��o.";
                            break;
                        case "fantasma2":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "Pelo que eu sei, o assassino � um homem.";
                            break;
                        case "fantasma3":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia estar muito calmo.";
                            break;
                        case "fantasma5":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino estava usando uma farda.";
                            break;
                        default:
                            break;
                    }
                }

                break;
            case "fantasma5":
                name = "Alexis Martin";
                genre = "N�o Identificado";
                profession = "Zelador(a)";
                temperamento = "Calmo(a)";
                anim.SetBool("fantasma5", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "Ele ou ela, n�o saberia te dizer quem sou, pode me chamar de Alexis. Sempre afirmei com plenitude que esse recinto era p�ssimo para recepcionar as pessoas e os animais.";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                sprite = spr_fantasma5;
                gender = interrogate;

                if (assasin) //Se assassino fantasma 5
                {
                    aboutAssassin = new string[1];
                    aboutAssassin[0] = "O assassino parecia trabalhar com algo que mexe com a vida.";
                }
                else //Se n�o fantasma 5
                {
                    switch (assassinType)
                    {
                        case "fantasma1":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "Pelo que eu sei, o assassino � um homem.";
                            break;
                        case "fantasma2":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia ser alguem muito raivoso.";
                            break;
                        case "fantasma3":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino estava usando uma farda.";
                            break;
                        case "fantasma4":
                            aboutAssassin = new string[1];
                            aboutAssassin[0] = "O assassino parecia trabalhar com algo que mexe com a vida.";
                            break;
                        default:
                            break;
                    }                    
                }

                break;
            default:
                break;
        }


        aboutWeapon = new string[1];
        aboutWeapon[0] = fraseArma;
        aboutRoom = new string[1];
        aboutRoom[0] = fraseRoom;

        dt.dialogue.name = name;
        dt.dialogue.sentences = historyDialogue;
        dt.dialogue.common = commomDialogue;
        dt.dialogue.assassin = aboutAssassin;
        dt.dialogue.weapon = aboutWeapon;
        dt.dialogue.local = aboutRoom;

        pista.tipo = tipo;
        pista.titulo = name;
        pista.subtitulo = profession;
        pista.temperamento = temperamento;
        pista.sprite = sprite;
        pista.genero = gender;
        //foreach (var frase in historyDialogue)
        //{
        //    pista.description += frase + " ";
        //}
        //pista.assassinTip = aboutAssassin[0];
        //pista.weaponTip = aboutWeapon[0];
        //pista.roomTip = aboutRoom[0];


}
    void Update()
    {
        
    }
}
