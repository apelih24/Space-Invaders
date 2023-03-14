using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_HealthPoints;
    private int m_CurrentHealthPointIndex;

    public void Setup()
    {
        foreach (var healthPoint in m_HealthPoints)
        {
            healthPoint.SetActive(true);
        }

        m_CurrentHealthPointIndex = 0;
    }

    public void TakeDamage()
    {
        m_HealthPoints[m_CurrentHealthPointIndex].SetActive(false);
        m_CurrentHealthPointIndex = Mathf.Clamp(m_CurrentHealthPointIndex + 1, 0, m_HealthPoints.Length - 1);
    }
}