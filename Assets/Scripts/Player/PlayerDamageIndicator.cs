using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageIndicator : MonoBehaviour
{

    public Text text;
    public float lifetime = 2f;
    public float minDist = 0f;
    public float maxDist = 0f;


    private Vector3 inipos;
    private Vector3 targetPos;
    private float timer;
    
    
    void Awake() 
    {
        inipos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = inipos +  new Vector3(dist,dist,0);
        transform.localScale = Vector3.up;
    }


    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        timer += Time.deltaTime;
        float fraction = lifetime / 2f;
        if (timer > lifetime) Destroy(gameObject);

        else if (timer > fraction)
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        {
            transform.position = Vector3.Lerp(inipos, targetPos, (timer / lifetime));
            transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(0.02f, 0.02f, 0), (timer / lifetime));

        }
    }

    public void SetPlayerDamageText(int damage)
    {
        text.text = (damage).ToString();
    }
}
