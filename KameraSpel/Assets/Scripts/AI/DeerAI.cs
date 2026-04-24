using UnityEngine;
using UnityEngine.AI;

public class DeerAI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    public enum BehaviourStates {fleeing,aware,resting};
    [SerializeField] BehaviourStates currentState;

    [Range(0f, 100f)]
    [SerializeField] float[] deerStats;
    [Range(0f, 100f)]
    [SerializeField] float Hunger;
    [Range(0f, 100f)]
    [SerializeField] float Thirst;
    [Range(0f, 100f)]
    [SerializeField] float Energy;
    [SerializeField] int statMax;
    [Range(0f, 100f)]
    [SerializeField] float Fear;
    [SerializeField] float hungerDrainSpeed = 0.1f;
    [SerializeField] float thirstDrainSpeed = 0.1f;
    [SerializeField] float energyDrainSpeed = 0.1f;

    [SerializeField] float spookRange = 10f;
    [SerializeField] float fearAmountToFlee = 20f;
    [SerializeField] float spookSpeed = 1f;

    void Start()
    {
        Invoke("Eat", Random.Range(0,6f));
        Hunger = statMax;
        Energy = statMax;
        Thirst = statMax;
    }

    // Update is called once per frame
    void Update()
    {
        DrainStats();
        DistanceToPlayer();
    }

    void Eat()
    {
        animator.SetTrigger("Eat");
    }

    void DistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,player.transform.position);
        if (distanceToPlayer <= spookRange)
        {
            currentState = BehaviourStates.aware;
            Fear += Time.deltaTime * spookSpeed;
            if(Fear >= fearAmountToFlee)
            {
                currentState = BehaviourStates.fleeing;
            }
        }

    }
    void DrainStats()
    {
        Hunger -= Time.deltaTime * hungerDrainSpeed;
        Thirst -= Time.deltaTime * thirstDrainSpeed;
        Energy -= Time.deltaTime * energyDrainSpeed;
    }
}
