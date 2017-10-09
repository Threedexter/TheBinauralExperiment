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
    bool WithinRange;
    bool FinishedPuzzle;
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
    }

    #endregion

    #region TriggerHandling
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            WithinRange = true;
            return;
        }
        WithinRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        WithinRange = false;
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
    }
    #endregion

    #region get/set
    public bool getCanInteract()
    {
        return canInteract;
    }

    public void setCanInteract(bool interactable)
    {
        if(!FinishedPuzzle)
        canInteract = interactable;
    }

    public bool getFinishedPuzzle()
    {
        return FinishedPuzzle;
    }

    public void FinishPuzzle()
    {
        FinishedPuzzle = true;
    }
    #endregion
}
