using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float DashSpeed = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;
    private bool shift;

    private List<AudioSource> sound = new List<AudioSource>();
    public float stepInterval = 0.5f; // 足音を鳴らす間隔 (秒)
    private float stepTimer;         // タイマー用
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudioSource[] sounds = GetComponentsInChildren<AudioSource>(true);
        foreach(AudioSource l in sounds)
        {
            sound.Add(l);
        }
        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Input Systemイベント用
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        shift = context.ReadValue<float>() > 0.5f;
    }

    void Update()
    {
        // 視点操作（マウス）省略なし

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        
        // 水平方向の速度を取得し、本当に移動しているかをチェック
        // y軸（垂直方向）の速度は無視、x-z平面での速度のみを考慮
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        
        // 速度の二乗の長さが、非常に小さな値（ここでは0.05f）より大きい場合に移動中と判断します。
        // これにより、物理演算の微細なブレで音が鳴り続けるのを防ぎます。
        bool isMovingOnGround = horizontalVelocity.sqrMagnitude > 0.05f; 

        if (isMovingOnGround)
        {
            // 移動速度に応じてタイマー間隔を調整
            // Dashのときはより速く音が鳴るように間隔を短くします。
            float currentInterval = stepInterval / (shift ? (DashSpeed / moveSpeed) : 1f);

            stepTimer -= Time.deltaTime;
            
            if (stepTimer <= 0f)
            {
                foreach (AudioSource s in sound)
                {
                    if (s.isPlaying)
                    {
                        s.Stop();
                    }
                }

                // 足音の選択
                AudioSource stepSound = shift ? sound[1] : sound[0];
                
                // Stopしてから再生することで、途中で音源が切り替わることを防ぐ
                if (stepSound.isPlaying)
                {
                    stepSound.Stop(); 
                }
                stepSound.Play(); 
                
                stepTimer = currentInterval; // タイマーリセット
            }
        }
        else
        {   
            //タイマーリセット
            stepTimer = 0f;
            
            //音源があれば停止
            foreach (AudioSource s in sound)
            {
                if (s.isPlaying)
                {
                    s.Stop();
                }
            }
        }
    }

//     void FixedUpdate()
//     {
//             if (shift)
//             {
//                 Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
//                 Vector3 rbval = rb.linearVelocity;
//                 rb.linearVelocity = moveDir.normalized * DashSpeed;
//                 if(rbval != rb.linearVelocity)
//                 {
//                     sound[0].Play();        
//                 }
//             }
//             else
//             {
//                 Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
//                 Vector3 rbval = rb.linearVelocity;
//                 rb.linearVelocity = moveDir.normalized * moveSpeed;
//                 if(rbval != rb.linearVelocity)
//                 {
//                     sound[1].Play();        
//                 }
//             }    
//         }
// }

    void FixedUpdate()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            float currentSpeed = shift ? DashSpeed : moveSpeed;
            Vector3 newVelocity = moveDir.normalized * currentSpeed;
            
            // Y軸の速度を保持しつつ、水平方向の速度を設定
            rb.linearVelocity = new Vector3(newVelocity.x, rb.linearVelocity.y, newVelocity.z);
        }
        else
        {
            // 移動入力をしていない場合、水平方向の速度をゼロにする
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }
}