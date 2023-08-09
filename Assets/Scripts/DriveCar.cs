using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveCar : MonoBehaviour
{

    // CODE FOR INPUT HANDLING 

    /* [SerializeField]
     private Rigidbody2D _frontTireRB;

     [SerializeField]
     private Rigidbody2D _backTireRB;

     [SerializeField]
     private Rigidbody2D _carRb;

     [SerializeField]
     private float _speed = 150f;

     [SerializeField]
     private float _rotationSpeed = 300f;

     private float _moveInput;


     // Update is called once per frame
     void Update()
     {
         _moveInput = Input.GetAxis("Horizontal");
     }

     private void FixedUpdate()
     {

         _frontTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
         _backTireRB.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
         _carRb.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime);
     }*/
    /*private void Update()
   {
       _moveInput = Input.GetAxis("Horizontal");
   }*/


    //CODE FOR TOUCH HANDLING 

    [SerializeField]
    private Rigidbody2D _frontTireRB;

    [SerializeField]
    private Rigidbody2D _backTireRB;

    [SerializeField]
    private Rigidbody2D _carRb;

    [SerializeField]
    private float _speed = 150f;

    [SerializeField]
    private float _brakeSpeed = 150f;

    [SerializeField]
    private float _rotationSpeed = 300f;

    private float _moveInput;

    private void FixedUpdate()
    {
        //Debug.Log("Move Input: " + _moveInput);
        ApplyTorque(_speed);
    }

    public void ApplyBrake()
    {
       // Debug.Log("Applying Brake");
        _moveInput = -1f;
        ApplyTorque(_brakeSpeed);
    }

    public void ReleaseBrake()
    {
        _moveInput = 0f;
        ApplyTorque(0f);
    }

    public void ApplyRace()
    {
        _moveInput = 1f;
        //Debug.Log("Applying Race");
        ApplyTorque(_speed);
    }

    public void ReleaseRace()
    {
        _moveInput = 0f;
        ApplyTorque(0f);
    }

    private void ApplyTorque(float speed)
    {
        _frontTireRB.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        _backTireRB.AddTorque(-_moveInput * speed * Time.fixedDeltaTime);
        _carRb.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime);
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (_carRb.transform.up * 2f));
    }
}
