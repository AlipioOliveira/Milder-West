using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public static ThirdPersonCamera instacia;

    public Transform targetTransform;

    public float sensitivity = 3f;
    public bool custorVisivle = false;

    private Transform myTransform;
    private Camera cam;

    private bool interacting;

    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float height = 4f;
    private float currX = 0;
    private float currY = 0;

    private Transform npc;   
    private float transitionTime = 1f;
    [Range(0.1f,5f)]
    public float transitionSpeed = 0.3f;
    private float camY = 2f;
    public bool playerDead = false;

    void Start ()
    {
        Cursor.visible = custorVisivle;
        myTransform = transform;        
        cam = Camera.main;
        instacia = this;
    }

    private void Update()
    {
        currX += Input.GetAxisRaw("Mouse X") * sensitivity;
        currY += Input.GetAxisRaw("Mouse Y") * -sensitivity;    

        currY = Mathf.Clamp(currY, -60f, 45f);        
    }

    void LateUpdate ()
    {
        if (!playerDead)
        {
            if (!interacting)
            {
                Vector3 direction = new Vector3(0, height, -distance);
                Quaternion rotation = Quaternion.Euler(currY, currX, 0);

                myTransform.position = targetTransform.position + rotation * direction;
                myTransform.LookAt(targetTransform.position);
            }
            else
            {
                if (transitionTime > 0)
                    transitionTime -= (1f / transitionSpeed) * Time.deltaTime;

                Vector3 newLookPos = npc.transform.position - (npc.transform.position - targetTransform.position) * transitionTime;
                myTransform.LookAt(newLookPos);
            }
        }      
	}

    public void StartInteraction(Transform target)
    {
        npc = target;
        transitionTime = 1f;
        myTransform.LookAt(target.position);
        interacting = true;
    }

    public void StopInteraction()
    {
        interacting = false;
    }
}
