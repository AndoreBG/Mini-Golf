using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManagerTutorial : MonoBehaviour
{
    public static LevelManagerTutorial main;
    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI;
    [Space(10)]
    [SerializeField] private GameObject levelCompleteUI;
    [SerializeField] private TextMeshProUGUI levelCompletedStrokeUI;
    [Space(10)]
    [SerializeField] private GameObject gameOverUI;

    [Header("Atribbutes")]
    [SerializeField] private int maxStrokes;

    private int strokes;
    [HideInInspector] public bool outOfStrokes;
    [HideInInspector] public bool levelCompleted;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateStrokeUI();
    }
    
    public void IncreaseStroke()
    {
        strokes++;
        UpdateStrokeUI();

        if (strokes >= maxStrokes)
        {
            outOfStrokes = true;
        }
    }

    public void LevelComplete()
    {
        levelCompleted = true;

        levelCompletedStrokeUI.text = strokes > 1 ? "Você acertou o buraco com " + strokes + " tacadas!" : "Você conseguiu um Hole in One!";

        levelCompleteUI.SetActive(true);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }
}