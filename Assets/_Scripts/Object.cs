using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : Interactable
{

    public int id;

    public ObjectType type;
    public GameObject luz;
    public new string name;
    public string description;

    public Sprite newSprite, spr_Caixa, spr_Carrinho, spr_Espelho, spr_Estatua, spr_Planta1, spr_Planta2,
        spr_Poste, spr_QuadroCaveira, spr_QuadroOnda, spr_QuadroSapo, spr_Totem, spr_Vela, spr_Abajur,
        spr_Armario, spr_ArmarioCosta1, spr_ArmarioCosta2, spr_CamaHorizontal, spr_CamaSolteiro,
        spr_CamaVertical, spr_Cranio, spr_Livro, spr_TV, spr_Caveira, spr_ComodaVazia, spr_ComodaPlanta;

    public float scale, offsetX, offsetY, offsetXC, offsetYC, sizeX, sizeY, radius;

    public bool containsWeapon = false;
    public Weapon weapon;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRender;
    public CapsuleCollider2D colCapsule;
    public CircleCollider2D colCircle;
    public CapsuleDirection2D direction = CapsuleDirection2D.Horizontal;
    public Transform tr;
    private GUObject guo;

    public bool dialogueState = false;
    private DialogueTrigger dt;

    // Start is called before the first frame update
    void Start()
    {

        updateInfos(type);

        rb = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = newSprite;

        colCapsule = GetComponent<CapsuleCollider2D>();
        colCapsule.offset = new Vector2(offsetX, offsetY);
        colCapsule.size = new Vector2(sizeX, sizeY);
        colCapsule.direction = direction;

        colCircle = GetComponent<CircleCollider2D>();
        colCircle.offset = new Vector2(offsetXC, offsetYC);
        colCircle.radius = radius;

        tr = GetComponent<Transform>();
        tr.localScale = new Vector3(scale, scale, 0);

        guo = GetComponent<GUObject>();
        guo.updateGUO(colCapsule.bounds);

        dt = GetComponent<DialogueTrigger>();
        dt.dialogue.name = this.name;
        dt.dialogue.sentences = new string[1];
        dt.dialogue.sentences[0] = this.description;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown("space") && !this.dialogueState)
            {
                dialogueState = true;
                dt.TriggerObjectDialogue(this);
            }
        }
        else
        {
            if (dialogueState)
            {
                dt.TriggerDialogueExit();
                dialogueState = false;
            }
        }
    }

    public void updateInfos(ObjectType newType)
    {
        type = newType;
        switch (type)
        {
            case ObjectType.CAIXA:
                name = "Caixa de apelão";
                description = "Qual a chance de ter um cara estranho em baixo dessa caixa?";
                newSprite = spr_Caixa;
                offsetX = 0f;
                offsetY = -0.14f;
                offsetXC = 0f;
                offsetYC = -0.14f;
                sizeX = 0.45f;
                sizeY = 0.25f;
                radius = 0.4f;
                scale = 1.5f;

                break;
            case ObjectType.CARRINHO:
                name = "Carrinho de hotel";
                description = "Fantasmas cheio de bagagens quem nem os daqui precisam de um desses para não machucar a coluna.";
                newSprite = spr_Carrinho;
                offsetX = 0f;
                offsetY = -0.25f;
                offsetXC = 0f;
                offsetYC = -0.25f;
                sizeX = 0.8f;
                sizeY = 0.45f;
                radius = 0.5f;
                scale = 1.5f;

                break;
            case ObjectType.ESPELHO:
                name = "Espelho";
                description = "Quando se olha muito tempo para um espelho, o espelho olha para você...";
                newSprite = spr_Espelho;
                offsetX = 0f;
                offsetY = -0.22f;
                offsetXC = 0f;
                offsetYC = -0.22f;
                sizeX = 0.27f;
                sizeY = 0.18f;
                radius = 0.27f;
                scale = 3f;

                break;
            case ObjectType.ESTATUA:
                name = "Estátua";
                description = "Anjo caído para apimentar o climinha de terror desse lugar...";
                newSprite = spr_Estatua;
                offsetX = 0.01f;
                offsetY = -0.35f;
                offsetXC = 0.01f;
                offsetYC = -0.35f;
                sizeX = 0.47f;
                sizeY = 0.28f;
                radius = 0.35f;
                scale = 2f;

                break;
            case ObjectType.PLANTA1:
                name = "Cacto";
                description = "Como que isso tá viva nessa ambiente tão mórbido...?";
                newSprite = spr_Planta1;
                offsetX = 0f;
                offsetY = -0.15f;
                offsetXC = 0f;
                offsetYC = -0.15f;
                sizeX = 0.42f;
                sizeY = 0.22f;
                radius = 0.3f;
                scale = 1f;

                break;
            case ObjectType.PLANTA2:
                name = "Plantinha";
                description = "É quase poético algo estar vivo em um ambiente tão inóspito...";
                newSprite = spr_Planta2;
                offsetX = 0f;
                offsetY = -0.15f;
                offsetXC = 0f;
                offsetYC = -0.15f;
                sizeX = 0.42f;
                sizeY = 0.22f;
                radius = 0.3f;
                scale = 1f;

                break;
            case ObjectType.POSTE:
                name = "Poste de luz";
                description = "Isso aqui não deveria tá na área externa?";
                newSprite = spr_Poste;
                offsetX = -0.01f;
                offsetY = -0.31f;
                offsetXC = -0.01f;
                offsetYC = -0.31f;
                sizeX = 0.08f;
                sizeY = 0.24f;
                radius = 0.23f;
                scale = 2.5f;
                luz.SetActive(true);

                break;
            case ObjectType.QUADROCAVEIRA:
                name = "Quadro Caveiroso";
                description = "Nada melhor do que uma representação em tela da nossa finitude não?";
                newSprite = spr_QuadroCaveira;
                offsetX = 0f;
                offsetY = -0.42f;
                offsetXC = 0f;
                offsetYC = -0.42f;
                sizeX = 0.5f;
                sizeY = 0.4f;
                radius = 0.5f;
                scale = 1f;

                break;
            case ObjectType.QUADROONDA:
                name = "A Pequena Onda de Ganakawa";
                description = "Essa onda parece ser bem famosa, será que foi um surfista pintor que fez?";
                newSprite = spr_QuadroOnda;
                offsetX = 0f;
                offsetY = -0.42f;
                offsetXC = 0f;
                offsetYC = -0.42f;
                sizeX = 0.5f;
                sizeY = 0.4f;
                radius = 0.5f;
                scale = 1f;

                break;
            case ObjectType.QUADROSAPO:
                name = "Quadro de Sapinho";
                description = "Que sapinho carismático, quem será que fez essa peça?";
                newSprite = spr_QuadroSapo;
                offsetX = 0f;
                offsetY = -0.42f;
                offsetXC = 0f;
                offsetYC = -0.42f;
                sizeX = 0.5f;
                sizeY = 0.4f;
                radius = 0.5f;
                scale = 1f;

                break;
            case ObjectType.TOTEM:
                name = "Mascara de touro";
                description = "Meio vintage meio furry, o equilibrio perfeito";
                newSprite = spr_Totem;
                offsetX = 0.02f;
                offsetY = -0.21f;
                offsetXC = 0.02f;
                offsetYC = -0.21f;
                sizeX = 0.5f;
                sizeY = 0.2f;
                radius = 0.34f;
                scale = 1f;

                break;
            case ObjectType.VELA:
                name = "Vela";
                description = "Nada mais romântico que um passeio à luz de velas em um hotel mal-assombrado...";
                newSprite = spr_Vela;
                offsetX = 0f;
                offsetY = -0.22f;
                offsetXC = 0f;
                offsetYC = -0.22f;
                sizeX = 0.38f;
                sizeY = 0.25f;
                radius = 0.29f;
                scale = 1f;
                luz.SetActive(true);

                break;
            case ObjectType.ABAJUR:
                name = "Cômoda com abajur";
                description = "Um uso não muito claro de decoração.";
                newSprite = spr_Abajur;
                offsetX = 0f;
                offsetY = -0.28f;
                offsetXC = 0f;
                offsetYC = -0.28f;
                sizeX = 0.84f;
                sizeY = 0.53f;
                radius = 0.55f;
                scale = 1f;

                break;
            case ObjectType.ARMARIO:
                name = "Guarda roupa";
                description = "Nada mais do que o primor da moda de 50 anos atrás.";
                newSprite = spr_Armario;
                offsetX = -0.03f;
                offsetY = -0.52f;
                offsetXC = -0.03f;
                offsetYC = -0.52f;
                sizeX = 1.19f;
                sizeY = 0.5f;
                radius = 0.65f;
                scale = 1f;

                break;
            case ObjectType.ARMARIOCOSTA1:
                name = "Armario";
                description = "Fogo em um local rodeado por madeiras... Bem pensado.";
                newSprite = spr_ArmarioCosta1;
                offsetX = 0f;
                offsetY = -0.41f;
                offsetXC = 0f;
                offsetYC = -0.41f;
                sizeX = 1.54f;
                sizeY =0.46f;
                radius = 0.85f;
                scale = 1f;

                break;
            case ObjectType.ARMARIOCOSTA2:
                name = "Armario";
                description = "Fogo em um local rodeado por madeiras... Bem pensado.";
                newSprite = spr_ArmarioCosta2;
                offsetX = 0f;
                offsetY = -0.27f;
                offsetXC = 0f;
                offsetYC = -0.27f;
                sizeX = 1.48f;
                sizeY = 0.24f;
                radius = 0.75f;
                scale = 1f;

                break;
            case ObjectType.CAMAHORIZONTAL:
                name = "Cama de casal";
                description = "De fato uma cama, mas não recomendaria ninguem dormir aí...";
                newSprite = spr_CamaHorizontal;
                offsetX = 0f;
                offsetY = -0.25f;
                offsetXC = 0f;
                offsetYC = -0.25f;
                sizeX = 2.07f;
                sizeY = 1.07f;
                radius = 1.3f;
                scale = 1f;

                break;
            case ObjectType.CAMASOLTEIRO:
                name = "Cama de solteiro";
                description = "Uma cama. Não parece ter visto muito uso recentemente...";
                newSprite = spr_CamaSolteiro;
                direction = CapsuleDirection2D.Vertical;
                offsetX = 0f;
                offsetY = -0.2f;
                offsetXC = 0f;
                offsetYC = -0.2f;
                sizeX = 0.82f;
                sizeY = 1.87f;
                radius = 0.97f;
                scale = 1f;

                break;
            case ObjectType.CAMAVERTICAL:
                name = "Cama de casal";
                description = "De fato uma cama, mas não recomendaria ninguem dormir aí";
                newSprite = spr_CamaVertical;
                direction = CapsuleDirection2D.Vertical;
                offsetX = 0f;
                offsetY = -0.32f;
                offsetXC = 0f;
                offsetYC = -0.32f;
                sizeX = 1.52f;
                sizeY = 2.05f;
                radius = 1.13f;
                scale = 1f;

                break;
            case ObjectType.CRANIO:
                name = "Cômoda com caveira";
                description = "Essa seria ou não seria a cômoda de Shakespeare, eis a questão...";
                newSprite = spr_Cranio;
                offsetX = 0f;
                offsetY = -0.17f;
                offsetXC = 0f;
                offsetYC = -0.17f;
                sizeX = 0.78f;
                sizeY = 0.55f;
                radius = 0.56f;
                scale = 1f;

                break;
            case ObjectType.LIVRO:
                name = "Cômoda com livro";
                description = "Nunca julgue um livro pela capa, nem pelos seus mofos!";
                newSprite = spr_Livro;
                offsetX = 0f;
                offsetY = -0.19f;
                offsetXC = 0f;
                offsetYC = -0.19f;
                sizeX = 0.7f;
                sizeY = 0.54f;
                radius = 0.53f;
                scale = 1f;

                break;
            case ObjectType.TV:
                name = "Televisão";
                description = "Qual a chance de sair algum espirito maligno de dentro dessa tevê?";
                newSprite = spr_TV;
                offsetX = -0.03f;
                offsetY = -0.3f;
                offsetXC = 0f;
                offsetYC = -0.3f;
                sizeX = 1.39f;
                sizeY = 0.6f;
                radius = 0.76f;
                scale = 1f;

                break;
            case ObjectType.CAVEIRA:
                name = "Caveira";
                description = "Quais as chances de isso aqui ser de mentira?";
                newSprite = spr_Caveira;
                offsetX = 0f;
                offsetY = 0f;
                offsetXC = 0f;
                offsetYC = 0f;
                sizeX = 0.5f;
                sizeY = 0.5f;
                radius = 0.5f;
                scale = 1f;

                break;
            case ObjectType.COMODAVAZIA:
                name = "Cômoda simples";
                description = "De fato uma cômoda simples, parece que ninguem quis colocar algo interessante aqui em cima.";
                newSprite = spr_ComodaVazia;
                offsetX = 0f;
                offsetY = -0.15f;
                offsetXC = 0f;
                offsetYC = 0f;
                sizeX = 0.74f;
                sizeY = 0.47f;
                radius = 0.6f;
                scale = 1f;

                break;
            case ObjectType.COMODAPLANTA:
                name = "Comoda com planta";
                description = "Será que as assombrações locais lembram de regar?";
                newSprite = spr_ComodaPlanta;
                offsetX = 0f;
                offsetY = -0.15f;
                offsetXC = 0f;
                offsetYC = 0f;
                sizeX = 0.74f;
                sizeY = 0.47f;
                radius = 0.6f;
                scale = 1f;

                break;
            default:
                break;
        }
    }

    public enum ObjectType
    {
        CAIXA,
        CARRINHO,
        ESPELHO,
        ESTATUA,
        PLANTA1,
        PLANTA2,
        POSTE,
        QUADROCAVEIRA,
        QUADROONDA,
        QUADROSAPO,
        TOTEM,
        VELA,
        ABAJUR,
        ARMARIO,
        ARMARIOCOSTA1,
        ARMARIOCOSTA2,
        CAMAHORIZONTAL,
        CAMASOLTEIRO,
        CAMAVERTICAL,
        CRANIO,
        LIVRO,
        TV,
        CAVEIRA,
        COMODAVAZIA,
        COMODAPLANTA

    }
}
