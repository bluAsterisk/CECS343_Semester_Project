using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    // Camera pointing at target
    public Transform target; // What camera is following
    private Vector3 targetCamPos; // Position of the camera following target

    public float smoothing; // smooths camera following target
    
    // Bounds where the camera should not show
    public float lowerBound;
    public float leftBound;
    public float rightBound;
    public float upperBound;

    //Leaves buffer zone where camera does not move to follow target.
    public float bufferY, bufferX;

    //public float camWidthHalf, camHeightHalf; // Leave here to check out the values of half camera size.
    /*
     * These are bounds for center of camera.
     * So camera does not stop at the bounds of map
     * but rather at the edges of the camera.
     */
    private float camXLB, camXRB, camYUB, camYLB;
    
    // Start is called before the first frame update
    void Start()
    {
        targetCamPos = transform.position;

        /*
         * Used to find edges for the camera that should
         * not show things outside of map.
         */
        float camWidthHalf, camHeightHalf;
        // Assuming that Half of Camera Height is always equal to value 1; If not use Camera.main.orthographicsize;
        camHeightHalf = Mathf.Abs(transform.position.z) * Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2);
        // Camera aspect is width/height. Width is also half.
        camWidthHalf = Camera.main.aspect * camHeightHalf;
        camXLB = leftBound + camWidthHalf;
        camXRB = rightBound - camWidthHalf;
        camYUB = upperBound - camHeightHalf;
        camYLB = lowerBound + camHeightHalf;

        // Depending on field of view the smoothing will slow down.
        /// Probably will remove if testing it not good
        smoothing = smoothing / (Camera.main.fieldOfView / 10);
    }

    // Uses FixedUpdate because body is a physics object.
    // When not using FixedUpdate the camera would leave after images as it follows target.
    void FixedUpdate()
    {
        UpdateTargetCamPos();
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }
    
    // Updates Target Camera Position and make sure that the camera does not go outside of map.
    void UpdateTargetCamPos()
    {
        targetCamPos.x = Mathf.Clamp(TargetXFollow(), camXLB, camXRB);
        targetCamPos.y = Mathf.Clamp(TargetYFollow(), camYLB, camYUB);
    }

    /*
     * If target movings outside of buffer zone then the camera will
     * be dragged by target by adding or subtracting the buffer length.
     */
    float TargetYFollow()
    {
        // if target position is less than camera - buffer then add buffer to target position. So drag camera behind target.
        if (target.transform.position.y < transform.position.y - bufferY)
        {
            return target.transform.position.y + bufferY;
        }
        // if target position is greater than camera + buffer then subtract buffer to target position. So drag camera behind target.
        else if (target.transform.position.y > transform.position.y + bufferY)
        {
            return target.transform.position.y - bufferY;
        }

        return transform.position.y;
    }

    /*
     * If target movings outside of buffer zone then the camera will
     * be dragged by target by adding or subtracting the buffer length.
     */
    float TargetXFollow()
    {
        // if target position is less than camera - buffer then add buffer to target position. So drag camera behind target.
        if (target.transform.position.x < transform.position.x - bufferX)
        {
            return target.transform.position.x + bufferX;
        }
        // if target position is greater than camera + buffer then subtract buffer to target position. So drag camera behind target.
        else if (target.transform.position.x > transform.position.x + bufferX)
        {
            return target.transform.position.x - bufferX;
        }

        return transform.position.x;
    }
}
