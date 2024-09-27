using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Transform statusBar;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody rbBody;
    [SerializeField] private float durationRotateAtTurn;
    [SerializeField] private float velocity;
    [SerializeField] private float multiplyAxis = 1f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private bool gameState;
    [SerializeField] private bool MoveByTouch;
    [SerializeField] private bool canMove;
    private string currentDirection;
    private Vector3 defaultRotatePlayer;
    private Vector3 directionToMove;
    private Vector3 direction;

    private void Start()
    {
        rbBody = GetComponent<Rigidbody>();
        RotateOnTurns("forward");
    }

    private void OnEnable() 
    {
        Player.OnFinish += Stop;        
        Player.OnLose += Stop;        
        UIManager.OnStartGame += ChangeGameState;
    }

    private void OnDisable()
    {
        Player.OnFinish -= Stop;  
        Player.OnLose -= Stop;             
        UIManager.OnStartGame -= ChangeGameState;
    }

    private void ChangeGameState()
    {
        gameState = true;
        canMove = true;
        rbBody.velocity = Vector3.zero;
    }

    private void Stop(float info)
    {
        gameState = false;
        canMove = false;
        rbBody.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (gameState)
        {
            if(canMove)
            {
                if(currentDirection == "forward")
                {
                    transform.position +=  Vector3.forward * runSpeed * Time.deltaTime;
                }
                if(currentDirection == "right")
                {
                    transform.position +=  Vector3.right * runSpeed * Time.deltaTime;
                }
                if(currentDirection == "left")
                {
                    transform.position +=  Vector3.left * runSpeed * Time.deltaTime;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                MoveByTouch = true;
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                MoveByTouch = false;
            }
        
            if (MoveByTouch)
            { 
                if(currentDirection == "forward")
                {
                    direction =  directionToMove * Mathf.Lerp(direction.x, Input.GetAxis("Mouse X") * multiplyAxis, Time.deltaTime * smoothSpeed);
                }
                if(currentDirection == "right")
                {
                    direction =  directionToMove * Mathf.Lerp(direction.z, -Input.GetAxis("Mouse X") * multiplyAxis, Time.deltaTime * smoothSpeed);
                }
                if(currentDirection == "left")
                {
                    direction =  directionToMove * Mathf.Lerp(direction.z, Input.GetAxis("Mouse X") * multiplyAxis, Time.deltaTime * smoothSpeed);
                }

                direction = Vector3.ClampMagnitude(direction,1f);
            }

            if (rbBody.velocity.magnitude > 0.5f)
            {
                rb.rotation = Quaternion.Slerp(rb.rotation,Quaternion.LookRotation(rbBody.velocity), Time.deltaTime * velocity );
            }
            else
            {
                rb.rotation = Quaternion.Slerp(rb.rotation,Quaternion.Euler(defaultRotatePlayer), Time.deltaTime * velocity * 2 );
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (gameState)
        {
            if (MoveByTouch)
            {
                Vector3 displacement = direction * Time.fixedDeltaTime;
            
                rbBody.velocity = (direction * Time.fixedDeltaTime * strafeSpeed) + displacement;
                rb.velocity = (direction * Time.fixedDeltaTime * strafeSpeed) + displacement;
            }
            else
            {
                rbBody.velocity = Vector3.zero;
                rb.velocity  = Vector3.zero;
            }
        }
    }


    private void RotateOnTurns(string direction)
    {
        currentDirection = direction;
        switch (direction)
        {
            case "forward":
                DOTween.To(()=> cameraFollow.offset, x=> cameraFollow.offset = x, new Vector3(0f, cameraFollow.offset.y, -3.45f), durationRotateAtTurn);
                DOTween.To(()=> defaultRotatePlayer, x=> defaultRotatePlayer = x, Vector3.zero, durationRotateAtTurn);
                DOTween.To(()=> cameraFollow.transform.eulerAngles, x=> cameraFollow.transform.eulerAngles = x, new Vector3(cameraFollow.transform.eulerAngles.x, 0f,0f), durationRotateAtTurn);
                statusBar.eulerAngles = Vector3.zero;
                directionToMove = Vector3.right;
                break;
            case "right":
                DOTween.To(()=> cameraFollow.offset, x=> cameraFollow.offset = x, new Vector3(-3.45f, cameraFollow.offset.y, 0), durationRotateAtTurn);
                DOTween.To(()=> defaultRotatePlayer, x=> defaultRotatePlayer = x, new Vector3(0f, 90f, 0f), durationRotateAtTurn);
                DOTween.To(()=> cameraFollow.transform.eulerAngles, x=> cameraFollow.transform.eulerAngles = x, new Vector3(cameraFollow.transform.eulerAngles.x, 90f, 0f), durationRotateAtTurn);
                statusBar.eulerAngles = new Vector3(0,90f,0);
                directionToMove = Vector3.forward;
                break;
            case "left":
                DOTween.To(()=> cameraFollow.offset, x=> cameraFollow.offset = x, new Vector3(3.45f, cameraFollow.offset.y, 0), durationRotateAtTurn);
                DOTween.To(()=> defaultRotatePlayer, x=> defaultRotatePlayer = x, new Vector3(0f, -90f, 0f), durationRotateAtTurn);
                DOTween.To(()=> cameraFollow.transform.eulerAngles, x=> cameraFollow.transform.eulerAngles = x, new Vector3(cameraFollow.transform.eulerAngles.x, -90f, 0f), durationRotateAtTurn);
                statusBar.eulerAngles = new Vector3(0,-90f,0);
                directionToMove = Vector3.forward;
                break;
            default:
                DOTween.To(()=> cameraFollow.offset, x=> cameraFollow.offset = x, new Vector3(0f, cameraFollow.offset.y, -3.45f), durationRotateAtTurn);
                DOTween.To(()=> defaultRotatePlayer, x=> defaultRotatePlayer = x, Vector3.zero, durationRotateAtTurn);
                DOTween.To(()=> cameraFollow.transform.eulerAngles, x=> cameraFollow.transform.eulerAngles = x, new Vector3(cameraFollow.transform.eulerAngles.x, 0f,0f), durationRotateAtTurn);
                statusBar.eulerAngles = Vector3.zero;
                directionToMove = Vector3.right;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Turn turn))
        {
            RotateOnTurns(turn.toDirection);
        }
    }

}
