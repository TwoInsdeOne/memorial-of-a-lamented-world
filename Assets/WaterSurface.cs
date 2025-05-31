using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wave
{
    public float originX;
    public float time;
    public float direction; // +1 ou -1
    public float initialAmplitude;
    public float frequency;
    public float speed;
    public float maxDistance;
    private float softening;
    public Wave(float originX, float direction, float amplitude, float frequency, float speed)
    {
        this.originX = originX;
        this.direction = direction;
        this.initialAmplitude = amplitude;
        this.frequency = frequency;
        this.speed = speed;
        this.time = 0;
        this.softening = 0;
    }

    public float GetDisplacement(float x)
    {
        float distance = Mathf.Abs(x - (originX + direction * speed * time));
        float decay = Mathf.Exp(-distance * 0.5f); // controla quanto a onda some com a distância
        return decay * initialAmplitude*softening * Mathf.Sin(frequency * distance - speed * time);
    }

    public void Update(float deltaTime)
    {
        softening = Mathf.Min(softening + deltaTime*2f, 1);
        time += deltaTime;
    }
    public bool isDead(float maxDistance)
    {
        return this.speed * this.time > maxDistance;
    }
}

public class WaterSurface : MonoBehaviour
{
    public Wave waveR;
    public Wave waveL;
    public Transform player;

    public List<Wave> activeWaves = new List<Wave>();
    public float maxDistance;
    public float stayInterval;
    private SpecialGeneretor sg;
    public float propagationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sg = GetComponent<SpecialGeneretor>();
        maxDistance = sg.dimensions.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = activeWaves.Count - 1; i >= 0 ; i--)
        {
            activeWaves[i].Update(Time.deltaTime);
            if (activeWaves[i].isDead(maxDistance))
            {
                //activeWaves.RemoveAt(i);
            }
        }
        stayInterval -= Time.deltaTime;
    }

    public float GetSurfaceY(float x)
    {
        float y = 0;
        foreach (var wave in activeWaves)
        {
            y += wave.GetDisplacement(x);
        }
        return y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            float velocity = collision.GetComponent<Rigidbody2D>().velocity.magnitude/12.0f;
            float x1 = collision.transform.position.x + 1;
            float x2 = collision.transform.position.x - 1;
            activeWaves.Add(new Wave(x1, 1f, Mathf.Max( 0.5f*velocity, 0.1f), 3f, propagationSpeed));
            activeWaves.Add(new Wave(x2, -1f, Mathf.Max(0.5f * velocity, 0.1f), 3f, propagationSpeed));
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && stayInterval <= 0)
        {
            float velocity = collision.GetComponent<Rigidbody2D>().velocity.magnitude / 12.0f;
            float x1 = collision.transform.position.x + 1;
            float x2 = collision.transform.position.x - 1;
            activeWaves.Add(new Wave(x1, 1f, Mathf.Max(0.5f * velocity, 0.1f), 2f, propagationSpeed));
            activeWaves.Add(new Wave(x2, -1f, Mathf.Max(0.5f * velocity, 0.1f), 2f, propagationSpeed));
            stayInterval = 3f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float velocity = collision.GetComponent<Rigidbody2D>().velocity.magnitude / 12.0f;
            float x1 = collision.transform.position.x + 1;
            float x2 = collision.transform.position.x - 1;
            activeWaves.Add(new Wave(x1, 1f, Mathf.Max(0.5f * velocity, 0.1f), 3f, propagationSpeed));
            activeWaves.Add(new Wave(x2, -1f, Mathf.Max(0.5f * velocity, 0.1f), 3f, propagationSpeed));
        }
    }

}
