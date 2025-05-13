using UnityEngine;
using IStartTurnAction

public class GemStartTurnBoost : MonoBehaviour, IStartTurnAction
{
    [Header("要套用的武器")]
    [SerializeField] private Weapon weapon;

    [Header("每回合增加臨時攻擊")]
    [SerializeField] private int tempBonusPerTurn = 1;

    void Start()
    {
        if (weapon != null)
        {
            weapon.AddGem(this);
        }
    }

    public void StartTurnAction()
    {
        if (weapon != null)
        {
            weapon.AddTempBonus(tempBonusPerTurn);
            Debug.Log($"[寶石B] 本回合 +{tempBonusPerTurn} 臨時攻擊力，總攻擊力：{weapon.TotalAttack}");
        }
    }
}
