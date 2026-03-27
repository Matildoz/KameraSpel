using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SetFocus : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameStates gameStates;
    [SerializeField] Volume volume;
    public float focusDistance;
    Ray raycast;
    float hitDistance;
    RaycastHit hit;
    DepthOfField dof;
     GameObject hitObject;
    void Start()
    {
       volume.profile.TryGet(out dof);
        
    }

    // Update is called once per frame
    void Update()
    {
         raycast = new Ray(transform.position,playerMovement.playerHead.transform.forward *focusDistance);
        if(Physics.Raycast(raycast,out hit, focusDistance))
        {
            hitDistance = Vector3.Distance(transform.position,hit.point);
            hitObject = hit.transform.gameObject;
            AdjustFocus();
        }
    }

    public void AdjustFocus()
    {
        dof.focusDistance.value = hitDistance;
        
    }
}
