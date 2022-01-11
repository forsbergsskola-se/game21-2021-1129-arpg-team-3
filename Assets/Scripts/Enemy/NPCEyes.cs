
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum Seeing
{
    Nothing,
    Player,
    Enemy,
    NPC
}

public class NPCEyes : MonoBehaviour
{
    public Seeing Seeing { get; private set; }
    public Transform PlayerTarget = null;
    
    public float maxDistance = 10f;
    private Vector3 playerDestination;
    [Range(0f, 360f)]
    public float angle = 45f;
    
    // [SerializeField] bool visualize = true;
    
    public bool targetIsVisible { get; private set; }


    public delegate void PlayerSeenEvent();

    public static event PlayerSeenEvent OnPlayerSeen;


    private void PlayerSeen()
    {
        if (OnPlayerSeen != null)
        {
            OnPlayerSeen();
        }
    }
    
    void Start()
    {
        PlayerTarget = GameObject.FindWithTag("Player").transform;
    }

  
    void Update()
    { 
       CheckVisibility();
       
       // if (Seeing == Seeing.Player)
       // {
       //  // GetComponentInChildren<NPCMovement>().relevantTransform = PlayerTarget;
       //  // GetComponent<NPCMovement>().SetADestination(playerDestination);
       // }
    }
    
    public bool CheckVisibilityToPoint(Vector3 worldPoint) 
    {
        
        var directionToTarget = worldPoint - transform.position;
        
        var degreesToTarget =
            Vector3.Angle(transform.forward, directionToTarget);
        
        var withinArc = degreesToTarget < (angle / 2);

        if (withinArc == false)
        {
            return false;
        }
        
        var distanceToTarget = directionToTarget.magnitude;
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
        var ray = new Ray(transform.position, directionToTarget);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance)) 
        {
            if (hit.collider.CompareTag("Player")) 
            {
                return true;
            }
        } 
        return false;
    }
    
     public bool CheckVisibility()
     {
         var directionToTarget = PlayerTarget.position - transform.position;

         var degreesToTarget =
             Vector3.Angle(transform.forward, directionToTarget);

         var withinArc = degreesToTarget < (angle / 2);

         if (!withinArc)
         {
             Seeing = Seeing.Nothing;
             return false;
         }

         var distanceToTarget = directionToTarget.magnitude;
         var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
         var ray = new Ray(transform.position, directionToTarget);
         RaycastHit hit;
         var canSee = false;
         
         if (Physics.Raycast(ray, out hit, rayDistance))
         {
             if (hit.collider.gameObject.CompareTag("Player"))
             {
                 Seeing = Seeing.Player;
                 playerDestination = hit.transform.position;
                 GetComponent<AI>().player = hit.transform;
             }

             Debug.DrawLine(transform.position, hit.point);
         }
         else 
         {
             Seeing = Seeing.Nothing;
             Debug.DrawRay(transform.position, directionToTarget.normalized * rayDistance);
         }
         return canSee;
     }
     
     
#if UNITY_EDITOR
[CustomEditor(typeof(NPCEyes))]
public class EnemyVisibilityEditor : Editor 
{
    private void OnSceneGUI()
    {
        var visibility = target as NPCEyes;
        
        Handles.color = new Color(1, 1, 1, 0.1f);

        var forwardPointMinusHalfAngle =
            Quaternion.Euler(0, -visibility.angle / 2, 0)
            * visibility.transform.forward;

        Vector3 arcStart =
            forwardPointMinusHalfAngle * visibility.maxDistance;

        Handles.DrawSolidArc(
            visibility.transform.position,
            Vector3.up,                    
            arcStart,                      
            visibility.angle,            
            visibility.maxDistance        
        );
        
        Handles.color = Color.white;
        
        Vector3 handlePosition =
            visibility.transform.position +
                  visibility.transform.forward * visibility.maxDistance;
        
        visibility.maxDistance = Handles.ScaleValueHandle(
            visibility.maxDistance,
            handlePosition,                
            visibility.transform.rotation,  
            1,                             
            Handles.ConeHandleCap,         
            0.25f);
    }
}
#endif
}
