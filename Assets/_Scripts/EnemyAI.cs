using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float chasingRange;
    [SerializeField] private float attackRange;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private PolygonCollider2D flashlight;
    [SerializeField] private CircleCollider2D enemyCollider;

    private SpriteRenderer material;
    
    public Seeker seeker;
    public Rigidbody2D rb;

    public float speed = 200f;
    public float nextWaypointDistance = 0;
    public int currentWaypoint = 0;

    public Vector2 pos;
    public Vector2 newPos;
    public Path path;

    public bool reachedEndOfPath = false;
        
    private bool illuminated;

    private Node topNode;
    private bool updPos = true;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {
        illuminated = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();        
        newPos = new Vector2(rb.position.x + Random.Range(-4, 4), rb.position.y + Random.Range(-4, 4));

        InvokeRepeating("UpdatePath", 0f, .5f);

        ConstructBehaviourTree();
    }

    private void ConstructBehaviourTree()
    {
        IlluminatedNode illuminatedNode = new IlluminatedNode(this, playerTransform, enemyTransform);
        RunNode runNode = new RunNode(this);
        PatrolNode patrolNode = new PatrolNode(this);
        RangeNode rangeNode = new RangeNode(chasingRange, this, playerTransform, enemyTransform);
        ApproachNode approachNode = new ApproachNode(this, playerTransform);
        CloserNode closerNode = new CloserNode(attackRange, playerTransform, enemyTransform);
        LoadNode loadNode = new LoadNode(this, playerTransform);
        AttackNode attackNode = new AttackNode(this, playerTransform);

        Succeeder attackSucceeder = new Succeeder(attackNode);
        Sequence attackSequence = new Sequence(new List<Node> {closerNode, loadNode, attackSucceeder});
        Sequence approachSequence = new Sequence(new List<Node> {approachNode, attackSequence});
        Sequence rangeSequence = new Sequence(new List<Node> {rangeNode, approachSequence});

        Sequence runSequence = new Sequence(new List<Node> {illuminatedNode, runNode});

        topNode = new Selector(new List<Node> {runSequence, rangeSequence, patrolNode});
    }

    void Update()
    {
        if(updPos){
            newPos = new Vector2(rb.position.x + Random.Range(-4, 4), rb.position.y + Random.Range(-4, 4));
            updPos = false;
        }

        topNode.Evaluate();
        if(topNode.nodeState == NodeState.FAILURE)
        {
            SetColor(Color.magenta);
        }
        
    }
    
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            pos = newPos;
            seeker.StartPath(rb.position, pos, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public bool isIlluminated()
    {
        return illuminated;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other is PolygonCollider2D){
            illuminated = true;
            //Debug.Log("Iluminado");
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other is PolygonCollider2D){
            illuminated = false;
            //Debug.Log("Não Iluminado");
        }
            
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }
}
