using UnityEngine;

public class FollowCameraFinal : MonoBehaviour
{
    public Transform player;  // 玩家对象
    public Vector3 offset = new Vector3(0, 5, -5);  // 调整Z轴，靠近玩家
    public float rotationSpeed = 100.0f;  // 相机旋转速度
    public float cameraDistance = 5f;  // 调整相机与玩家的距离
    public float verticalOffset = 2f;   // 相机在 Y 轴上的高度偏移

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void Start()
    {
        // 初始化相机的位置
        transform.position = player.position + offset;
    }

    void LateUpdate()
    {
        // 使用键盘输入控制相机旋转
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotationX -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotationX += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentRotationY -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentRotationY += rotationSpeed * Time.deltaTime;
        }

        // 限制垂直旋转角度
        currentRotationY = Mathf.Clamp(currentRotationY, -30f, 60f);

        // 计算相机基于玩家的旋转角度
        Quaternion playerRotation = player.rotation;
        Quaternion rotation = playerRotation * Quaternion.Euler(currentRotationY, currentRotationX, 0);

        // 根据旋转计算相机位置
        Vector3 desiredPosition = player.position + rotation * new Vector3(0, verticalOffset, -cameraDistance);

        // 更新相机的位置和旋转
        transform.position = desiredPosition;
        transform.LookAt(player.position + Vector3.up * verticalOffset);
    }
}
