﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class CameraShake : MonoBehaviour
    {
        public IEnumerator shake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            transform.localPosition = originalPos;
        }
    }
}
