using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ArrowDraw : MonoBehaviour
{
    // 矢印画像オブジェクト
    [SerializeField] private Image _arrowImage;

    // クリック
    InputAction _clickAction;

    // クリックした座標
    private Vector2 _clickPosition;

    // ドラッグ状態か否か
    private bool _isDrag = false;

    /// <summary>
    /// クリック時のコールバック関数
    /// </summary>
    /// <param name="context"></param>
    void OnClick(InputAction.CallbackContext context)
    {
        _clickPosition = Mouse.current.position.ReadValue();
        // ドラッグ開始
        _isDrag = true;
        // ArrowImage無効化
        _arrowImage.enabled = true;
    }

    /// <summary>
    /// クリックを離したときのコールバック関数
    /// </summary>
    /// <param name="context"></param>
    void OnRelease(InputAction.CallbackContext context)
    {
        // ドラッグ終了
        _isDrag = false;
        // ArrowImage無効化
        _arrowImage.enabled = false;
    }

    void Start()
    {
        // InputActionの取得
        InputActionAsset inputActions = InputSystem.actions;
        // InputAction(クリック)の取得
        _clickAction = inputActions.FindAction("Player/Click");
        // クリックしたときのコールバック関数を追加
        _clickAction.performed += OnClick;
        // 離したときのコールバック関数を追加
        _clickAction.canceled += OnRelease;

        // ArrowImage無効化
        _arrowImage.enabled = false;
    }

    void Update()
    {
        //もしドラッグしてたら
        if (_isDrag)
        {
            // 今のマウスポインタの座標を取得
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            // クリックした座標と今のマウスポインタの座標の差を算出
            Vector2 dist = _clickPosition - mousePosition;
            // ベクトルの長さを算出
            float size = dist.magnitude;
            // ベクトルから角度を算出
            float angleRad = Mathf.Atan2(dist.y, dist.x);
            //矢印の画像をクリックした場所に画像を移動
            _arrowImage.rectTransform.position = _clickPosition;
            //矢印の画像をベクトルから算出した角度を度数法に変換してZ軸回転
            _arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, angleRad * Mathf.Rad2Deg);
            //矢印の画像の大きさをドラッグした距離に合わせる
            _arrowImage.rectTransform.sizeDelta = new Vector2(size, size);
        }
    }
}