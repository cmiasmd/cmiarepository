using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostGhost : MonoBehaviour
{
    public new string name;
    public string type;
    public string tipo = "vitima";
    public string[] historyDialogue;
    public string[] commomDialogue;
    public string[] commomDialogue2;
    public string[] correctAnswer;
    public string[] wrogAnswer;

    public int typeDialogue = 0;
    public DialogueTrigger dt;
    
    public Animator anim;

    public Pista pista;
    public Sprite sprite, spr_elefante, spr_jacare, spr_touro;

    // Start is called before the first frame update
    void Start()
    {

        pista = ScriptableObject.CreateInstance<Pista>();
        typeDialogue = 0;
        string[] lostGhosts = { "elefante", "jacare", "touro" };
        type = lostGhosts[Random.Range(0, lostGhosts.Length)];

        switch (type)
        {
            case "elefante":
                name = "Heleno Fantes";
                anim.SetBool("elefante", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "Oi eu sou Heleno Fantes, to precisando chegar no para�so dos bichos, que fica no �ltimo andar, mas n�o lembro como morri e o porteiro do elevador s� permite quem conta sua hist�ria de morte";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                commomDialogue2 = new string[1];
                commomDialogue2[0] = "O que voc� descobriu?";
                correctAnswer = new string[2];
                correctAnswer[0] = "Oh... Estou conseguindo lembrar...";
                correctAnswer[1] = "Agora tudo faz sentido! Obrigado!";
                wrogAnswer = new string[1];
                wrogAnswer[0] = "Isso n�o faz sentido...";
                sprite = spr_elefante;
                break;
            case "jacare":
                name = "Jack Aur�lio";
                anim.SetBool("jacare", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "Oi, eu me chamo Jack Aur�lio, n�o sei como vim parar aqui e nem o porqu�, mas o mo�o do elevador disse que eu preciso contar minha hist�ria de morte para passar e eu simplesmente n�o lembro nada *SNIFF SNIFF";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                commomDialogue2 = new string[1];
                commomDialogue2[0] = "O que voc� descobriu?";
                correctAnswer = new string[2];
                correctAnswer[0] = "Oh... Estou conseguindo lembrar...";
                correctAnswer[1] = "Agora tudo faz sentido! Obrigado!";
                wrogAnswer = new string[1];
                wrogAnswer[0] = "Isso n�o faz sentido...";
                sprite = spr_jacare;
                break;
            case "touro":
                name = "Tom Roll";
                anim.SetBool("touro", true);
                historyDialogue = new string[1];
                historyDialogue[0] = "O tio do elevador tem sorte dele n�o ser vermelho, se n�o eu passava por ele com toda minha f�ria de touro, mas enquanto eu t� calmo n�o consigo ir para o c�u dos animais. Ser� que voc� poderia me dar uma for�a para eu lembrar minha hist�ria f�nebre?";
                commomDialogue = new string[1];
                commomDialogue[0] = "Em que posso ajud�-lo?";
                commomDialogue2 = new string[1];
                commomDialogue2[0] = "O que voc� descobriu?";
                correctAnswer = new string[2];
                correctAnswer[0] = "Oh... Estou conseguindo lembrar...";
                correctAnswer[1] = "Agora tudo faz sentido! Obrigado!";
                wrogAnswer = new string[1];
                wrogAnswer[0] = "Isso n�o faz sentido...";
                sprite = spr_touro;
                break;
            default:
                break;
        }

        dt.dialogue.name = name;
        dt.dialogue.sentences = historyDialogue;

        pista.tipo = tipo;
        pista.titulo = name;
        pista.subtitulo = type;
        pista.sprite = sprite;
        foreach (var frase in historyDialogue)
        {
            pista.description += frase + " ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (typeDialogue == 0)
        //    dt.dialogue.sentences = historyDialogue;
        //else if (typeDialogue == 1)
        //    dt.dialogue.sentences = commomDialogue;
        //else if (typeDialogue == 2)
        //    dt.dialogue.sentences = commomDialogue2;
        //else if (typeDialogue == 3)
        //    dt.dialogue.sentences = correctAnswer;
        //else if (typeDialogue == 4)
        //    dt.dialogue.sentences = wrogAnswer;
    }
}
