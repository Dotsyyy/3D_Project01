using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointMover : MonoBehaviour
{
    // Stores a reference to the waypoint system this object will use
    [SerializeField] private Waypoints waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [Range(0f,15f)] // How fast the agent will rotate once it reaches theri waypoint.

    [SerializeField] private float rotateSpeed = 4f;

    [SerializeField] private float distanceThreshold = 0.1f;

    private Transform currentWayPoint;

    // The rotation target for the current frame.
    private Quaternion rotationGoal;
    // The direction to the next waypoint that the agent needs to rotate towards
    private Vector3 directionToWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        // set initial position to the first waypoint
        currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
        transform.position = currentWayPoint.position;

        // Set the next wayPoint target

        currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
        transform.LookAt(currentWayPoint);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, moveSpeed* Time.deltaTime);
        if(Vector3.Distance(transform.position, currentWayPoint.position)< distanceThreshold)
        {
            currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
            //transform.LookAt(currentWayPoint);

        }
        RotateTowardsWaypoint();
    }

    // Will slowly rotate towards the curren waypoint its moving towards.

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (currentWayPoint.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotateSpeed * Time.deltaTime);
    }
}

