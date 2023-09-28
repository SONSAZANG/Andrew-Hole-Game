using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float screenPositionFollowThreshold;
    [SerializeField] private float moveSpeed;
    private Vector3 clickedScreenPosition;

    private void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // 시작 지점과 이동한 지점의 차이값 구하기
            Vector3 difference = Input.mousePosition - clickedScreenPosition;

            // 차이값을 정규화 시켜서 방향 구하기
            Vector3 direction = difference.normalized;

            float maxScreenDistance = screenPositionFollowThreshold * Screen.height;

            if (difference.magnitude > maxScreenDistance)
            {
                clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }

            difference /= Screen.height;

            difference.z = difference.y;
            difference.y = 0;

            Vector3 targetPosition = transform.position + difference * moveSpeed * Time.deltaTime;

            transform.position = targetPosition;
        }
    }
}
