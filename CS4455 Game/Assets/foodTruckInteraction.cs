//using UnityEngine;

//public class foodTruckInteraction : MonoBehaviour
//{
//    public GameObject normalWindow;  // 正常窗户子对象
//    public GameObject brokenWindow;  // 破碎窗户子对象
//    private bool isBroken = false;  // 标记窗户是否已破裂

//    private void Start()
//    {
//        // 初始化窗户状态
//        normalWindow.SetActive(true);  // 正常玻璃显示
//        //brokenWindow.SetActive(false); // 破碎玻璃隐藏
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // 检测是否是 NPC 且窗户尚未破裂
//        if (!isBroken && other.CompareTag("Player"))
//        {
//            BreakWindow();
//        }
//    }

//    private void BreakWindow()
//    {
//        // 切换到破碎窗户状态
//        normalWindow.SetActive(false);  // 隐藏正常玻璃
//        brokenWindow.SetActive(true);   // 显示破碎玻璃
//        isBroken = true;                // 标记窗户已破裂
//        Debug.Log("Window broken!");    // 输出调试信息
//    }
//}

using UnityEngine;

public class foodTruckInteraction : MonoBehaviour
{
    public GameObject normalWindow;     
    public GameObject brokenWindow;     
    public AudioClip shatterSound;      
    public AudioSource shatterAudioSource; 
    private bool isBroken = false;     

    private void Start()
    {
        
        normalWindow.SetActive(true);

        
        if (shatterAudioSource == null)
        {
            Debug.LogError("Shatter AudioSource is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBroken && other.CompareTag("Player"))
        {
            BreakWindow();
        }
    }

    private void BreakWindow()
    {
        
        normalWindow.SetActive(false);  
        brokenWindow.SetActive(true);   
        isBroken = true;                
        Debug.Log("Window broken!");    

        
        if (shatterSound != null && shatterAudioSource != null)
        {
            shatterAudioSource.PlayOneShot(shatterSound); 
        }

        Debug.Log("Window broken!");
    }
}