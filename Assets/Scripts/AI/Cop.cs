using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] waypoints;

    int waypoint=0;

    private void Update() {
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        agent.SetDestination(new Vector3(waypoints[waypoint].transform.position.x,1,waypoints[waypoint].transform.position.z));
        if(this.transform.position==agent.destination)
        {
            waypoint = Random.Range(0,waypoints.Length);
        }
        yield return new WaitForSeconds(2f);
    }
}
