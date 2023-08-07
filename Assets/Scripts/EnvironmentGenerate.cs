using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class EnvironmentGenerate : MonoBehaviour
{

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


    [Header("Prefab Placement")]
    [SerializeField] private GameObject prefabToInstantiate;
    [SerializeField] private int numberOfPrefabs = 5; 
    [SerializeField] private float spacingBetweenPrefabs = 500f; 

    //FLAT TERRAIN
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField, Range(3f, 102f)] private int _levelLength = 50;
    [SerializeField, Range(1f, 50f)] private float _xMultiplier = 2f;
    [SerializeField] private float _flatY = 0f; // Set this to the desired Y-coordinate for a flat terrain
    [SerializeField] private float _bottom = 10f;
    [SerializeField, Range(0f, 1f)] private float _curveSmoothness = 0.5f;
    private Vector3 _lastPos;
    private BoxCollider2D _boxCollider;


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


        //PREFABS
        List<Vector3> prefabPositions = new List<Vector3>();

        float yScale = 1.25f; 
        float yOffset =0.95f; 
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float normalizedPosition = (float)i / (numberOfPrefabs - 1); 
            float xPos = normalizedPosition * (_levelLength - 1) * _xMultiplier; 

            xPos = Mathf.Clamp(xPos, 0f, (_levelLength - 10) * _xMultiplier);

            Vector3 prefabPosition = transform.position + new Vector3(xPos, _flatY + yOffset, 0f); 
            prefabPositions.Add(prefabPosition);
        }

        foreach (Vector3 position in prefabPositions)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, position, Quaternion.identity);
            instantiatedPrefab.transform.localScale = new Vector3(1.25f, yScale, 1.0f); 
        }

    }
}
