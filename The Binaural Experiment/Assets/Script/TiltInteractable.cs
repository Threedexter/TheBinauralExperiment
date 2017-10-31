using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltInteractable : MonoBehaviour
{

    #region Globals
    BaseInteractable baseInteract;
    public GameObject Maze;
    public GameObject ball;

    Vector3 mazeStartPosition;
    Vector3 ballStartPosition;

    [Range(0f, 10f)]
    public float sensitivityFactor;
    float sensFact;

    [Range(0f, 90f)]
    public float maxTilt;

    bool resetBall;

    #endregion

    #region UnityEngine
    void Start()
    {
        mazeStartPosition = Maze.transform.localEulerAngles;
        ballStartPosition = ball.transform.position;
        baseInteract = GetComponent<BaseInteractable>();
        sensFact = 1 / sensitivityFactor;
    }

    void Update()
    {
        MazeTilt();
        CheckIfPlayerIsInRange();
        CheckBallPosition();
    }
    #endregion

    #region interaction
    void MazeTilt()
    {
        if (baseInteract.getCanInteract())
        {
            float inputVal = Input.GetAxis("Mouse X") * 0.2f;
            float InputY = Input.GetAxis("Mouse Y") * 0.2f;
            XTilt(inputVal);
            YTilt(InputY);
        }
    }

    void XTilt(float axisval)
    {
        if(Maze.transform.localEulerAngles.x < maxTilt || Maze.transform.localEulerAngles.x > (360 - maxTilt))
        {
            Maze.transform.Rotate(new Vector3(axisval, 0));
        }
        if(Maze.transform.localEulerAngles.x >= maxTilt && Maze.transform.localEulerAngles.x < 90)
        {
            Maze.transform.localEulerAngles = new Vector3(maxTilt - 1, Maze.transform.localEulerAngles.y,0);
        }
        if (Maze.transform.localEulerAngles.x <= (360 -maxTilt) && Maze.transform.localEulerAngles.x > 270)
        {
            Maze.transform.localEulerAngles = new Vector3(360 - maxTilt + 1, Maze.transform.localEulerAngles.y,0);
        }
        Maze.transform.localEulerAngles = new Vector3(Maze.transform.localEulerAngles.x, Maze.transform.localEulerAngles.y, 0);

    }

    void YTilt(float axisval)
    {
        if (Maze.transform.localEulerAngles.y < maxTilt || Maze.transform.localEulerAngles.y > (360 - maxTilt))
        {
            Maze.transform.Rotate(new Vector3(0, axisval));
        }
        if (Maze.transform.localEulerAngles.y >= maxTilt && Maze.transform.localEulerAngles.y < 90)
        {
            Maze.transform.localEulerAngles = new Vector3(Maze.transform.localEulerAngles.x,maxTilt - 1,0);
        }
        if (Maze.transform.localEulerAngles.y <= (360 - maxTilt) && Maze.transform.localEulerAngles.y > 270)
        {
            Maze.transform.localEulerAngles = new Vector3(Maze.transform.localEulerAngles.x, 360 - maxTilt + 1,0);
        }
        Maze.transform.localEulerAngles = new Vector3(Maze.transform.localEulerAngles.x, Maze.transform.localEulerAngles.y, 0);
    }

    void CheckIfPlayerIsInRange()
    {
        if(!baseInteract.getWithinRange() && !baseInteract.getFinishedPuzzle() && !resetBall)
        {
            ball.transform.position = ballStartPosition;
            resetBall = true;
            Maze.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if (baseInteract.getWithinRange())
        {
            resetBall = false;
        }
    }
    #endregion

    void RemoveBallAfterPuzzle()
    {
        if(baseInteract.getFinishedPuzzle())
        {
            ball.SetActive(false);
        }
    }

    void CheckBallPosition()
    {
        if(ball.transform.position.y < 1.18)
        {
            ball.transform.position = ballStartPosition;
            resetBall = true;
            Maze.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
