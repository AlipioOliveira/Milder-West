using UnityEngine;

//As texturas têm que ter a opçao "Alpha Is Transpanêncy" ligada
//E os Materials têm que ter seleciondado "Fade" ou "Transparent" na opçâo "Rendering Mode"
[RequireComponent(typeof(MeshRenderer))]
public class FadeObject : MonoBehaviour {

    private GameObject[] Children;
    private float newAlpha = 0f;
    
    [SerializeField]
    [Range(0f,10f)]
    private float timeToStartFading = 1f;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float fadeDuration = 5f;

    void Start ()
    {      
        Children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)        
           Children[i] = transform.GetChild(i).gameObject;              
    }
    
    void Update()
    {        
        timeToStartFading -= Time.deltaTime;

        if (newAlpha < 0.99 && timeToStartFading <= 0f)
            newAlpha += (1f / fadeDuration) * Time.deltaTime;
        else if(newAlpha >= 0.99) Destroy(gameObject);

        for (int i = 0; i < Children.Length; i++)
        {
            MeshRenderer renderer = Children[i].GetComponent<MeshRenderer>();
            Color originalColour = renderer.material.color;
            renderer.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, 1 - newAlpha);
        }
    }
    public void setValues(float _timeToStartFading, float _fadeDuration)
    {
        timeToStartFading = _timeToStartFading;
        fadeDuration = _fadeDuration;
    }
}
