using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEnemy
{
    private const int ShootDelay = 2;
    [SerializeField] private BulletController m_BulletController;
    private readonly List<BulletController> m_Bullets = new();
    private bool m_IsPaused;

    public override int Score => 2;

    public override void Restart()
    {
        m_Bullets.ForEach(x =>
        {
            x.OnDestroyEvent -= OnBulletDestroy;
            Destroy(x.gameObject);
        });
    }

    public override void Pause()
    {
        m_IsPaused = false;
        m_Bullets.ForEach(x => x.Pause());
        StopAllCoroutines();
    }

    public override void Unpause()
    {
        m_IsPaused = false;
        m_Bullets.ForEach(x => x.UnPause());
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        var wait = new WaitForSeconds(ShootDelay);
        while (!m_IsPaused)
        {
            yield return wait;
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(m_BulletController, transform.parent.parent);
        bullet.SetupDirection(-1);
        bullet.transform.position = transform.position;
        bullet.OnDestroyEvent += OnBulletDestroy;
        m_Bullets.Add(bullet);
    }

    private void OnBulletDestroy(BulletController bulletController)
    {
        bulletController.OnDestroyEvent -= OnBulletDestroy;
        m_Bullets.Remove(bulletController);
    }
}