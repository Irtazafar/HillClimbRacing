using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{

    [Header("Car Prefabs")]
    public GameObject[] carPrefabs;

    [Header("Other Components")]
    public CinemachineVirtualCamera virtualCamera;

    private DistanceCalculate distanceCalculator;

    private DriveCar currentCarController;
    private ButtonController brakeButton;
    private ButtonController raceButton;

    private GameObject currentCar;

    private void Start()
    {
        /* Vector3 desiredPosition = new Vector3(0.85f, 2.5f, 0.12f);
         Vector3 desiredScale = new Vector3(1f, 1f, 1f);

         InstantiateCarPrefab(desiredPosition, desiredScale);*/

        Vector3 desiredPosition = new Vector3(0.85f, 2.5f, 0.12f);
        Vector3 desiredScale = new Vector3(1f, 1f, 1f);

        distanceCalculator = FindObjectOfType<DistanceCalculate>();
        brakeButton = GameObject.FindGameObjectWithTag("BrakeBtnTag").GetComponent<ButtonController>();
        raceButton = GameObject.FindGameObjectWithTag("RaceBtnTag").GetComponent<ButtonController>();

        if (distanceCalculator != null && brakeButton !=null && raceButton!=null)
        {
            InstantiateCarPrefab(desiredPosition, desiredScale);
            distanceCalculator.PlayerPos = currentCar.transform;
            currentCarController = currentCar.GetComponent<DriveCar>();
            brakeButton.SetCarController(currentCarController);
            raceButton.SetCarController(currentCarController);
        }
        else
        {
            Debug.LogError("DistanceCalculate component not found!");
        }

        
    }

    public void SwitchCar(int carIndex)
    {
        if (currentCar != null)
        {
            Destroy(currentCar);
        }
        currentCar = Instantiate(carPrefabs[carIndex], transform.position, Quaternion.identity);
        currentCar.transform.localScale = Vector3.one;
        virtualCamera.Follow = currentCar.transform;
        distanceCalculator.PlayerPos = currentCar.transform;
        currentCarController = currentCar.GetComponent<DriveCar>();
        brakeButton.SetCarController(currentCarController);
        raceButton.SetCarController(currentCarController);

    }

    public void InstantiateCarPrefab(Vector3 position, Vector3 scale)
    {
       
        if (carPrefabs.Length > 0 && carPrefabs[0] != null)
        {
          
            currentCar = Instantiate(carPrefabs[2], position, Quaternion.identity);
            currentCar.transform.localScale = scale;
            virtualCamera.Follow = currentCar.transform;
            distanceCalculator.PlayerPos = currentCar.transform;
            currentCarController = currentCar.GetComponent<DriveCar>();
            brakeButton.SetCarController(currentCarController);
            raceButton.SetCarController(currentCarController);

        }
    }

}
