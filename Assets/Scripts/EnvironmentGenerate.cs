using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//[ExecuteInEditMode]
public class EnvironmentGenerate : MonoBehaviour
{

    #region Curved Terrain
    /* [SerializeField] private SpriteShapeController _spriteShapeController;
     [SerializeField, Range(3f, 100f)] private int _levelLength = 50;
     [SerializeField, Range(1f, 50f)] private float _xMultipler = 2f;
     [SerializeField, Range(1f, 50f)] private float _yMultipler = 2f;
     [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
     [SerializeField] private float _noiseStep = 0.5f;
     [SerializeField] private float _bottom = 10f;

     private Vector3 _lastPos;

     private void OnValidate()
     {
         _spriteShapeController.spline.Clear();

         for (int i = 0; i < _levelLength; i++)
         {
             _lastPos = transform.position + new Vector3(i * _xMultipler, Mathf.PerlinNoise(0, i * _noiseStep) * _yMultipler);
             _spriteShapeController.spline.InsertPointAt(i, _lastPos);

             if (i != 0 && i != _levelLength - 1)
             {
                 _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                 _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultipler * _curveSmoothness);
                 _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultipler * _curveSmoothness);
             }
         }

         _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom));
         _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
     }*/
    #endregion

    [Header("Fuel Placement")]
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private int numberOfPrefabs = 5; 
    [SerializeField] private float spacingBetweenPrefabs = 500f;
    //[SerializeField] private Transform FuelParent;

    [Header("Coins Placement")]
    [SerializeField] private GameObject _fiveCoint;
    [SerializeField] private GameObject _twentyFiveCoint;
    [SerializeField] private GameObject _hundredCoint;
    [SerializeField] private GameObject _fiveHundredCoint;
    [SerializeField, Range(3f, 100f)] private int numberofCoins = 5;
    //[SerializeField, Range(3f, 100f)] private int coinSpacing = 5;
    //[SerializeField, Range(3f, 100f)] private int spacingBetweenCoins = 5;

    //[SerializeField] private Transform coinParent;

    //FLAT TERRAIN
    [Header("Flat Terrain")]
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField, Range(3f, 102f)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField] private float _flatY = 0f; // Set this to the desired Y-coordinate for a flat terrain
    [SerializeField] private float _bottom = 10f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    private Vector3 _lastPos;
    private BoxCollider2D _boxCollider;


    private void Start()
    {
        //Fuel Placement 
        GenerateFuelPlacements();

        //Coins Placement
        CoinsPlacement();

    }


    private void CoinsPlacement()
    {
        List<Vector3> coinBatchPositions = new List<Vector3>();

        float yScale = 1.0f; // Adjust the yScale as needed
        float yOffset = 1f; // Adjust the yOffset as needed
        float coinSpacing = 20f; // Increased spacing between batches

        for (int i = 0; i < numberofCoins; i++)
        {
            Vector3 coinBatchPosition = Vector3.zero;
            bool foundPosition = false;

            for (int attempt = 0; attempt < 100; attempt++) // Increased attempts
            {
                float normalizedPosition = Random.Range(0f, 1f);
                float xPos = normalizedPosition * (_levelLength - 10) * _xMultiplier;

                xPos = Mathf.Clamp(xPos, 10f, (_levelLength - 10) * _xMultiplier);

                coinBatchPosition = transform.position + new Vector3(xPos, _flatY + yOffset, 0f);

                // Check if there's any overlap with existing coins using raycasting
                bool overlap = Physics2D.Raycast(coinBatchPosition, Vector2.up, yScale);
                if (!overlap)
                {
                    foundPosition = true;
                    break; // No overlap, use this position
                }
            }

            if (!foundPosition)
            {
                continue; // Skip this iteration if no suitable position found
            }

            coinBatchPositions.Add(coinBatchPosition);

            // Add the spacing between batches
            if (i > 0)
            {
                coinBatchPosition.x += coinSpacing * _xMultiplier;
            }
        }

        foreach (Vector3 position in coinBatchPositions)
        {
            GameObject coinPrefab = GetRandomCoinPrefab();
            if (coinPrefab != null)
            {
                GameObject instantiatedCoin = Instantiate(coinPrefab, position, Quaternion.identity);
                instantiatedCoin.transform.localScale = new Vector3(1f, yScale, 1.0f);
            }
        }
    }

    private void GenerateFuelPlacements()
    {
        List<Vector3> prefabPositions = new List<Vector3>();

        float yScale = 1.25f;
        float yOffset = 0.95f;
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float normalizedPosition = (float)i / (numberOfPrefabs - 1);
            float xPos = normalizedPosition * (_levelLength - 10) * _xMultiplier;

            xPos = Mathf.Clamp(xPos, 10f, (_levelLength - 10) * _xMultiplier);

            Vector3 prefabPosition = transform.position + new Vector3(xPos, _flatY + yOffset, 0f);
            prefabPositions.Add(prefabPosition);
        }

        foreach (Vector3 position in prefabPositions)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, position, Quaternion.identity);
            instantiatedPrefab.transform.localScale = new Vector3(1.25f, yScale, 1.0f);
        }
    }

    //WITHOUT BOX COLLIDER ADJUSTMENT
    /*private void OnValidate()
    {
        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLength; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, _flatY, 0f);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }


        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom));
        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));
    }*/

    //WITH BOX COLLIDER ADJUSTMENT AUTOMATICALLY
    private void OnValidate()
    {
        if (_spriteShapeController == null)
            return;

        _spriteShapeController.spline.Clear();

        for (int i = 0; i < _levelLength; i++)
        {
            _lastPos = transform.position + new Vector3(i * _xMultiplier, _flatY, 0f);
            _spriteShapeController.spline.InsertPointAt(i, _lastPos);

            if (i != 0 && i != _levelLength - 1)
            {
                _spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                _spriteShapeController.spline.SetLeftTangent(i, Vector3.left * _xMultiplier * _curveSmoothness);
                _spriteShapeController.spline.SetRightTangent(i, Vector3.right * _xMultiplier * _curveSmoothness);
            }
        }

        _spriteShapeController.spline.InsertPointAt(_levelLength, new Vector3(_lastPos.x, transform.position.y - _bottom));
        _spriteShapeController.spline.InsertPointAt(_levelLength + 1, new Vector3(transform.position.x, transform.position.y - _bottom));

        // Adjust the Box Collider 2D size based on the level length and x multiplier
        if (_boxCollider == null)
            _boxCollider = GetComponent<BoxCollider2D>();

        float colliderWidth = (_levelLength - 1) * _xMultiplier;
        _boxCollider.size = new Vector2(colliderWidth, 0.1f);
        _boxCollider.offset = new Vector2(colliderWidth / 2f, _spriteShapeController.transform.position.y);
        }

    private GameObject GetRandomCoinPrefab()
    {
        GameObject[] coinPrefabs = new GameObject[] { _fiveCoint, _twentyFiveCoint, _hundredCoint, _fiveHundredCoint };
        int randomIndex = Random.Range(0, coinPrefabs.Length);
        return coinPrefabs[randomIndex];
    }

}
