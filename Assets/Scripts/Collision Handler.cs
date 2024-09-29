using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the starting point. Get going!");
                break;
            case "Finish":
                Debug.Log("Ship has reached destination. Stage complete.");
                break;
            default:
                Debug.Log("Ship has crashed!");
                break;
        }
    }
}
