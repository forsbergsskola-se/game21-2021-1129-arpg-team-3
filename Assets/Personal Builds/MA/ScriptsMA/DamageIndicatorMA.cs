using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicatorMA : MonoBehaviour
{

    public Text text;
    public float lifetime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;


    private Vector3 inipos;
    private Vector3 targetPos;
    private float timer;
    
    
    void Start()
    {
        transform.LookAt(transform.position-Camera.main.transform.position);
        
        inipos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = inipos +  new Vector3(0,dist,0);
        transform.localScale = Vector3.up;
    }


    void Update()
    {
        timer += Time.deltaTime;
        float fraction = lifetime / 2f;
        if (timer > lifetime) Destroy(gameObject);

        else if (timer > fraction)
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        {
            transform.position = Vector3.Lerp(inipos, targetPos, (timer / lifetime));
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(0.05f, 0.05f, 0), (timer / lifetime));

        }
    }

        public void SetDamageText(int damage)
        {
            text.text = damage.ToString();
        }
    }

