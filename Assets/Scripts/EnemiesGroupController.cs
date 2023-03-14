using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesGroupController : MonoBehaviour
{
    private const float SpeedMultiplication = 0.1f;

    [SerializeField] private BaseEnemy m_EnemyPrefab;
    [SerializeField] private BaseEnemy m_ShootingEnemyPrefab;
    [SerializeField] private int m_Cols;
    [SerializeField] private int m_Rows;
    [SerializeField] private int m_Space;
    [SerializeField] private float m_Speed;
    [SerializeField] private RectTransform m_ParentRectTransform;

    private float m_DefaultSpeed;
    private readonly List<BaseEnemy> m_Enemies = new();
    private Bounds m_Bounds;
    private Vector2 m_EnemySize;
    private bool m_IsPause = true;
    private int m_MoveDirection = 1;
    private RectTransform m_RectTransform;

    private void Awake()
    {
        m_DefaultSpeed = m_Speed;
        m_EnemySize = (m_EnemyPrefab.transform as RectTransform).sizeDelta;
        m_RectTransform = transform as RectTransform;
    }


    private void FixedUpdate()
    {
        if (m_IsPause) return;

        MoveGroup();
        ProcessBounds();
    }

    public event Action OnAllEnemiesDied;

    public event Action<int> OnEnemyDeath;

    private void ProcessBounds()
    {
        var position = m_RectTransform.anchoredPosition;
        if (m_RectTransform.anchoredPosition.x + m_Bounds.RightX > m_ParentRectTransform.rect.width)
        {
            position.y -= m_Space;
            position.x = m_ParentRectTransform.rect.width - m_Bounds.RightX;
            m_MoveDirection = -1;
            m_Speed += m_Speed * SpeedMultiplication;
        }
        else if (m_RectTransform.anchoredPosition.x - m_Bounds.LeftX < 0)
        {
            position.y -= m_Space;
            position.x = m_Bounds.LeftX;
            m_MoveDirection = 1;
            m_Speed += m_Speed * SpeedMultiplication;
        }

        m_RectTransform.anchoredPosition = position;
    }


    private void MoveGroup()
    {
        var position = m_RectTransform.anchoredPosition;
        position.x += m_Speed * m_MoveDirection * Time.fixedDeltaTime;
        m_RectTransform.anchoredPosition = position;
    }

    public void CreateEnemies()
    {
        var enemyPosition = new Vector2(0, 0);
        for (var i = 0; i < m_Rows; i++)
        {
            for (var j = 0; j < m_Cols; j++)
            {
                CreateEnemyAtPosition(enemyPosition, new Vector2Int(j + 1, i + 1));
                enemyPosition.x += m_Space;
            }

            enemyPosition.y -= m_Space;
            enemyPosition.x = 0;
        }

        ResizeBounds(true);
    }

    private void CreateEnemyAtPosition(Vector2 enemyPosition, Vector2Int gridPosition)
    {
        var enemyPrefab = GetEnemyPrefab(gridPosition);
        var enemy = Instantiate(enemyPrefab, transform);
        enemy.Init(enemyPosition, gridPosition);
        enemy.OnDeath += OnDeath;
        m_Enemies.Add(enemy);

        void OnDeath(BaseEnemy baseEnemy)
        {
            baseEnemy.OnDeath -= OnDeath;
            m_Enemies.Remove(baseEnemy);
            ResizeBounds(false);
            OnEnemyDeath?.Invoke(baseEnemy.Score);

            if (m_Enemies.Count == 0) OnAllEnemiesDied?.Invoke();
        }
    }

    private BaseEnemy GetEnemyPrefab(Vector2Int gridPosition)
    {
        if ((gridPosition.x == 1 || gridPosition.x == m_Cols) && gridPosition.y is 2 or 4) return m_ShootingEnemyPrefab;

        return m_EnemyPrefab;
    }

    public void Pause()
    {
        m_IsPause = true;
        m_Enemies.ForEach(x => x.Pause());
    }

    public void UnPause()
    {
        m_IsPause = false;
        m_Enemies.ForEach(x => x.Unpause());
    }

    private void ResizeBounds(bool resizeRect)
    {
        if (m_Enemies.Count == 0) return;

        var biggestX = m_Enemies.Max(x => x.GridPosition.x);
        var smallestX = m_Enemies.Min(x => x.GridPosition.x);

        if (resizeRect)
        {
            var biggestY = m_Enemies.Max(x => x.GridPosition.y);
            var size = new Vector2(biggestX * m_EnemySize.x + (biggestX - 1) * (m_Space - m_EnemySize.x),
                biggestY * m_EnemySize.x + (biggestY - 1) * (m_Space - m_EnemySize.y));
            m_RectTransform.sizeDelta = size;
        }

        var halfCols = m_Cols / 2f;
        var space = m_Space - m_EnemySize.x;
        var halfSpace = space / 2f;
        m_Bounds.LeftX = m_EnemySize.x * (halfCols - smallestX + 1) + space * (halfCols - smallestX) +
                         halfSpace;
        m_Bounds.RightX = m_EnemySize.x * (biggestX - halfCols) + space * (biggestX - halfCols - 1) + halfSpace;
    }

    public void Restart()
    {
        m_Enemies.ForEach(x =>
        {
            x.Restart();
            Destroy(x.gameObject);
        });

        m_Enemies.Clear();
        m_Speed = m_DefaultSpeed;
        m_RectTransform.anchoredPosition = new Vector2(m_ParentRectTransform.rect.width / 2, 0);
        CreateEnemies();
    }


    private struct Bounds
    {
        public float LeftX;
        public float RightX;
    }
}