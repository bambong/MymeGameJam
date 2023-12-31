
using UnityEngine;

public class GameStateController
{
    public GameManager stateParent;
    public IState<GameManager> curState;
    public GameStateController(GameManager gameManager) 
    {
        stateParent = gameManager;
        curState = GameNone.Instance;
    }

    public void UpdateActive() 
    {
        curState.OnUpdate(stateParent);
    }
    public void FixedUpdateActive() 
    {
        curState.OnFixedUpdate(stateParent);
     
    }

    public void ChangeState(IState<GameManager> state) 
    {
        if(curState == state) 
        {
            Debug.Log("같은 상태 전환 시도");
            return;
        }

        state.OnEnter(stateParent);
        curState.OnExit(stateParent);
        curState = state;
    }
}

public class GameNone : Singleton<GameNone>, IState<GameManager>,IInit
{
    public void Init()
    {
    }

    public void OnEnter(GameManager controller)
    {
    }

    public void OnExit(GameManager controller)
    {
    }

    public void OnFixedUpdate(GameManager controller)
    {
    }

    public void OnUpdate(GameManager controller)
    {
    }
}
public class GameOver : Singleton<GameOver>, IState<GameManager>, IInit
{
    public void Init()
    {
    }

    public void OnEnter(GameManager controller)
    {
        foreach (var item in controller.merchantSpawnController.currentMerchantControllers) 
        {
            item.StopAllCoroutines();
        }
        controller.endPanel.Open();
        SoundManager.Instance.StopAudio_BGM();
        SoundManager.Instance.PlayAudio_Etc_End();
    }

    public void OnExit(GameManager controller)
    {
    }

    public void OnFixedUpdate(GameManager controller)
    {
    }

    public void OnUpdate(GameManager controller)
    {
    }
}
public class GamePlay : Singleton<GamePlay>, IState<GameManager>, IInit
{
    public void Init()
    {
    }

    public void OnEnter(GameManager controller)
    {
    }

    public void OnExit(GameManager controller)
    {
    }

    public void OnFixedUpdate(GameManager controller)
    {
    }

    public void OnUpdate(GameManager controller)
    {
        controller.merchantSpawnController.SpawnUpdate();
    }
}