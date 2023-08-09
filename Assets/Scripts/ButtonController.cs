using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private DriveCar carController;

    [SerializeField]
    private bool applyBrake = false;

    private void Start()
    {
        if(gameObject.CompareTag("BrakeBtnTag"))
        {
            applyBrake = true;
        }
        else
        {
            applyBrake = false;
        }
    }

    public void SetCarController(DriveCar controller)
    {
        carController = controller;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("BUTTON PRESSED");
        if (applyBrake)
        {
            carController.ApplyBrake();
        }
        else
        {
            carController.ApplyRace();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("BUTTON RElased");
        if (applyBrake)
        {
            carController.ReleaseBrake();
        }
        else
        {
            carController.ReleaseRace();
        }
    }
}