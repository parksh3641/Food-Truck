using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Shake")]
    [Range(0, 10)]
    public float shakeTime;
    [Range(0, 10)]
    public float shakeAmount;
    [Range(0, 10)]
    private bool shaking;

    [Header("Point")]
    public Transform aPoint;
    public Transform bPoint;

    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private bool shake = false;
    private bool isA = false;
    private bool isB = false;

    public GameManager gameManager;

    private void Awake()
    {
        Camera.main.transform.position = aPoint.position;
        Camera.main.transform.rotation = aPoint.rotation;
    }

    private void Update()
    {
        if (!shake)
        {
            if (isA)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, aPoint.position, ref velocity, smoothTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, aPoint.rotation, smoothTime * 0.5f);

                if (Vector3.Distance(aPoint.position, Camera.main.transform.position) < 0.01f)
                {
                    Camera.main.transform.position = aPoint.position;
                    Camera.main.transform.rotation = aPoint.rotation; 

                    isA = false;

                    gameManager.isDelay_Camera = true;
                }
            }

            if (isB)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, bPoint.position, ref velocity, smoothTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, bPoint.rotation, smoothTime * 0.5f);

                if (Vector3.Distance(bPoint.position, Camera.main.transform.position) < 0.01f)
                {
                    Camera.main.transform.position = bPoint.position;
                    Camera.main.transform.rotation = bPoint.rotation;

                    isB = false;

                    gameManager.isDelay_Camera = true;
                }
            }
        }
    }

    public void Shake()
    {
        if (shake) return;

        shake = true;

        StartCoroutine(ShakeCoroution());

        Debug.Log("Camera Shake");
    }

    public void GoToA()
    {
        if (isA || isB) return;

        isA = true;

        Debug.Log("Go To A Point");
    }

    public void GoToB()
    {
        if (isA || isB) return;

        isB = true;

        Debug.Log("Go To B Point");
    }

    IEnumerator ShakeCoroution()
    {
        float timer = 0;
        Vector3 trans = Camera.main.transform.position;
        while (timer <= shakeTime)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            Camera.main.transform.position += new Vector3(shakePos.x, shakePos.y, 0);
            timer += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = trans;
        shake = false;
    }
}
