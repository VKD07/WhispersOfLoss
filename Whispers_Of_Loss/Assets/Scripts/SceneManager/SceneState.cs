
public abstract class SceneState
{
    public abstract void OnEnterState(GameStateManager state);
    public abstract void OnUpdateState(GameStateManager state);
    public abstract void OnCollisionEnter(GameStateManager state);
}

