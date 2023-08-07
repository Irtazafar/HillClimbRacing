using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Mime;
using Unity.VisualScripting;

public class DistanceCalculate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _distanceText;

    [SerializeField]
    private Transform _playerPos;

    private float _maxDistance;
    private Vector2 _startPos;
   

    private void Start()
    {
        _startPos = _playerPos.position;
        _maxDistance = 0f;
       
    }

    private void Update()
    {
        Vector2 distance = (Vector2)_playerPos.position - _startPos;
        distance.y = 0f;

        if(distance.x <0f)
        {
            distance.x = 0f;
        }

        if(distance.x > 0f)
        {
            _maxDistance += distance.x;
            _startPos = _playerPos.position;
        }else
        {
            distance.x = _maxDistance;
        }

        distance.x = _maxDistance;
        _distanceText.text =distance.x.ToString("F0") + "m";

    }
}
