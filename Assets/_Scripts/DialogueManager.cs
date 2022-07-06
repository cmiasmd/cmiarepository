using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue:")]
    [Space(5)]
    public Text nameText;
    public Text dialogueText;
    public bool dialogueState;
    public Button button1;
    public Text textButton1;
    public Button button2;
    public Text textButton2;
    public Button button3;
    public Text textButton3;
    public Button button5;
    public Button button6;
    public Button button7;
    public Animator animator;
    private Queue<string> sentences;
    [Space(10)]

    [Header("ItemFounded:")]
    [Space(5)]
    public Image itemFounded;
    public Image sprite;
    public Animator itemAnimator;
    [Space(10)]

    [Header("LostGhost:")]
    [Space(5)]
    public Button button4;
    public Text textButton4;
    [Space(10)]


    [Header("RevealPanel:")]
    [Space(5)]
    public GameObject RP;
    public Text assassinText;
    public Text weaponText;
    public Text roomText;
    public Button confirmButtom;
    public Button btnUp1;
    public Button btnUp2;
    public Button btnUp3;
    public Button btnDown1;
    public Button btnDown2;
    public Button btnDown3;
    private int assassinIndex = 0;
    private int weaponIndex = 0;
    private int roomIndex = 0;
    [Space(10)]


    [Header("Others:")]
    [Space(5)]
    private Object currentObj;
    private bool containsWeapon = false;
    private LostGhost currentLG;
    private CommonGhost currentCG;
    public GameManager gm;
    private Dialogue dg;
    private GhostInteract ghostInteract;
    private NPCBounds npcBounds;
    public HashSet<Pista> pistas;
    public GameConfig gc;
    void Start()
    {
        sentences = new Queue<string>();
        pistas = new HashSet<Pista>();
    }

    public void CheckCase()
    {
        confirmButtom.gameObject.SetActive(false);
        if ((assassinText.text == gc.assassin) && (weaponText.text == gc.weapon) && (roomText.text == gc.room))
        {
            string sentence = "Aaaaah! Agora eu lembro! Muito obrigado!";
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));

            Debug.Log("Sucesso");
            gm.State = GameManager.GameState.SUCESS;
        }
        else
        {
            string sentence = "Eéeh... Acho que não foi bem isso não... Não lembro de nada do tipo.";
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            Debug.Log("Falha");
            gm.State = GameManager.GameState.FAIL;
        }
    }
    public void StartDialogue (Dialogue dialogue, NPCBounds npc)
    {
        animator.SetBool("isOpen", true);
        dg = dialogue;
        npcBounds = npc;
        currentCG = npc.GetComponent<CommonGhost>();
        nameText.text = dialogue.name;
        textButton1.text = "CONTINUAR";

        sentences.Clear();

        if(gm.State == GameManager.GameState.INTRO)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            if (!currentCG.getHistory)
            {
                currentCG.getHistory = true;
                foreach (var frase in currentCG.historyDialogue)
                {
                    currentCG.pista.description += frase + " ";
                }

                pistas.Add(currentCG.pista);
                foreach (string sentence in dialogue.sentences)
                {
                    sentences.Enqueue(sentence);
                }
            }
            else
            {
                button5.gameObject.SetActive(true);
                button6.gameObject.SetActive(true);
                button7.gameObject.SetActive(true);
                foreach (string sentence in dialogue.common)
                {
                    sentences.Enqueue(sentence);
                }
            }
        }

        DisplayNextSentence();
    }

    public void AboutAssassin()
    {
        currentCG.pista.assassinTip = currentCG.aboutAssassin[0];

        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        sentences.Clear();

        foreach (string sentence in dg.assassin)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void AboutWeapon()
    {
        currentCG.pista.weaponTip = currentCG.aboutWeapon[0];

        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        sentences.Clear();

        foreach (string sentence in dg.weapon)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void AboutRoom()
    {
        currentCG.pista.roomTip = currentCG.aboutRoom[0];

        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        sentences.Clear();

        foreach (string sentence in dg.local)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void StartObjectDialogue(Dialogue dialogue, Object obj)
    {
        animator.SetBool("isOpen", true);
        currentObj = obj;
        containsWeapon = obj.containsWeapon;

        nameText.text = dialogue.name;
        textButton1.text = "CONTINUAR";

        if (gm.State != GameManager.GameState.INTRO)
        {
            button2.gameObject.SetActive(true);
            textButton2.text = "INVESTIGAR";
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void InvestigateObject()
    {
        string sentence = "";
        if (containsWeapon)
        {
            sentence = "Encontrei algo!";
            currentObj.containsWeapon = false;
            itemAnimator.SetBool("founded", true);
            sprite.sprite = currentObj.weapon.sprite;
            pistas.Add(currentObj.weapon.pista);
        }
        else
        {
            sentence = "Parece que não há nada por aqui...";
        }
        sentences.Clear();
        button2.gameObject.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            textButton1.text = "SAIR";
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void StartDialogueLostGhots(Dialogue dialogue, LostGhost lg)
    {
        animator.SetBool("isOpen", true);
        currentLG = lg;
        ghostInteract = currentLG.GetComponent<GhostInteract>();
        nameText.text = dialogue.name;
        textButton1.text = "CONTINUAR";

        sentences.Clear();

        if (gm.State == GameManager.GameState.INTRO)
            gm.updateGameState(GameManager.GameState.INVESTIGATE);

        if (currentLG.typeDialogue == 0)
        {
            currentLG.typeDialogue = 1;
            foreach (string sentence in currentLG.historyDialogue)
            {
                sentences.Enqueue(sentence);
            }

            pistas.Add(currentLG.pista);
        }
        else if (currentLG.typeDialogue == 1)
        {
            foreach (string sentence in currentLG.commomDialogue)
            {
                sentences.Enqueue(sentence);
            }
            button4.gameObject.SetActive(true);
            textButton4.text = "REVELAR";            
        }

        DisplayNextSentencelg();
    }
    public void DisplayNextSentencelg()
    {
        if (sentences.Count == 1)
        {
            textButton1.text = "SAIR";
        }
        if (sentences.Count == 0)
        {
            EndDialoguelg();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    public void RevealCase()
    {
        currentLG.typeDialogue = 2;
        button4.gameObject.SetActive(false);
        confirmButtom.gameObject.SetActive(true);
        UpdateCase();
        string sentence = currentLG.commomDialogue2[0];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void UpdateCase()
    {
        RP.gameObject.SetActive(true);
        
        string[] roomsInventory = { "Quarto 1"+gc.roomsNumbers[0], "Quarto 1" + gc.roomsNumbers[1], "Quarto 1" + gc.roomsNumbers[2], "Quarto 1" + gc.roomsNumbers[3], "Quarto 1" + gc.roomsNumbers[4] };

        int qntSusp = 0;
        int qntWeapons = 0;

        foreach (var pista in pistas)
        {

            if (pista != null && pista.tipo == "suspeito")
            {
                qntSusp++;
            }

            if (pista != null && pista.tipo == "arma")
            {
                qntWeapons++;
            }
        }
        string[] assassinsInventory = new string[qntSusp];
        string[] weaponsInventory = new string[qntWeapons];

        int indSusp = 0;
        int indWeapons = 0;

        foreach (var pista in pistas)
        {
            if (pista != null && pista.tipo == "suspeito")
            {
                assassinsInventory[indSusp] = pista.titulo;
                indSusp++;
            }

            if (pista != null && pista.tipo == "arma")
            {
                weaponsInventory[indWeapons] = pista.titulo;
                indWeapons++;
            }
        }

        if (assassinIndex == 0)
            btnUp1.interactable = false;
        else
            btnUp1.interactable = true;

        if (assassinIndex == assassinsInventory.Length - 1)
            btnDown1.interactable = false;
        else
            btnDown1.interactable = true;

        if (weaponIndex == 0)
            btnUp2.interactable = false;
        else
            btnUp2.interactable = true;

        if (weaponIndex == weaponsInventory.Length - 1)
            btnDown2.interactable = false;
        else
            btnDown2.interactable = true;

        if (roomIndex == 0)
            btnUp3.interactable = false;
        else
            btnUp3.interactable = true;

        if (roomIndex == roomsInventory.Length - 1)
            btnDown3.interactable = false;
        else
            btnDown3.interactable = true;
        if(assassinsInventory != null)
            assassinText.text = assassinsInventory[assassinIndex];
        if (weaponsInventory != null)
            weaponText.text = weaponsInventory[weaponIndex];
        if (roomsInventory != null)
            roomText.text = roomsInventory[roomIndex];
    }

    public void ClickedUpAssasin()
    {
        assassinIndex--;
        UpdateCase();
    }

    public void ClickedDownAssasin()
    {
        assassinIndex++;
        UpdateCase();
    }

    public void ClickedUpWeapon()
    {
        weaponIndex--;
        UpdateCase();
    }

    public void ClickedDownWeapon()
    {
        weaponIndex++;
        UpdateCase();
    }

    public void ClickedUpRoom()
    {
        roomIndex--;
        UpdateCase();
    }

    public void ClickedDownRoom()
    {
        roomIndex++;
        UpdateCase();
    }



    public void EndDialoguelg()
    {
        currentLG.typeDialogue = 1;
        animator.SetBool("isOpen", false);
        itemAnimator.SetBool("founded", false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        itemAnimator.SetBool("founded", false);
        RP.gameObject.SetActive(false);
        if (currentObj != null)
            currentObj.dialogueState = false;
        if(ghostInteract != null)
            ghostInteract.dialogueState = false;
        if (npcBounds != null)
            npcBounds.dialogueState = false;

        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        confirmButtom.gameObject.SetActive(false);
    }
}
