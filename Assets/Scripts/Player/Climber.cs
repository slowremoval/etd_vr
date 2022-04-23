using UnityEngine;

public class Climber : MonoBehaviour
{
    public static HandControllerInput CurrentHandControllerInput;

    private Rigidbody _playerRb;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if (CurrentHandControllerInput != null)
        {
            Climb();
        }
        else
        {
            _playerRb.useGravity = true;
        }
    }
    
    private void Climb()
    {
        _playerRb.velocity = new Vector3(
            -CurrentHandControllerInput.Velocity.x, 
            -CurrentHandControllerInput.Velocity.y, 
            -CurrentHandControllerInput.Velocity.z);
    }
}