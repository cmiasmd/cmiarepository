using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    public static GameConfig instance;

    public GameObject Player;
    public bool playerInRange;
    public LostGhost lg;
    public Text roomText;

    public List<HashSet<Vector2Int>> roomFloorList = new List<HashSet<Vector2Int>>();
    public Queue<GameObject> gameObjectsQueue;
    public Bounds[] roomsBounds;
    public int[] roomsNumbers;

    public string lostGhostType;
    public string assassin, weapon, conjunto, room;
    public int caseRoomNumber;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        roomsNumbers = new int[roomsBounds.Length];
        lostGhostType = lg.type;
        GenerateRoomsNumbers();
        DistributeWeapons(gameObjectsQueue);
        GenerateCase();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentRoom();
    }

    private void UpdateCurrentRoom()
    {
        int currentRoom = -1;

        for (int i = 0; i < roomsBounds.Length; i++)
        {
            Bounds room = new Bounds(roomsBounds[i].center, roomsBounds[i].extents);
            if (room.Contains(Player.transform.position))
            {
                currentRoom = i;
            }
        }
        if (currentRoom == -1)
            roomText.text = "Corredor";
        else
        {
            roomText.text = "Quarto 1" + roomsNumbers[currentRoom];
            if (currentRoom == roomsBounds.Length - 1)
                roomText.color = Color.red;
            else
                roomText.color = Color.black;
        }
    }

    private void GenerateRoomsNumbers()
    {
        int x;
        for (int i = 0; i < roomsNumbers.Length; i++)
        {
            x = Random.Range(10, 100);
            if (i == 0)
                roomsNumbers[i] = x;
            else
            {
                bool add = false;
                while (!add)
                {
                    bool temp = true;
                    for (int j = 0; j < i; j++)
                    {                        
                        if (x == roomsNumbers[j] && temp)
                        {
                            temp = false;
                        }
                    }
                    if (temp)
                        add = true;
                    else
                        x = Random.Range(10, 100);
                }
                roomsNumbers[i] = x;
            }
        }
    }

    private void GenerateCase()
    {
        string[] weapons = {"Pistola com Silenciador", "Serrote", "Veneno L�quido", "Travesseiro", "Rev�lver"};
        string[] lostGhosts = {"elefante","jacare","touro"};

        lostGhostType = lg.type;

        weapon = weapons[Random.Range(0, weapons.Length)];
        caseRoomNumber = roomsNumbers.Length - 1;
        room = "Quarto 1" + roomsNumbers[caseRoomNumber];
    }

    public Vector2 RandomPointInBoundsCenter(Bounds bounds)
    {
        return new Vector2(
            (bounds.center.x) + Random.Range((-bounds.extents.x / 4) + 1.5f, (bounds.extents.x / 4) - 1.5f),
            (bounds.center.y) + Random.Range((-bounds.extents.y / 4) + 1.5f, (bounds.extents.y / 4) - 1.5f)
        );
    }

    public Vector2 RandomPointInBoundsArea(Bounds bounds, string position, float ajuste, float ajuste2)
    {
        switch (position)
        {
            case "Cima":
                if (Random.Range(1, 3) == 1)
                {
                    return new Vector2(
                        bounds.center.x + Random.Range((-bounds.extents.x / 2) + 0.5f, -2f) + ajuste2,
                        bounds.center.y + (bounds.extents.y / 2) + ajuste
                    );
                }
                else
                {
                    return new Vector2(
                        bounds.center.x + Random.Range((bounds.extents.x / 2) - 0.5f, 2f) - ajuste2,
                        bounds.center.y + (bounds.extents.y / 2) + ajuste
                    );
                }
            case "Centro":
                return new Vector2(
                    (bounds.center.x) + Random.Range((-bounds.extents.x / 4) + 1.5f, (bounds.extents.x / 4) - 1.5f),
                    (bounds.center.y) + Random.Range((-bounds.extents.y / 4) + 1.5f, (bounds.extents.y / 4) - 1.5f)
                );
            case "Direita":
                if (Random.Range(1, 3) == 1)
                {
                    return new Vector2(
                        bounds.center.x + (bounds.extents.x / 2) - 0.5f + ajuste,
                        bounds.center.y + Random.Range((-bounds.extents.y / 2) + 1.5f, -3f) + ajuste2
                    );
                }
                else
                {
                    return new Vector2(
                        bounds.center.x + (bounds.extents.x / 2) - 0.5f + ajuste,
                        bounds.center.y + Random.Range((bounds.extents.y / 2) - 1.5f, 3f) - ajuste2
                    );
                }
            case "Esquerda":
                if (Random.Range(1, 3) == 1)
                {
                    return new Vector2(
                        bounds.center.x - (bounds.extents.x / 2) + 1.25f + ajuste,
                        bounds.center.y + Random.Range((-bounds.extents.y / 2) + 1.5f, -3f) + ajuste2
                    );
                }
                else
                {
                    return new Vector2(
                        bounds.center.x - (bounds.extents.x / 2) + 1.25f + ajuste,
                        bounds.center.y + Random.Range((bounds.extents.y / 2) - 1.5f, 3f) - ajuste2
                    );
                }
            case "Baixo":
                if (Random.Range(1, 3) == 1)
                {
                    return new Vector2(
                        bounds.center.x + Random.Range((-bounds.extents.x / 2) + 1.5f, -3f) + ajuste2,
                        bounds.center.y - (bounds.extents.y / 2) + 0.5f + ajuste
                    );
                }
                else
                {
                    return new Vector2(
                        bounds.center.x + Random.Range((bounds.extents.x / 2) - 1.5f, 3f) - ajuste2,
                        bounds.center.y - (bounds.extents.y / 2) + 0.5f + ajuste
                    ); ;
                }
            case "Total":
                return new Vector2(
                    bounds.center.x + Random.Range((-bounds.extents.x / 2) + 1.5f, (bounds.extents.x / 2) - 1.5f),
                    bounds.center.y + Random.Range((-bounds.extents.y / 2) + 1.5f, (bounds.extents.y / 2) - 1.5f)
                );
            default:
                return new Vector2(0, 0);
        }

    }

    public void SpawnSingleObject(int i, GameObject objectPrefab, Queue<GameObject> objectsQueue, Object.ObjectType type)
    {
        string area = "Total";
        float ajuste = 0f;
        float ajuste2 = 0f;
        int rand = Random.Range(1, 5);
        switch (type)
        {
            case Object.ObjectType.ESPELHO:
                area = "Baixo";
                ajuste = 1.5f;
                break;
            case Object.ObjectType.POSTE:
                if (rand == 1)
                {
                    area = "Cima";
                    ajuste = -0.5f;
                }
                else if (rand == 2)
                {
                    area = "Direita";
                    ajuste = -0.5f;
                    ajuste2 = 0.5f;
                }
                else if (rand == 3)
                {
                    area = "Esquerda";
                    ajuste = 1f;
                }
                else
                {
                    area = "Baixo";
                    ajuste = 2f;
                }
                break;
            case Object.ObjectType.QUADROCAVEIRA:
                area = "Cima";
                ajuste = 0.5f;
                break;
            case Object.ObjectType.QUADROONDA:
                area = "Cima";
                ajuste = 0.5f;
                break;
            case Object.ObjectType.QUADROSAPO:
                area = "Cima";
                ajuste = 0.5f;
                break;
            case Object.ObjectType.TOTEM:
                break;
            case Object.ObjectType.VELA:
                area = "Total";
                break;
            case Object.ObjectType.ABAJUR:
                area = "Cima";
                break;
            case Object.ObjectType.ARMARIO:
                area = "Cima";
                ajuste = 0.25f;
                ajuste2 = 1f;
                break;
            case Object.ObjectType.CAMAHORIZONTAL:
                area = "Esquerda";
                ajuste = 1f;
                ajuste2 = 0.5f;
                break;
            case Object.ObjectType.CAMASOLTEIRO:
                area = "Baixo";
                ajuste = 1.5f;
                break;
            case Object.ObjectType.CAMAVERTICAL:
                area = "Cima";
                ajuste = -0.5f;
                break;
            case Object.ObjectType.CRANIO:
                area = "Cima";
                break;
            case Object.ObjectType.LIVRO:
                area = "Cima";
                break;
            case Object.ObjectType.TV:
                area = "Cima";
                ajuste2 = 1f;
                break;
            default:
                break;
        }

        var position = RandomPointInBoundsArea(roomsBounds[i], area, ajuste, ajuste2);
        GameObject temp = (GameObject)Instantiate(objectPrefab, position, Quaternion.identity);

        bool emptyPosition = false;
        int breakloop = 0;
        while (!emptyPosition)
        {
            emptyPosition = true;
            foreach (var item in objectsQueue)
            {
                if (item.GetComponent<CircleCollider2D>().bounds.Intersects(temp.GetComponent<CircleCollider2D>().bounds))
                {
                    Destroy(temp, 1.0f);
                    position = RandomPointInBoundsArea(roomsBounds[i], area, ajuste, ajuste2);
                    temp = (GameObject)Instantiate(objectPrefab, position, Quaternion.identity);
                    emptyPosition = false;
                    breakloop++;
                    break;
                }
            }
            if (breakloop == 100)
            {
                break;
            }
        }

        temp.name = "Object";
        temp.GetComponent<Object>().updateInfos(type);
        objectsQueue.Enqueue(temp);
    }
    public void DistributeWeapons(Queue<GameObject> gameObjects)
    {
        GameObject[] gameObjectsArray = gameObjects.ToArray();
        int[] withWeapons = new int[System.Enum.GetValues(typeof(Weapon.WeaponType)).Length];
        int i = 0;


        foreach (Weapon.WeaponType weapon in System.Enum.GetValues(typeof(Weapon.WeaponType)))
        {
            int x = 0;

            bool b = true;
            while (b)
            {
                x = Random.Range(0, gameObjectsArray.Length);
                for (int j = 0; j < withWeapons.Length; j++)
                {
                    if (withWeapons[j] == x)
                    {
                        b = true;
                        break;
                    }
                    else
                    {
                        b = false;
                    }
                }
            }

            withWeapons[i] = x;
            i++;
            gameObjectsArray[x].GetComponent<Object>().containsWeapon = true;
            gameObjectsArray[x].GetComponent<Object>().weapon = gameObjectsArray[x].AddComponent<Weapon>();
            gameObjectsArray[x].GetComponent<Weapon>().NewWeapon(weapon);

        }
    }
}