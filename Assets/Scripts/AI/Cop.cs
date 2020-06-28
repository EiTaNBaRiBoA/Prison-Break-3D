using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    public GameObject player;
    float maxDistance = 10f;
    public NavMeshAgent agent;
    public GameObject wanderMap;
    private float maxViewAngle = 45f;
    private float minViewAngle = -45f;
    private float timeAgentConfirm = 2f;
    private Transform[] waypoints;
    private Animator animator;

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
        if (!FindObjectOfType<MenuManager>().isLost&&agent.speed>0)
        {
            StartCoroutine(AISeesTarget());
        }
        else if (agent.speed<=0)
        {
            StartCoroutine(NavMeshStopped());
            animator.SetBool("isWalking",false);
        }
    }

    IEnumerator NavMeshStopped()
    {
            agent.enabled = false;
            agent.enabled = true;
            yield return new WaitForSeconds(timeAgentConfirm);
    }

    IEnumerator AISeesTarget()
    {
        if (CopScanArea() || Vector3.Distance(player.transform.position, this.transform.position) <= maxDistance && !FindObjectOfType<PlayerMovement>().isCrouch)
        {
            agent.ResetPath();
            StopCoroutine(Walking());
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
                        ConfirmTarget(); // going towards targets to confirm with red eyes

                        yield return new WaitForSeconds(timeAgentConfirm);
                        if (CopScanArea())
                        {
                            FindObjectOfType<MenuManager>().LosingCanvas();
                        }

                    }
                }
                else
                {
                    StartCoroutine(Walking());
                }
            }
            else if (CopScanArea())
            {
                ConfirmTarget(); // going towards targets to confirm with red eyes

                yield return new WaitForSeconds(timeAgentConfirm);
                if (CopScanArea())
                {
                    FindObjectOfType<MenuManager>().LosingCanvas();
                }
            }


        }
        else
        {
            StartCoroutine(Walking());
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
    IEnumerator Walking()
    {
        agent.stoppingDistance = 0;
        agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
        animator.SetBool("isWalking",true);
        if (transform.position.x == waypoints[waypoint].transform.position.x && transform.position.z == waypoints[waypoint].transform.position.z)
        {
            agent.ResetPath();
            waypoint = Random.Range(0, waypoints.Length);
            agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
            animator.SetBool("isWalking",true);
        }
        yield return new WaitForSeconds(timeAgentConfirm);
    }

    public void ConfirmTarget()
    {
        agent.stoppingDistance = 2;
        agent.SetDestination(player.transform.position);
    }

}
