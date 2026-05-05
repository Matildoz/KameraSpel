using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SetFocus : MonoBehaviour
{
    [SerializeField] Camera physicalCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameStates gameStates;
    [SerializeField] Volume volume;
    [SerializeField] LayerMask focusLayer;
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
         raycast = new Ray(physicalCamera.transform.position,playerMovement.playerHead.transform.forward *focusDistance);
        if(Physics.Raycast(raycast,out hit, focusDistance,focusLayer))
        {
            hitDistance = Vector3.Distance(transform.position,hit.point);
            hitObject = hit.transform.gameObject;
           // AdjustFocus();
            Debug.DrawLine(physicalCamera.transform.position, hit.point,Color.green);
        }
        else
        {
            Debug.DrawRay(physicalCamera.transform.position,playerMovement.playerHead.transform.forward,Color.red);
        }
    }
    //Vill man fokusera manuellt eller automatiskt?
    public void AdjustFocus()
    {
        dof.focusDistance.value = hitDistance;        
    }
}
