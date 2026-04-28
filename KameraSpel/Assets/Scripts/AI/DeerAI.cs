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
    [SerializeField] float Hunger;
    [SerializeField] float hungerToEat;
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
    [SerializeField] float spookRaiseSpeed = 1f;

    void Start()
    {
       // Invoke("Eat", Random.Range(0,6f));
        Hunger = statMax;
        Energy = statMax;
        Thirst = statMax;
    }

    // Update is called once per frame
    void Update()
    {
        DrainStats();

        if(DistanceToPlayer() <= spookRange)
        {
            currentState = BehaviourStates.aware;
            Fear += Time.deltaTime * spookRaiseSpeed;      
            if (Fear >= fearAmountToFlee)
            {
                Flee();
            }
        }
        else
        {
            Fear -= Time.deltaTime * spookRaiseSpeed;
            //Check needs
        }

    }

    void Eat()
    {
        animator.SetTrigger("Eat");
    }

    float DistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,player.transform.position);      
        return distanceToPlayer;
    }
    void Flee()
    {
        currentState = BehaviourStates.fleeing;
    }
    void SetNewPosition()
    {

    }
    void DrainStats()
    {
        Hunger -= Time.deltaTime * hungerDrainSpeed;
        Thirst -= Time.deltaTime * thirstDrainSpeed;
        Energy -= Time.deltaTime * energyDrainSpeed;
    }
}
