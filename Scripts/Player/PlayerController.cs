using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask layer;

    private UnityEngine.AI.NavMeshAgent agent;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    public GameObject enemy;

    bool clickedEnemy;

    public float direction;
    public float speed;
    private Animator anim;
    // Use this for initialization
    void Start ()
    {
        clickedEnemy = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            ClickToMove();

        agent.updatePosition = false;
        agent.updateRotation = false;

        direction = Vector3.Angle(transform.forward, agent.desiredVelocity) *Mathf.Sign(Vector3.Dot(agent.desiredVelocity, transform.right));
        //Debug.DrawLine(transform.forward, agent.desiredVelocity, Color.green, 10000);
        speed = agent.desiredVelocity.magnitude;

            anim.SetFloat("Speed", speed, 0, Time.deltaTime);
            anim.SetFloat("Direction", direction, 0, Time.deltaTime);



        agent.nextPosition = transform.position;
        if(enemy != null)
        {
            CheckInRange();
        }

    }

    void CheckInRange()
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance <= (attackRange + 0.5f))
        {
            anim.SetBool("Attacking", true);
        }

    }

    void ClickToMove()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,layer))
        {
            if(hit.transform.tag == "Enemy")
            {
                agent.stoppingDistance = attackRange;
                enemy = hit.transform.gameObject;
                enemy.GetComponent<EnemyStats>().EnableDisableHealthBar(true);

            }
            else
            {
                agent.stoppingDistance = 0.3f;
                anim.SetBool("Attacking", false);
                if(enemy != null)
                {
                    Debug.Log(enemy.name);
                    enemy.GetComponent<EnemyStats>().EnableDisableHealthBar(false);
                }
                enemy = null;

            }
            agent.destination = hit.point;
            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "LevelStreamer")
        {
            StartCoroutine(StreamingLevel());
        }
    }

    private IEnumerator StreamingLevel()
    {
        AsyncOperation async = Application.LoadLevelAdditiveAsync("InGame2");
        yield return async;
    }
}
