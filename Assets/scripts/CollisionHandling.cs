using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.mass = 0f;
            
        }
    }

    // IEnumerator WaitForReset(float waitTime)
    // {
    //     // Chờ trong khoảng thời gian cụ thể
    //     yield return new WaitForSeconds(waitTime);

    //     ballRigidbody.mass = 0f;
    //     // Thực hiện hành động
    //     Debug.Log("Hành động đã được thực hiện sau" + waitTime + " giây!");
    // }
    // IEnumerator WaitForResetTriger(float waitTime, string triggerName)
    // {

    //     // Chờ trong khoảng thời gian cụ thể
    //     yield return new WaitForSeconds(waitTime);

    //     // Thực hiện hành động
    //     Debug.Log("Hành động đã được thực hiện sau" + waitTime + " giây!");
    // }
}
