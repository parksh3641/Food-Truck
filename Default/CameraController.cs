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
    public Transform cPoint;

    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private bool shake = false;
    public bool isA = false;
    public bool isB = false;
    public bool isC = false;

    public GameManager gameManager;

    private void Awake()
    {
        Camera.main.transform.position = aPoint.position;
        Camera.main.transform.rotation = aPoint.rotation;

        isA = false;
        isB = false;
        isC = false;
    }

    private void Update()
    {
        if (!shake)
        {
            if (isA)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, aPoint.position, ref velocity, smoothTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, aPoint.rotation, smoothTime * 0.5f);

                if (Vector3.Distance(aPoint.position, Camera.main.transform.position) < 0.03f)
                {
                    Camera.main.transform.position = aPoint.position;
                    Camera.main.transform.rotation = aPoint.rotation;

                    isA = false;
                    isB = false;
                    isC = false;

                    gameManager.isDelay_Camera = true;

                    Debug.Log("Go To A Point End");
                }
            }

            if (isB)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, bPoint.position, ref velocity, smoothTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, bPoint.rotation, smoothTime * 0.5f);

                if (Vector3.Distance(bPoint.position, Camera.main.transform.position) < 0.03f)
                {
                    Camera.main.transform.position = bPoint.position;
                    Camera.main.transform.rotation = bPoint.rotation;

                    isA = false;
                    isB = false;
                    isC = false;

                    gameManager.isDelay_Camera = true;

                    Debug.Log("Go To B Point End");
                }
            }

            if (isC)
            {
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cPoint.position, ref velocity, smoothTime);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, cPoint.rotation, smoothTime * 0.5f);

                if (Vector3.Distance(cPoint.position, Camera.main.transform.position) < 0.03f)
                {
                    Camera.main.transform.position = cPoint.position;
                    Camera.main.transform.rotation = cPoint.rotation;

                    isA = false;
                    isB = false;
                    isC = false;

                    Debug.Log("Go To C Point End");
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
        isB = false;
        isC = false;
        isA = true;

        SoundManager.instance.PlaySFX(GameSfxType.Screen_Out);

        Debug.Log("Go To A Point");
    }

    public void GoToB()
    {
        isA = false;
        isC = false;
        isB = true;

        SoundManager.instance.PlaySFX(GameSfxType.Screen_In);

        Debug.Log("Go To B Point");
    }

    public void GoToC()
    {
        isA = false;
        isB = false;
        isC = true;

        SoundManager.instance.PlaySFX(GameSfxType.Screen_In);

        Debug.Log("Go To C Point");
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
