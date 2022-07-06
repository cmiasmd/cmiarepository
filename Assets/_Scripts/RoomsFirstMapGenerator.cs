using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;
using Pathfinding;

public class RoomsFirstMapGenerator : SimpleRandomMapGenerator
{
    [SerializeField]
    public GameConfig gameConfig = null;
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int mapWidth = 20, mapHeight = 20;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private GameObject lostGhostPrefab = null;
    [SerializeField]
    private GameObject enemyGhostPrefab = null;
    [SerializeField]
    private GameObject commonGhostPrefab = null;
    [SerializeField]
    private GameObject objectPrefab = null;

    public Queue<GameObject> commomGhosts = null;
    public Queue<GameObject> objectsQueue = null;

    public List<Vector2Int> roomCentersList = null;

    public Bounds[] roomsBounds;

    private string conjunto;
    private int scanOnce = 0;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
        // ClearRooms();
        //SpawnObjects();
    }

    private void Awake()
    {
        ClearRooms();
        RunProceduralGeneration();
        while (gameConfig.roomFloorList.Count != 5)
        {
            ClearRooms();
            RunProceduralGeneration();
        }

        commomGhosts = new Queue<GameObject>();
        SpawnGhosts();

        objectsQueue = new Queue<GameObject>();
        SpawnObjectsCommonRoom();


        scanOnce = 0;
        
    }

    private void Update()
    {
        if (scanOnce < 2)
        {
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            scanOnce++;
        }
    }

    private void CreateRooms()
    {

        tilemapVisualizer.Clear();

        var roomsList = GeradorProcedural.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(mapWidth, mapHeight, 0)), minRoomWidth, minRoomHeight);

        var floor = CreateSimpleRooms(roomsList);        

        var WallPositions = WallGenerator.FindWallsInDirections(floor, Direction2D.cardinalDirectionsList);
        WallPositions.UnionWith(WallGenerator.FindWallsInDirections(floor, Direction2D.diagonalDirectionsList));

        var floorList = CreateSimpleRoomsList(roomsList, WallPositions);
        gameConfig.roomFloorList = floorList;

        var roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        roomsBounds = new Bounds[roomCenters.Count];
        for (int i = 0; i < roomCenters.Count; i++)
        {
            int boundsX = 0;
            int boundsY = 0;

            Vector2Int positionTemp = roomCenters[i];
            while (floor.Contains(positionTemp))
            {
                positionTemp.x += 1;
                boundsX ++;
            }

            positionTemp = roomCenters[i];
            while (floor.Contains(positionTemp))
            {
                positionTemp.y += 1;
                boundsY++;
            }

            roomsBounds[i] = new Bounds( new Vector3(roomCenters[i].x, roomCenters[i].y), new Vector3(boundsX*4 , boundsY*4));

            //GameObject rect = new GameObject();
            //rect.transform.position = roomsBounds[i].center;
            //rect.transform.localScale = roomsBounds[i].extents;
        }

        gameConfig.roomsBounds = roomsBounds;

        var corridors = ConnectRooms(roomCenters);
        corridors.ExceptWith(floor);
        
        tilemapVisualizer.PaintCorridorTiles(corridors);
        tilemapVisualizer.PaintFloorTiles(floor);
        floor.UnionWith(corridors);
        //tilemapVisualizer.PaintFloorTilesList(floorList);

        WallGenerator.CreateWalls(floor, tilemapVisualizer);

        

        player.transform.position = new Vector2(roomsBounds[0].center.x, roomsBounds[0].center.y);
        enemyGhostPrefab.transform.position = new Vector2(roomsBounds[0].center.x + 3, roomsBounds[0].center.y + 2);
        lostGhostPrefab.GetComponent<LostGhost>().type = gameConfig.lostGhostType;
        lostGhostPrefab.transform.position = new Vector2(roomsBounds[1].center.x, roomsBounds[1].center.y);
    }

    public void SpawnGhosts()
    {
        string[] typeGhosts = { "fantasma1", "fantasma2", "fantasma3", "fantasma4", "fantasma5" };
        string[] weapons = { "arma1", "arma2", "arma3", "arma4", "arma5" };
        string[] conjuntos = { "conjunto1", "conjunto2", "conjunto3", "conjunto4", "conjunto5" };

        string assassin = typeGhosts[Random.Range(0, typeGhosts.Length)];
        string weapon = weapons[Random.Range(0, weapons.Length)];
        conjunto = conjuntos[Random.Range(0, conjuntos.Length)];
        gameConfig.weapon = weapon;
        gameConfig.conjunto = conjunto;

        string fraseAssassinoArma = "";
        Queue<string> fraseComumArma = new Queue<string>();

        switch (weapon)
        {
            case "arma1": //Pistola Com Silenciador
                fraseAssassinoArma = "Vi de relance o que pode ter sido a arma do crime, e com certeza n�o era de metal ";
                fraseComumArma.Enqueue("N�o se foi escutado nenhum barulho na hora do crime.");
                fraseComumArma.Enqueue("A v�tima morreu com certeza por causa de um disparo");
                fraseComumArma.Enqueue("Com toda certeza n�o foi uma assassinato por asfixia ");
                fraseComumArma.Enqueue("N�o foi nenhum tipo de envenenamento");
                break;
            case "arma2": //Serrote
                fraseAssassinoArma = "N�o se foi escutado nenhum barulho na hora do crime.";
                fraseComumArma.Enqueue("Vi naquela noite o reflexo de algo met�lico na m�o do assassino");
                fraseComumArma.Enqueue("A v�tima morreu com certeza por causa daquele do ferimento grave");
                fraseComumArma.Enqueue("Com toda certeza n�o foi uma assassinato por asfixia ");
                fraseComumArma.Enqueue("N�o foi nenhum tipo de envenenamento");
                break;
            case "arma3": //Veneno Liquido
                fraseAssassinoArma = "A v�tima morreu com certeza por causa daquele do ferimento grave.";
                fraseComumArma.Enqueue("N�o se foi escutado nenhum barulho na hora do crime");
                fraseComumArma.Enqueue("Vi de relance o que pode ter sido a arma do crime, e com certeza n�o era de metal ");
                fraseComumArma.Enqueue("Com toda certeza n�o foi uma assassinato por asfixia");
                fraseComumArma.Enqueue("Eu conhe�o um envenenamento de longe, e com certeza esse � um caso sobre!");
                break;
            case "arma4": //Travesseiro
                fraseAssassinoArma = "Vi naquela noite o reflexo de algo met�lico na m�o do assassino";
                fraseComumArma.Enqueue("Ouvi dizer que foi algo muito barulhento");
                fraseComumArma.Enqueue("Vi a v�tima, mas n�o consegui enxegar nenhum ferimento aparente");
                fraseComumArma.Enqueue("Pelo que eu ouvi, a v�tima morreu por falta de oxig�nio");
                fraseComumArma.Enqueue("N�o foi nenhum tipo de envenenamento");
                break;
            case "arma5": //Revolver
                fraseAssassinoArma = "Vi de relance o que pode ter sido a arma do crime, e com certeza n�o era de metal ";
                fraseComumArma.Enqueue("Ouvi dizer que foi algo muito barulhento");
                fraseComumArma.Enqueue("A v�tima morreu com certeza por causa de um disparo");
                fraseComumArma.Enqueue("Com toda certeza n�o foi uma assassinato por asfixia ");
                fraseComumArma.Enqueue("N�o foi nenhum tipo de envenenamento");
                break;
            default:
                break;
        }

        string fraseAssassinoRoom = "";
        Queue<string> fraseComumRoom = new Queue<string>();

        switch (conjunto)
        {
            case "conjunto1":
                fraseAssassinoRoom = "Tinha um quadro no local que me lembrou algo que vi no museu";
                fraseComumRoom.Enqueue("Tinha algo em cima da c�moda");
                fraseComumRoom.Enqueue("Tinha algo para iluminar o quarto");
                fraseComumRoom.Enqueue("Tinha um quadro meio macabro no local");
                break;
            case "conjunto2":
                fraseAssassinoRoom = "Tinha algo em cima da c�moda";
                fraseComumRoom.Enqueue("Com certeza eu lembro de ter visto uma planta em algum lugar do quarto");
                fraseComumRoom.Enqueue("Tinha algo macabro em algum lugar do quarto ");
                fraseComumRoom.Enqueue("Acho que n�o tinha nada em cima da c�moda");
                break;
            case "conjunto3":
                fraseAssassinoRoom = "Tinha algo que eu lembro de ter visto em um museu, em cima da c�moda";
                fraseComumRoom.Enqueue("Tinha algo macabro em algum lugar do quarto ");
                fraseComumRoom.Enqueue("Tinha um quadro no local ");
                fraseComumRoom.Enqueue("Tinha algo em cima da c�moda que servia para iluminar o quarto");
                break;
            case "conjunto4":
                fraseAssassinoRoom = "Tinha uma planta em cima da c�moda";
                fraseComumRoom.Enqueue("Tinha um quadro no local ");
                fraseComumRoom.Enqueue("Tinha algo para iluminar o quarto");
                fraseComumRoom.Enqueue("Tinha algo macabro em cima da c�moda");
                break;
            case "conjunto5":
                fraseAssassinoRoom = "Tinha um quadro no local que me lembrou algum tipo de animal";
                fraseComumRoom.Enqueue("Tinha algo em cima da c�moda");
                fraseComumRoom.Enqueue("Com certeza eu lembro de ter visto uma planta em algum lugar do quarto");
                fraseComumRoom.Enqueue("Tinha algo macabro que me lembrou algum tipo de animal ");
                break;
            default:
                break;
        }

        fraseComumRoom.Enqueue("Se n�o me engano, o n�mero do quarto era... Qual era o n�mero mesmo? ...");

        HashSet<int> x = new HashSet<int>();
        for (int i = 0; i < roomsBounds.Length; i++)
        {
            var position = gameConfig.RandomPointInBoundsCenter(roomsBounds[i]);

            GameObject temp = (GameObject)Instantiate(commonGhostPrefab, position, Quaternion.identity);
            temp.name = "CommonGhost";

            int rand = Random.Range(0, 5);
            while (x.Contains(rand))
                rand = Random.Range(0, 5);

            temp.GetComponentInChildren<CommonGhost>().type = typeGhosts[rand];
            temp.GetComponentInChildren<CommonGhost>().weapon = weapon;
            temp.GetComponentInChildren<CommonGhost>().conjunto = conjunto;

            temp.GetComponentInChildren<CommonGhost>().assassinType = assassin;
            if (typeGhosts[rand] == assassin)
            {
                gameConfig.assassin = assassin;
                temp.GetComponentInChildren<CommonGhost>().assasin = true;
                temp.GetComponentInChildren<CommonGhost>().fraseArma = fraseAssassinoArma;
                temp.GetComponentInChildren<CommonGhost>().fraseRoom = fraseAssassinoRoom;
            }
            else
            {
                temp.GetComponentInChildren<CommonGhost>().fraseArma = fraseComumArma.Dequeue();
                temp.GetComponentInChildren<CommonGhost>().fraseRoom = fraseComumRoom.Dequeue();
            }

            x.Add(rand);

            commomGhosts.Enqueue(temp);
        }
    }


    public void SpawnObjectsCommonRoom()
    {
        switch (conjunto)
        {
            case "conjunto1":
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.COMODAPLANTA);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.QUADROCAVEIRA);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.VELA);
                break;
            case "conjunto2":
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.COMODAVAZIA);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.PLANTA1);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.ESTATUA);
                break;
            case "conjunto3":
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.ABAJUR);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.CAVEIRA);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.QUADROONDA);
                break;
            case "conjunto4":
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.CRANIO);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.POSTE);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.QUADROSAPO);
                break;
            case "conjunto5":
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.LIVRO);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.PLANTA2);
                gameConfig.SpawnSingleObject(4, objectPrefab, objectsQueue, Object.ObjectType.TOTEM);
                break;
            default:
                break;
        }
        for (int i = 0; i < roomsBounds.Length; i++)
        {
            gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.TV);
            int rand = Random.Range(1, 5);
            if(rand == 1)
                gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.CAMAHORIZONTAL);
            else if(rand == 2)
                gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.CAMAVERTICAL);
            else if(rand == 3)
                gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.CAMASOLTEIRO);
            else
            {
                gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.CAMASOLTEIRO);
                gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.CAMASOLTEIRO);
            }

            gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.ARMARIO);
            gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, Object.ObjectType.ESPELHO);
        }

        for (int i = 0; i < roomsBounds.Length - 1; i++)
        {
            switch (conjunto)
            {
                case "conjunto1":
                    Object.ObjectType[] objectTypesRoom = { Object.ObjectType.COMODAVAZIA, Object.ObjectType.PLANTA1, Object.ObjectType.ESTATUA, Object.ObjectType.ABAJUR, Object.ObjectType.CAVEIRA,
                    Object.ObjectType.QUADROONDA, Object.ObjectType.CRANIO, Object.ObjectType.POSTE, Object.ObjectType.QUADROSAPO, Object.ObjectType.LIVRO, Object.ObjectType.PLANTA2, Object.ObjectType.TOTEM};

                    for (int j = 0; j < 3; j++)
                    {
                        gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, objectTypesRoom[Random.Range(0, objectTypesRoom.Length)]);
                    }

                    break;
                case "conjunto2":
                    Object.ObjectType[] objectTypesRoom2 = { Object.ObjectType.COMODAPLANTA, Object.ObjectType.QUADROCAVEIRA, Object.ObjectType.VELA, Object.ObjectType.ABAJUR, Object.ObjectType.CAVEIRA,
                    Object.ObjectType.QUADROONDA, Object.ObjectType.CRANIO, Object.ObjectType.POSTE, Object.ObjectType.QUADROSAPO, Object.ObjectType.LIVRO, Object.ObjectType.PLANTA2, Object.ObjectType.TOTEM};

                    for (int j = 0; j < 3; j++)
                    {
                        gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, objectTypesRoom2[Random.Range(0, objectTypesRoom2.Length)]);
                    }

                    break;
                case "conjunto3":
                    Object.ObjectType[] objectTypesRoom3 = { Object.ObjectType.COMODAPLANTA, Object.ObjectType.QUADROCAVEIRA, Object.ObjectType.VELA, Object.ObjectType.COMODAVAZIA, Object.ObjectType.PLANTA1,
                    Object.ObjectType.ESTATUA, Object.ObjectType.CRANIO, Object.ObjectType.POSTE, Object.ObjectType.QUADROSAPO, Object.ObjectType.LIVRO, Object.ObjectType.PLANTA2, Object.ObjectType.TOTEM};

                    for (int j = 0; j < 3; j++)
                    {
                        gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, objectTypesRoom3[Random.Range(0, objectTypesRoom3.Length)]);
                    }

                    break;
                case "conjunto4":
                    Object.ObjectType[] objectTypesRoom4 = { Object.ObjectType.COMODAPLANTA, Object.ObjectType.QUADROCAVEIRA, Object.ObjectType.VELA, Object.ObjectType.ABAJUR, Object.ObjectType.CAVEIRA,
                    Object.ObjectType.QUADROONDA, Object.ObjectType.COMODAVAZIA, Object.ObjectType.PLANTA1, Object.ObjectType.ESTATUA, Object.ObjectType.LIVRO, Object.ObjectType.PLANTA2, Object.ObjectType.TOTEM};

                    for (int j = 0; j < 3; j++)
                    {
                        gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, objectTypesRoom4[Random.Range(0, objectTypesRoom4.Length)]);
                    }

                    break;
                case "conjunto5":
                    Object.ObjectType[] objectTypesRoom5 = { Object.ObjectType.COMODAPLANTA, Object.ObjectType.QUADROCAVEIRA, Object.ObjectType.VELA, Object.ObjectType.ABAJUR, Object.ObjectType.CAVEIRA,
                    Object.ObjectType.QUADROONDA, Object.ObjectType.CRANIO, Object.ObjectType.POSTE, Object.ObjectType.QUADROSAPO, Object.ObjectType.COMODAVAZIA, Object.ObjectType.PLANTA1, Object.ObjectType.ESTATUA};

                    for (int j = 0; j < 3; j++)
                    {
                        gameConfig.SpawnSingleObject(i, objectPrefab, objectsQueue, objectTypesRoom5[Random.Range(0, objectTypesRoom5.Length)]);
                    }

                    break;
                default:
                    break;
            }
        }

        gameConfig.gameObjectsQueue = objectsQueue;
    }

    

    private void ClearRooms()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (var ghost in ghosts)
            DestroyImmediate(ghost);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Obj");
        foreach (var obj in objs)
            DestroyImmediate(obj);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];

        roomCentersList = new List<Vector2Int>();
        roomCentersList.Add(currentRoomCenter);   
        
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;

            roomCentersList.Add(currentRoomCenter);

            corridors.UnionWith(newCorridor);
        }
        
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();

        var position = currentRoomCenter;

        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            } else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
            corridor.Add(position + Vector2Int.left);
            //corridor.Add(position + Vector2Int.right);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
            corridor.Add(position + Vector2Int.up);
            //corridor.Add(position + Vector2Int.down);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }

    private List<HashSet<Vector2Int>> CreateSimpleRoomsList(List<BoundsInt> roomsList, HashSet<Vector2Int> WallPositions)
    {
        List<HashSet<Vector2Int>> roomFloorList = new List<HashSet<Vector2Int>>();
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomsList) 
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
            floor.ExceptWith(WallPositions);
            roomFloorList.Add(floor);
        }

        return roomFloorList;
    }
}
