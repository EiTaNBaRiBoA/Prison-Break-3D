using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopTwo : MonoBehaviour
{
    public GameObject player;
    float maxDistance = 10f;
    public NavMeshAgent agent;
    public GameObject wanderMap;
    private float maxViewAngle = 45f;
    private float minViewAngle = -45f;
    private float timeAgentConfirm = 3f;
    private Transform[] waypoints;
    private Animator animator;
    public AudioClip[] copSeesYou;
    public int walkingDistance;

    int waypoint = 0;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isWalking",false);
        waypoints = wanderMap.GetComponentsInChildren<Transform>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
        player = FindObjectOfType<PlayerMovement>().gameObject;

    }
    private void Update()
    {
        if (!FindObjectOfType<MenuManager>().isLost)
        {
            StartCoroutine(AISeesTarget());
        }
        else if (agent.speed<=0)
        {
            NavMeshStopped();
            animator.SetBool("isWalking",false);
        }
    }

    void NavMeshStopped()
    {
            agent.enabled = false;
            agent.enabled = true;
            
    }

    IEnumerator AISeesTarget()
    {
        if (CopScanArea() || Vector3.Distance(player.transform.position, this.transform.position) <= maxDistance && !FindObjectOfType<PlayerMovement>().isCrouch)
        {
            agent.ResetPath();
            Ray ray = new Ray(transform.position, player.transform.position - transform.position);
            RaycastHit playerCheck;
            if (Physics.Raycast(ray, out playerCheck, maxDistance))
            {
                if (playerCheck.transform.CompareTag("Player"))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                     Quaternion.LookRotation(player.transform.position - transform.position),
                      timeAgentConfirm * Time.deltaTime);
                    if (Vector3.Angle(transform.forward, player.transform.position - transform.position) <= 35 && CopScanArea())
                    {
                        StartCoroutine(SpeakToThePlayer());
                        ConfirmTarget();

                        yield return new WaitForSeconds(timeAgentConfirm);
                        if (CopScanArea())
                        {
                            FindObjectOfType<MenuManager>().LosingCanvas();
                        }

                    }
                }
                else
                {
                    Walking();
                }
            }
            else if (CopScanArea())
            {
                ConfirmTarget(); 

                yield return new WaitForSeconds(timeAgentConfirm);
                if (CopScanArea())
                {
                    FindObjectOfType<MenuManager>().LosingCanvas();
                }
                else
                {
                    NavMeshStopped();
                    Walking();
                }
            }


        }
        else
        {
            Walking();
        }
        yield return null;
    }
    private bool CopScanArea()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) <= maxDistance)
        {
            float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
            if (angle >= minViewAngle && angle <= maxViewAngle)
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, player.transform.position - transform.position);
                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        return false;
    }
    void Walking()
    {
        agent.stoppingDistance = walkingDistance;
        //agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
        animator.SetBool("isWalking",true);
        if (transform.position.x == waypoints[waypoint].transform.position.x && transform.position.z == waypoints[waypoint].transform.position.z || agent.remainingDistance<=1)
        {
            waypoint = Random.Range(0, waypoints.Length);
            agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
            animator.SetBool("isWalking",true);
        }
    }

    public void ConfirmTarget()
    {
        agent.stoppingDistance = 1;
        agent.SetDestination(player.transform.position);
    }

    IEnumerator SpeakToThePlayer()
    {
        GetComponent<CopSFX>().NarrativeCop(copSeesYou);
        yield return new WaitForSeconds(timeAgentConfirm+2);
    }
}