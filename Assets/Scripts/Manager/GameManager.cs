using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum GameState
{
    RUNNER,
    TRANSITION,
    PAINTING
}



public class GameManager : Singleton<GameManager>
{
    [Header("General Settings")]
    public GameState gameState;

    [Header("Painting Wall")]
    [SerializeField] GameObject paintWall;
    [SerializeField] Canvas wallSectionCanvas;
    [SerializeField] private Slider percentageBar;
    [SerializeField] private TMP_Text percentageText;

    [Header("Mobile Platform")]
    [SerializeField] private Canvas mobileInputCanvas;

    private float paintedVerticeCount;
    private float totalVerticeCount;


    private void Start()
    {
        //open joystick canvas for mobile platform
        if (Application.platform.Equals(RuntimePlatform.Android) || Application.platform.Equals(RuntimePlatform.IPhonePlayer))
        {
            mobileInputCanvas.gameObject.SetActive(true);
        }

        totalVerticeCount = paintWall.GetComponent<MeshFilter>().mesh.vertexCount;
    }

    private void Update()
    {
        percentageText.text = string.Format("{0:P1}", CalculatePercentagePaintedWall());
        percentageBar.value = (float) CalculatePercentagePaintedWall(); 
    }


    #region Increment and Calculate Percentage of Painted Wall
    public float IncrementPaintedVerticeCount()
    {
        return paintedVerticeCount++;
    }

    private double CalculatePercentagePaintedWall()
    {
        double percentage = (paintedVerticeCount / totalVerticeCount);
        return percentage;
    }
    #endregion

    #region Transition From Running to Painting
    public void OpenPaintWall()
    {

        StartCoroutine(TransitionFromRunningToPainting());

    }

    IEnumerator TransitionFromRunningToPainting()
    {
        gameState = GameState.TRANSITION;
        mobileInputCanvas.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameState = GameState.PAINTING;
        paintWall.gameObject.SetActive(true);
        wallSectionCanvas.gameObject.SetActive(true);
    }
    #endregion
}
