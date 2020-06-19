using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    public GameObject player;
    float maxDistance = 10f;
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    private float maxViewAngle = 35f;
    private float minViewAngle = -35f;

    int waypoint = 0;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;

    }
    private void Update()
    {
        if(!FindObjectOfType<MenuManager>().isLost){
        StartCoroutine(AISeesTarget());
        }
    }

    IEnumerator AISeesTarget()
    {
        if(CopScanArea())
        {
        agent.isStopped = true;
        StopCoroutine(Walking());
        yield return new WaitForSeconds(2f);
        if(CopScanArea())
            {
            FindObjectOfType<MenuManager>().LosingCanvas();
            }
        }
        else if(!CopScanArea())
        {
            agent.isStopped = false;
            StartCoroutine(Walking());
            yield return null;
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
        agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x, transform.position.y, waypoints[waypoint].transform.position.z));
        if (transform.position.x == waypoints[waypoint].transform.position.x && transform.position.z == waypoints[waypoint].transform.position.z)
        {
            waypoint = Random.Range(0, waypoints.Length);
        }
        yield return new WaitForSeconds(2f);
    }

}
