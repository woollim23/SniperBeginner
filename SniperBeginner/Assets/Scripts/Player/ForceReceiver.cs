using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ForceReceiver : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float drag = 0.3f;

    float vertical;
    Vector3 dampingVelocity;
    Vector3 force;

    public Vector3 Movement => force + Vector3.up * vertical;

    private void Awake() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update() 
    {
        AdjustForce();
    }


    public void Reset()
    {
        vertical = 0;
        force = Vector3.zero;
    }

    void AdjustForce()
    {
        if (controller.isGrounded)
            vertical = Physics.gravity.y * Time.deltaTime;
        else
            vertical += Physics.gravity.y * Time.deltaTime;

        force = Vector3.SmoothDamp(force, Vector3.zero, ref dampingVelocity, drag);    
    }

    public void AddForce(Vector3 amount)
    {
        force += amount;
    }
}
