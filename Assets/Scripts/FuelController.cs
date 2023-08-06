using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FuelController : MonoBehaviour
{

    public static FuelController instance;

    [SerializeField]
    private Image _fuelImage;

    [SerializeField, Range(0.1f, 5f)]
    private float _fuelDrainSpeed = 1f;

    [SerializeField]
    private float _maxFuelAmount = 100f;

    [SerializeField]
    private Gradient _fuelGradient;

    private float _currentFuelAmount;
    

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
        _fuelImage.color = _fuelGradient.Evaluate(_fuelImage.fillAmount);

    }    
}
