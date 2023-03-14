using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int HealthAmount = 3;
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private RectTransform m_FieldRectTransform;
    [SerializeField] private BulletController m_BulletPrefab;
    [SerializeField] private HealthBarController m_HealthBarController;
    private readonly List<BulletController> m_Bullets = new();
    private int m_CurrentHealth = HealthAmount;
    private bool m_IsPause;
    private RectTransform m_RectTransform;

    private void Awake()
    {
        m_RectTransform = transform as RectTransform;
    }

    private void Update()
    {
        if (m_IsPause) return;

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    private void FixedUpdate()
    {
        if (m_IsPause) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1);
            ProcessLeftBound();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(1);
            ProcessRightBound();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
            OnGameOver?.Invoke();
        else if (col.CompareTag("EnemyBullet")) OnBulletCollision(col);
    }

    public event Action OnGameOver;
    public event Action OnTakeDamage;

    public void Restart()
    {
        PlaceAtStartPosition();
        m_HealthBarController.Setup();
        m_CurrentHealth = HealthAmount;
        m_Bullets.ForEach(x =>
        {
            x.OnDestroyEvent -= OnBuleltDestroy;
            Destroy(x.gameObject);
        });
    }

    private void PlaceAtStartPosition()
    {
        m_RectTransform.anchoredPosition =
            new Vector2(m_FieldRectTransform.rect.width / 2, m_RectTransform.anchoredPosition.y);
    }

    private void OnBulletCollision(Collider2D bullet)
    {
        Destroy(bullet.gameObject);
        m_HealthBarController.TakeDamage();
        m_CurrentHealth--;
        if (m_CurrentHealth == 0)
        {
            OnGameOver?.Invoke();
            return;
        }

        PlaceAtStartPosition();
        OnTakeDamage?.Invoke();
    }

    private void Shoot()
    {
        var bullet = Instantiate(m_BulletPrefab, transform.parent);
        bullet.SetupDirection(1);
        bullet.transform.position = transform.position;
        bullet.OnDestroyEvent += OnBuleltDestroy;
        m_Bullets.Add(bullet);
    }

    private void OnBuleltDestroy(BulletController bullet)
    {
        bullet.OnDestroyEvent -= OnBuleltDestroy;
        m_Bullets.Remove(bullet);
    }

    private void ProcessRightBound()
    {
        if (m_RectTransform.anchoredPosition.x + m_RectTransform.rect.xMax > m_FieldRectTransform.rect.width)
            SetPlayerXPosition(m_FieldRectTransform.rect.width - m_RectTransform.sizeDelta.x / 2);
    }

    private void ProcessLeftBound()
    {
        if (m_RectTransform.anchoredPosition.x + m_RectTransform.rect.xMin < 0)
            SetPlayerXPosition(m_RectTransform.sizeDelta.x / 2);
    }

    private void SetPlayerXPosition(float xPosition)
    {
        var position = m_RectTransform.anchoredPosition;
        position.x = xPosition;
        m_RectTransform.anchoredPosition = position;
    }

    private void Move(int direction)
    {
        var position = m_RectTransform.anchoredPosition;
        position.x += direction * m_MoveSpeed * Time.fixedDeltaTime;
        m_RectTransform.anchoredPosition = position;
    }

    public void UnPause()
    {
        m_IsPause = false;
        m_Bullets.ForEach(x => x.UnPause());
    }

    public void Pause()
    {
        m_IsPause = true;
        m_Bullets.ForEach(x => x.Pause());
    }
}