using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    [Header("基本攻擊力")]
    [SerializeField] private int baseAttack = 5;

    [Header("裝備帶來的加成")]
    [SerializeField] private int bonusAttack = 0;

    [Header("本回合的臨時加成")]
    [SerializeField] private int tempAttackBonus = 0;

    [Header("已安裝的寶石")]
    [SerializeField] private List<MonoBehaviour> gems = new List<MonoBehaviour>();

    public int TotalAttack => baseAttack + bonusAttack + tempAttackBonus;

    public void ResetTempBonus()
    {
        tempAttackBonus = 0;
    }

    public void AddBonus(int amount)
    {
        bonusAttack += amount;
    }

    public void AddTempBonus(int amount)
    {
        tempAttackBonus += amount;
    }

    public void AddGem(MonoBehaviour gem)
    {
        if (!gems.Contains(gem))
        {
            gems.Add(gem);
            Debug.Log($"[Weapon] 已加入寶石：{gem.GetType().Name}");
        }
    }

    public void RemoveGem(MonoBehaviour gem)
    {
        if (gems.Contains(gem))
        {
            gems.Remove(gem);
            Debug.Log($"[Weapon] 已移除寶石：{gem.GetType().Name}");
        }
    }

    public IReadOnlyList<MonoBehaviour> GetGems() => gems.AsReadOnly();
}
