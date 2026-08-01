using UnityEngine;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    // シーン遷移を行うコンポーネント
    [SerializeField] private ChangeScene changeScene_;

    void Start()
    {
        // スペースキー入力(Player/Select)の登録
        InputActionAsset inputActions = InputSystem.actions;
        InputAction selectAction = inputActions.FindAction("Player/Select");
        selectAction.started += OnSelect;
    }

    private void OnDestroy()
    {
        // 登録したコールバックを解除する
        InputActionAsset inputActions = InputSystem.actions;

        InputAction selectAction = inputActions.FindAction("Player/Select");
        
        selectAction.started -= OnSelect;
    }

    void OnSelect(InputAction.CallbackContext context)
    {
        if (changeScene_ == null) { return; }

        // 指定されたシーンへ遷移する
        changeScene_.Change();
    }
}
