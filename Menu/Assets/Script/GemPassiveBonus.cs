using UnityEngine;


// gemA
public class GemPassiveBonus : MonoBehaviour
{
    [Header("要套用的武器")]
    [SerializeField] private Weapon weapon;

    [Header("裝備時增加的攻擊力")]
    [SerializeField] private int bonus = 2;

    private bool applied = false;

    void Start()
    {
        if (!applied && weapon != null)
        {
            weapon.AddBonus(bonus);
            weapon.AddGem(this);
            Debug.Log($"[寶石A] 裝備 +{bonus} 攻擊力。");
            applied = true;
        }
    }
}
