
public interface IState<T>
{
    public void OnEnter(T controller);
    public void OnExit(T controller);
    public void OnFixedUpdate(T controller);
    public void OnUpdate(T controller);
}
