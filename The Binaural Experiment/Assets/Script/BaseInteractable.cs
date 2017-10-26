using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{

    #region Globals
    [Range(1, 10)]
    public int BoxExtendX = 1;
    [Range(1, 10)]
    public int BoxExtendY = 1;
    [Range(1, 10)]
    public int BoxExtendZ = 1;

    public enum puzzleType { Safe, Maze, Lever, Pad };

    public puzzleType thisType;

    bool canInteract;

    BoxCollider box;
    public GameObject interactionIndicator;
    bool WithinRange;
    bool FinishedPuzzle;

    FPSHandler playerHandler;
    #endregion

    #region UnityEngine
    // Use this for initialization
    void Start()
    {
        box = GetComponent("BoxCollider") as BoxCollider;
        if (box == null)
        {
            return;
        }
        box.size = new Vector3(BoxExtendX, BoxExtendY, BoxExtendZ);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHandler = player.GetComponentInParent<FPSHandler>();
        ToggleRenderer(false);
    }

    void Update()
    {
        FadeBox();
    }

    #endregion

    #region TriggerHandling
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            WithinRange = false;
            return;
        }
        WithinRange = true;
        if(!getFinishedPuzzle()) ToggleRenderer(true);
    }

    void OnTriggerExit(Collider other)
    {
        WithinRange = false;
        ToggleRenderer(false);
    }

    public bool getWithinRange()
    {
        return WithinRange;
    }

    public void OnInteract(bool active)
    {
        if (getWithinRange() && !FinishedPuzzle)
        {
            canInteract = active;
        }
        if (canInteract)
        {
            ToggleRenderer(false);
        }
        else if(!canInteract && getWithinRange() && !getFinishedPuzzle())
        {
            ToggleRenderer(true);
        }
    }
    #endregion

    #region get/set
    public bool getCanInteract()
    {
        return canInteract;
    }

    public void setCanInteract(bool interactable)
    {
        if (!FinishedPuzzle)
            canInteract = interactable;
    }

    public bool getFinishedPuzzle()
    {
        return FinishedPuzzle;
    }

    public void FinishPuzzle()
    {
        FinishedPuzzle = true;
        canInteract = false;
        playerHandler.WinPuzzle();
    }
    #endregion

    #region materialHandling

    float lastColorChangeTime;

    void FadeBox()
    {
        Material material = interactionIndicator.GetComponent<Renderer>().material;
        float FadeDuration = 1f;
        Color matColor = interactionIndicator.GetComponent<Renderer>().material.color;
        Color startColor = new Color(matColor.r, matColor.g, matColor.b, 0.5f);
        Color endColor = new Color(matColor.r, matColor.g, matColor.b, 0.1f);

        var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
        ratio = Mathf.Clamp01(ratio);
        //material.color = Color.Lerp(startColor, endColor, ratio);
        material.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
        //material.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect

        if (ratio == 1f)
        {
            lastColorChangeTime = Time.time;

            // Switch colors
            var temp = startColor;
            startColor = endColor;
            endColor = temp;
        }
    }

    void ToggleRenderer(bool mode)
    {
        interactionIndicator.GetComponent<Renderer>().enabled = mode;
    }

    #endregion
}
