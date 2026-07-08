using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PullingJump : MonoBehaviour
{
    [SerializeField] private float jumpPower_ = 10;
    [SerializeField] private float maxSpeed_ = 10;

    private Vector2 clickPosition_;
    private Rigidbody rigidbody_;

    void Start()
    {
        // クリック入力の登録
        InputActionAsset inputActions = InputSystem.actions;
        InputAction submitAction = inputActions.FindAction("Player/Click");
        submitAction.started += OnClick;
        submitAction.canceled += OnRelease;

        // コンポーネント定義
        rigidbody_ = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 移動量の取得
        Vector3 linearVelocity = rigidbody_.linearVelocity;
        // 移動量の長さの二乗を取得·maxSpeedの二乗と比較
        if (linearVelocity.sqrMagnitude > maxSpeed_ * maxSpeed_)
        {
            // 移動量の大きさを最大速度に丸める
            rigidbody_.linearVelocity = linearVelocity.normalized * maxSpeed_;
        }
    }

    // 左クリックを押したとき
    void OnClick(InputAction.CallbackContext context)
    {
        //物理移動量を強制的に(0,10,0)に変える。
        clickPosition_ = Mouse.current.position.ReadValue();
    }

    // 左クリックを離したとき
    public void OnRelease(InputAction.CallbackContext context)
    {
        // クリックした座標と離した座標の差分を取得
        Vector2 dist = clickPosition_ - Mouse.current.position.ReadValue();

        // クリックとリリースが同じ座標ならば無視
        if (dist.sqrMagnitude == 0) { return; }

        // 差分を標準化し、jumpPowerをかけ合わせた値を移動量とする。
        rigidbody_.linearVelocity = dist.normalized * jumpPower_;

    }

}
