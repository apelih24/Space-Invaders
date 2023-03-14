using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float m_Speed;

    private bool m_IsPaused;
    private int m_MoveDirection;
    private RectTransform m_RectTransform;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        if (m_IsPaused) return;

        var pos = transform.position;
        pos.y += m_MoveDirection * m_Speed * Time.fixedDeltaTime;
        transform.position = pos;
    }

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }

    public event Action<BulletController> OnDestroyEvent;

    public void SetupDirection(int direction)
    {
        m_MoveDirection = direction;
    }

    public void UnPause()
    {
        m_IsPaused = false;
    }

    public void Pause()
    {
        m_IsPaused = true;
    }
}