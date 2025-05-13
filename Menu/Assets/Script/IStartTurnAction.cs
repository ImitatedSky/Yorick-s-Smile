public interface IStartTurnAction
{
    void StartTurnAction() {
        Console.WriteLine($"Default StartTurnAction executed in {this.GetType().Name}.cs");
    };
}