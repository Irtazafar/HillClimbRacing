using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int _totalCoinScore;

    public int getTotalCoinScore()
    {
        return _totalCoinScore;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       _totalCoinScore = 0;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = _totalCoinScore.ToString("F0");
    }

    public void calculateScore (int score)
    {
        _totalCoinScore += score;
        UpdateUI();
    }
}
