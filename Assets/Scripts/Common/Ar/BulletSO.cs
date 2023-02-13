using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ar/Bullet"), System.Serializable]
public class BulletSO : ScriptableObject
{
    public float bulletDamage;
    public float bulletSpeed;
    public bool isEnemyBullet;
    public bool isPenetrate;
}