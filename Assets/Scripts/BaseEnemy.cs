using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private RectTransform m_RectTransform;

    public Vector2Int GridPosition { get; private set; }

    public virtual int Score => 1;

    private void Awake()
    {
        m_RectTransform = transform as RectTransform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) OnBulletCollision(other);
    }

    public virtual void Pause()
    {
    }

    public virtual void Unpause()
    {
    }

    public virtual void Restart()
    {
    }

    public event Action<BaseEnemy> OnDeath;

    private void OnBulletCollision(Collider2D other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
        OnDeath?.Invoke(this);
    }

    public void Init(Vector2 position, Vector2Int gridPosition)
    {
        m_RectTransform.anchoredPosition = position;
        GridPosition = gridPosition;
    }
}