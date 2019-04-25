
using UnityEngine;
public class AutoDestroy : MonoBehaviour
{
    public float time = 1.0f;
    public ScaleMode mode = ScaleMode.Scaled;
    public enum ScaleMode
    {
        Scaled,
        UnScaled,

    }

    // Start is called before the first frame update
    void Start()
    {
       

    }

    void Update()
    {
        if(time < 0 )
        {
            GameObject.Destroy(gameObject);
        }

        switch (mode)
        {
            case ScaleMode.Scaled:
                {
                    time -= Time.deltaTime;
                }
                break;
            case ScaleMode.UnScaled:
                {
                    time -= Time.unscaledDeltaTime;
                }
                break;
        }

      
    }

}
