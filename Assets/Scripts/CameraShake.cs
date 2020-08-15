using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator ShakeOnce(float time, float magnitude)
    {
        Vector3 originPosition = transform.position;

        float totalTime = 0f;

        while (totalTime < time)
        {
            totalTime += Time.deltaTime;

            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            float z = Random.Range(-1, 1) * magnitude;

            transform.position += new Vector3(x, y, originPosition.z);

            yield return null;
        }

        transform.position = originPosition;
    }
}
