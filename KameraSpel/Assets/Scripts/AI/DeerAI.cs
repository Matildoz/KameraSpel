using UnityEngine;

public class DeerAI : MonoBehaviour
{
    [SerializeField] Animator animator;
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
   
    [SerializeField] float hungerDrainSpeed;
 
    void Start()
    {
        Invoke("Eat", Random.Range(0,6f));
        Hunger = statMax;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseFood();
    }

    void Eat()
    {
        animator.SetTrigger("Eat");
    }

    void DecreaseFood()
    {
        Hunger -= Time.deltaTime * hungerDrainSpeed;

    }
}
