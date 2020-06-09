using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    public GameObject player;
    float maxDistance = 5f;
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    public float maxViewAngle = 25f;
    public float minViewAngle = -25f;

    int waypoint = 0;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;

    }
    private void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) <= maxDistance)
        {
            float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
            if (angle >= minViewAngle && angle <= maxViewAngle)
            {
                    agent.isStopped = true;
                    StopCoroutine(Walking());
                    FindObjectOfType<MenuManager>().LosingCanvas();
            }
            else
            {
                agent.isStopped = false;
                StartCoroutine(Walking());
            }
        }
        else
        {
            agent.isStopped = false;
            StartCoroutine(Walking());
        }
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
