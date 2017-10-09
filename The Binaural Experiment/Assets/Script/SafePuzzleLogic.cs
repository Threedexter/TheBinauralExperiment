using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePuzzleLogic : MonoBehaviour
{
    #region safecombinationstep
    public enum TurnDirection { Left, Right };

    public struct CombinationStep
    {
        public TurnDirection direction;
        public int tumblerAngle;
    }
    #endregion

    #region safecombination
    public CombinationStep[,] combinations = new CombinationStep[3,10];

    CombinationStep CreateNewCombination(TurnDirection dir, int angle)
    {
        CombinationStep step = new CombinationStep();
        step.direction = dir;
        step.tumblerAngle = angle;
        return step;
    }

    void PopulateCombinationsList()
    {
        combinations[0,0] = CreateNewCombination(TurnDirection.Right, 51);
        combinations[1,0] = CreateNewCombination(TurnDirection.Left, 290);
        combinations[2,0] = CreateNewCombination(TurnDirection.Right, 65);

        combinations[0, 1] = CreateNewCombination(TurnDirection.Left, 274);
        combinations[1, 1] = CreateNewCombination(TurnDirection.Right, 73);
        combinations[2, 1] = CreateNewCombination(TurnDirection.Left, 169);

        combinations[0, 2] = CreateNewCombination(TurnDirection.Right, 95);
        combinations[1, 2] = CreateNewCombination(TurnDirection.Left, 243);
        combinations[2, 2] = CreateNewCombination(TurnDirection.Right, 125);

        combinations[0, 3] = CreateNewCombination(TurnDirection.Left, 68);
        combinations[1, 3] = CreateNewCombination(TurnDirection.Right, 258);
        combinations[2, 3] = CreateNewCombination(TurnDirection.Left, 164);

        combinations[0, 4] = CreateNewCombination(TurnDirection.Right, 241);
        combinations[1, 4] = CreateNewCombination(TurnDirection.Left, 25);
        combinations[2, 4] = CreateNewCombination(TurnDirection.Right, 13);

        combinations[0, 5] = CreateNewCombination(TurnDirection.Left, 85);
        combinations[1, 5] = CreateNewCombination(TurnDirection.Right, 176);
        combinations[2, 5] = CreateNewCombination(TurnDirection.Left, 56);

        combinations[0, 6] = CreateNewCombination(TurnDirection.Right, 45);
        combinations[1, 6] = CreateNewCombination(TurnDirection.Left, 94);
        combinations[2, 6] = CreateNewCombination(TurnDirection.Right, 168);

        combinations[0, 7] = CreateNewCombination(TurnDirection.Left, 35);
        combinations[1, 7] = CreateNewCombination(TurnDirection.Right, 91);
        combinations[2, 7] = CreateNewCombination(TurnDirection.Left, 238);

        combinations[0, 8] = CreateNewCombination(TurnDirection.Right, 48);
        combinations[1, 8] = CreateNewCombination(TurnDirection.Left, 86);
        combinations[2, 8] = CreateNewCombination(TurnDirection.Right, 12);

        combinations[0, 9] = CreateNewCombination(TurnDirection.Left, 165);
        combinations[1, 9] = CreateNewCombination(TurnDirection.Right, 95);
        combinations[2, 9] = CreateNewCombination(TurnDirection.Left, 49);
    }

    public CombinationStep[] ReturnCombination(int combinationIndex)
    {
        CombinationStep[] combination = new CombinationStep[3];
        combination[0] = combinations[0, combinationIndex];
        combination[1] = combinations[1, combinationIndex];
        combination[2] = combinations[2, combinationIndex];
        return combination;
    }
    #endregion

    void Start()
    {
        PopulateCombinationsList();
    }

    void Update()
    {

    }
}
