using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidsManager
{
    public System.Action _asteroidDestroyed;

    private ObjectPool<Asteroid> _asteroidPool;
    private Sprite _sprite;

    public int _timer;
    int Timer
    {
        get { return _timer += 1; }
        set{ ; }
    }

    /// <summary>
    /// Make asteroids list, load sprite and add event
    /// </summary>
    public AsteroidsManager()
    {
        _asteroidPool = new ObjectPool<Asteroid>();
        _sprite = Resources.Load<Sprite>("Sprites/Asteroid");
        SpawnAsteroid(Random.Range(8, 20), 1);
    }

    public void PhysicsUpdate()
    {
        foreach(Asteroid asteroid in _asteroidPool._activePool) 
        {
            asteroid.PhysicsUpdate();
        }

        CheckAsteroidPosition();
    }
    
    /// <summary>
    /// Function called if asteroid is destroyed
    /// </summary>
    void OnAsteroidDestroyed(Asteroid asteroid)
    {
        _asteroidPool.ReturnObjectToInactive(asteroid);

        SpawnAsteroid(2, asteroid.Size / 2, asteroid.ThisAsteroid.transform.position);
    }

    /// <summary>
    /// Function that can be called to spawn any number if asteroids with a set size
    /// </summary>
    public void SpawnAsteroid(int amount, float size)
    {
        for (int i = 0; i < amount; i++) 
        {
            Asteroid asteroid = _asteroidPool.RequestItem();
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed); //TODO test
        }
    }

    /// <summary>
    /// Function that can be called to spawn any number if asteroids with a set size and start position
    /// </summary>
    public void SpawnAsteroid(int amount, float size, Vector3 startPos)
    {
        for (int i = 0; i < amount; i++)
        {
            Asteroid asteroid = _asteroidPool.RequestItem(size, startPos);
            EventManager<Asteroid>.AddListener(EventType.ON_ASTEROID_DESTROYED, OnAsteroidDestroyed);
        }
    }

    private void CheckAsteroidPosition()
    {
        if (Timer >= 30)
        {
            foreach (Asteroid asteroid in _asteroidPool._activePool.Reverse<Asteroid>())
            {
                if (Screen.width < Camera.main.WorldToScreenPoint(asteroid.ThisAsteroid.transform.position).x - 130 ||
                    0 > Camera.main.WorldToScreenPoint(asteroid.ThisAsteroid.transform.position).x + 130)
                {
                    _asteroidPool.ReturnObjectToInactive(asteroid);
                    SpawnAsteroid(1, 1);
                    Debug.Log("Asteroid despawned");
                }
                if (Screen.height < Camera.main.WorldToScreenPoint(asteroid.ThisAsteroid.transform.position).y - 130 ||
                    0 > Camera.main.WorldToScreenPoint(asteroid.ThisAsteroid.transform.position).y + 130)
                {
                    _asteroidPool.ReturnObjectToInactive(asteroid);
                    SpawnAsteroid(1, 1);
                    Debug.Log("Asteroid despawned");
                }
            }

            _timer = 0;
        }
    }
}
