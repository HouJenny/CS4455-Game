using UnityEngine;

public class SpeedUpPotion : MonoBehaviour
{
    public float speedBoostDuration = 5f;  // 速度增益持续时间

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取 RootMotionControlScript 脚本，激活速度增益
            RootMotionControlScript playerControl = other.GetComponent<RootMotionControlScript>();
            if (playerControl != null)
            {
                playerControl.ActivateSpeedBoost(speedBoostDuration);
            }


        }
    }
}