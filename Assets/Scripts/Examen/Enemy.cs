using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int lifeEnemy = 100;
    public Transform Player;
    private NavMeshAgent agent;
    public int count = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
   { 
        if (Player != null)
        {
            agent.SetDestination(Player.position);

        }
    }
    public void HasPath()
    {
        print(agent.hasPath);
    }

}