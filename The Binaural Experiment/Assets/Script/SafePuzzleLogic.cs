using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePuzzleLogic : MonoBehaviour
{
    #region unity
    void Start()
    {
        PopulateCombinationsList();
    }

    void Update()
    {

    }
    #endregion

    #region safecombinationstep
    public enum TurnDirection { Left, Right };

    public struct CombinationStep
    {
        public TurnDirection direction;
        public int tumblerAngle;
    }
    #endregion

    #region safecombination
    public CombinationStep[,] combinations = new CombinationStep[10, 3];

    CombinationStep CreateNewCombination(TurnDirection dir, int angle)
    {
        CombinationStep step = new CombinationStep();
        step.direction = dir;
        step.tumblerAngle = angle;
        return step;
    }

    void PopulateCombinationsList()
    {
        combinations[0, 0] = CreateNewCombination(TurnDirection.Right, 51);
        combinations[0, 1] = CreateNewCombination(TurnDirection.Left, 290);
        combinations[0, 2] = CreateNewCombination(TurnDirection.Right, 65);

        combinations[1, 0] = CreateNewCombination(TurnDirection.Left, 274);
        combinations[1, 1] = CreateNewCombination(TurnDirection.Right, 73);
        combinations[1, 2] = CreateNewCombination(TurnDirection.Left, 169);

        combinations[2, 0] = CreateNewCombination(TurnDirection.Right, 95);
        combinations[2, 1] = CreateNewCombination(TurnDirection.Left, 243);
        combinations[2, 2] = CreateNewCombination(TurnDirection.Right, 125);

        combinations[3, 0] = CreateNewCombination(TurnDirection.Left, 68);
        combinations[3, 1] = CreateNewCombination(TurnDirection.Right, 258);
        combinations[3, 2] = CreateNewCombination(TurnDirection.Left, 164);

        combinations[4, 0] = CreateNewCombination(TurnDirection.Right, 241);
        combinations[4, 1] = CreateNewCombination(TurnDirection.Left, 25);
        combinations[4, 2] = CreateNewCombination(TurnDirection.Right, 13);

        combinations[5, 0] = CreateNewCombination(TurnDirection.Left, 85);
        combinations[5, 1] = CreateNewCombination(TurnDirection.Right, 176);
        combinations[5, 2] = CreateNewCombination(TurnDirection.Left, 56);

        combinations[6, 0] = CreateNewCombination(TurnDirection.Right, 45);
        combinations[6, 1] = CreateNewCombination(TurnDirection.Left, 94);
        combinations[6, 2] = CreateNewCombination(TurnDirection.Right, 168);

        combinations[7, 0] = CreateNewCombination(TurnDirection.Left, 35);
        combinations[7, 1] = CreateNewCombination(TurnDirection.Right, 91);
        combinations[7, 2] = CreateNewCombination(TurnDirection.Left, 238);

        combinations[8, 0] = CreateNewCombination(TurnDirection.Right, 48);
        combinations[8, 1] = CreateNewCombination(TurnDirection.Left, 86);
        combinations[8, 2] = CreateNewCombination(TurnDirection.Right, 12);

        combinations[9, 0] = CreateNewCombination(TurnDirection.Left, 165);
        combinations[9, 1] = CreateNewCombination(TurnDirection.Right, 95);
        combinations[9, 2] = CreateNewCombination(TurnDirection.Left, 49);
    }

    public CombinationStep[] ReturnCombination(int combinationIndex)
    {
        CombinationStep[] combination = new CombinationStep[3];
        combination[0] = combinations[combinationIndex, 0];
        combination[1] = combinations[combinationIndex, 1];
        combination[2] = combinations[combinationIndex, 2];
        return combination;
    }
    #endregion
}
