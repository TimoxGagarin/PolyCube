using UnityEngine;
public class LogoAnimation : MonoBehaviour{
    public float MaxSize;
    public float delta;
    void Update(){
        if (transform.localScale.x <= MaxSize){
            transform.localScale = new Vector2(transform.localScale.x + delta, transform.localScale.y + delta);
        }
    }
}
