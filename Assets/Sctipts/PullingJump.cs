using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PullingJump : MonoBehaviour
{
    [SerializeField] private float jumpPower_ = 10;
    [SerializeField] private float maxSpeed_ = 10;

    private Vector2 clickPosition_;
    private Rigidbody rigidbody_;

    private bool canJump_ = false;

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

    private void OnCollisionStay(Collision collision)
    {
        // 衝突点の情報が複数格納されている
        ContactPoint[] contacts = collision.contacts;
        // 0番目の衝突情報から、衝突している点の法線を取得
        Vector3 otherNormal = contacts[0].normal;
        // 上方向を示すベクトル。長さは1。
        Vector3 upVector = new Vector3(0, 1, 0);
        // 上方向と法線の内積。二つのベクトルはともに長さが1なので、cosBがdotUN変数に入る。
        float dotUN = Vector3.Dot(upVector, otherNormal);
        // 内積値に逆三角関数arccosを掛けて角度を算出。それを度数法へ変換する。
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        
        // 二つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする。
        if (dotDeg <= 45)
        {
            canJump_ = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            canJump_ = false;
        }
    }

    void OnClick(InputAction.CallbackContext context)
    {
        //物理移動量を強制的に(0,10,0)に変える。
        clickPosition_ = Mouse.current.position.ReadValue();
    }

    public void OnRelease(InputAction.CallbackContext context)
    {
        if (!canJump_) { return; }

        // クリックした座標と離した座標の差分を取得
        Vector2 dist = clickPosition_ - Mouse.current.position.ReadValue();

        // クリックとリリースが同じ座標ならば無視
        if (dist.sqrMagnitude == 0) { return; }

        // 差分を標準化し、jumpPowerをかけ合わせた値を移動量とする。
        rigidbody_.linearVelocity = dist.normalized * jumpPower_;

    }

}
