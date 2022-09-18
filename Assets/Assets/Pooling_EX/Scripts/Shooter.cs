using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject _BulletPrefab;

    private Camera _MainCam;

    private IObjectPool<Bullet> _Pool;
    private void Awake()
    {

        _Pool = new ObjectPool<Bullet>(CreatBullet, onGetBullet, onReleaseBullet, OnDestroyBullet, maxSize: 30);
    }
    void Start()
    {
        _MainCam = Camera.main;
    } 
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hitResult;
            if(Physics.Raycast(_MainCam.ScreenPointToRay(Input.mousePosition), out hitResult))
            {
                var direction = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
                // 총알을 새로 생성해서 사용
                //var bullet = Instantiate(_BulletPrefab, transform.position + direction.normalized, Quaternion.identity).GetComponent<Bullet>();
                var bullet = _Pool.Get();
                //bullet.transform.position = transform.position + direction.normalized;
                bullet.Shoot(direction.normalized);
            }
        }
    }

    private Bullet CreatBullet()
    {
        Bullet bullet = Instantiate(_BulletPrefab).GetComponent<Bullet>();
        bullet.SetManagedPool(_Pool);
        return bullet;
    }
    private void onGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    private void onReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}