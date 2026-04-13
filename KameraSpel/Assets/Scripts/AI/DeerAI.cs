using UnityEngine;

public class DeerAI : MonoBehaviour
{
    [SerializeField] Animator animator;
    public enum BehaviourStates {fleeing,aware,resting};
    public BehaviourStates currentState;

    void Start()
    {
        Invoke("Eat", Random.Range(0,6f));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Eat()
    {
        animator.SetTrigger("Eat");
    }
}
