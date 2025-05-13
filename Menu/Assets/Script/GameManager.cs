using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("回合開始時執行的物件")]
    [SerializeField] private List<MonoBehaviour> startTurnObjects;

    [Header("回合結束時執行的物件")]
    [SerializeField] private List<MonoBehaviour> endTurnObjects;

    [Header("主武器（可選）")]
    [SerializeField] private Weapon mainWeapon;

    public void StartTurn()
    {
        Debug.Log("=== [GameManager] 回合開始 ===");

        if (mainWeapon != null)
        {
            mainWeapon.ResetTempBonus();
        }

        foreach (var obj in startTurnObjects)
        {
            if (obj is IStartTurnAction action)
            {
                action.StartTurnAction();
            }
        }
    }

    public void EndTurn()
    {
        Debug.Log("=== [GameManager] 回合結束 ===");

        foreach (var obj in endTurnObjects)
        {
            if (obj is IEndTurnAction action)
            {
                action.EndTurnAction();
            }
        }
    }
}
