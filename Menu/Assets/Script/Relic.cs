using UnityEngine;

public class Relic : MonoBehaviour, IEndTurnAction
{
    [Header("遺物名稱")]
    [SerializeField] private string relicName = "神秘遺物";

    public void EndTurnAction()
    {
        Debug.Log($"[遺物] {relicName} 在回合結束時觸發效果！");
    }
}
