using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    public float SpeedConst = 3F;
    public float Speed = 0F;
    public float JumpPower = 2F;
    public bool OnGround;

    public GameObject hp;
    public SpriteRenderer Accessorie;
    public Profil profil;

    public AudioClip[] Sounds;
    public AudioSource WalkSource;

    private Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
        try
        {
            GetComponent<SpriteRenderer>().sprite = profil.GetComponent<ButtonController>().skins[Profil.ChoosenSkin].Skinn;
        }
        catch(NullReferenceException)
        {
            GetComponent<SpriteRenderer>().sprite = profil.GetComponent<ButtonController>().skins[0].Skinn;
        }

        try
        {
            Accessorie.sprite = profil.GetComponent<ButtonController>().accessories[Profil.ChoosenAccessorie].Skinn;
        }
        catch (InvalidOperationException)
        {
            Accessorie.sprite = profil.GetComponent<ButtonController>().accessories[0].Skinn;
        }
        catch (NullReferenceException)
        {
            Accessorie.sprite = profil.GetComponent<ButtonController>().accessories[0].Skinn;
        }
        catch (IndexOutOfRangeException)
        {
            Accessorie.sprite = profil.GetComponent<ButtonController>().accessories[0].Skinn;
        }
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, GetComponent<Rigidbody2D>().velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            System.Random random = new System.Random();
            PlaySound(0);
            Profil.Money += random.Next(5)+1;
            PlayerPrefs.SetInt("Money", Profil.Money);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "DeathSpace")
        {
            SceneManager.LoadScene(Profil.lvl + 4);
        }
        if (collision.gameObject.tag == "SharpnessBush")
        {
            RemoveHp(0.05F);
            SpeedConst *= 0.75F;
            Speed *= 0.75F;
            JumpPower *= 0.5F;
        }
        if (collision.gameObject.tag == "Log")
        {
            Destroy(collision);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30000, 0));
        }
        if (collision.gameObject.tag == "Rock" && collision.gameObject.GetComponent<Rigidbody2D>().gravityScale == 1 && collision.isTrigger)
        {
            PlaySound(2);
            Destroy(collision);
            RemoveHp(0.3F);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            return;
        }
        if(collision.gameObject.tag == "Bee")
        {
            RemoveHp(0.3F);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Profil.Money += 5;
            PlayerPrefs.SetInt("Level", Profil.lvl);
            PlayerPrefs.SetInt("ChoosenSkin", Profil.ChoosenSkin);
            PlayerPrefs.SetInt("Money", Profil.Money);
            SceneManager.LoadScene(3);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "HpBox")
        {
            if (hp.GetComponentInChildren<Image>().fillAmount == 1F && transform.localScale.y == 1)
            {
                return;
            }

            if (hp.GetComponentInChildren<Image>().fillAmount > 0.75F && hp.GetComponentInChildren<Image>().fillAmount != 1)
            {
                Destroy(collision.gameObject);
                hp.GetComponentInChildren<Image>().fillAmount = 1F;
            }
            if (hp.GetComponentInChildren<Image>().fillAmount <= 0.75F)
            {
                Destroy(collision.gameObject);
                hp.GetComponentInChildren<Image>().fillAmount += 0.25F;
            }
            if(transform.localScale.y < 1F)
            {
                Destroy(collision.gameObject);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
            }

            hp.GetComponentInChildren<Text>().text = Math.Round(hp.GetComponentInChildren<Image>().fillAmount * 100).ToString();
        }
        if (collision.gameObject.tag == "Rock" && collision.gameObject.GetComponent<Rigidbody2D>().gravityScale == 0)
        {
            Destroy(collision.collider);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            return;
        }
        if (collision.gameObject.tag == "Log")
        {
            RemoveHp(0.25F);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SharpnessBush")
        {
            SpeedConst = SpeedConst * 4 / 3;
            Speed = Speed * 4 / 3;
            JumpPower *= 2;
        }
    }
    public void RemoveHp(float damage)
    {
        anim.SetTrigger("Hurt");
        PlaySound(1);
        hp.GetComponentInChildren<Image>().fillAmount -= damage;
        hp.GetComponentInChildren<Text>().text = Math.Round(hp.GetComponentInChildren<Image>().fillAmount*100).ToString();
        if (hp.GetComponentInChildren<Image>().fillAmount < 0.01)
            SceneManager.LoadScene(Profil.lvl + 4);
    }

    public void PlaySound(int numOfClip)
    {
        GetComponent<AudioSource>().clip = Sounds[numOfClip];
        GetComponent<AudioSource>().Play();
    }
    public void Right()
    {
        anim.SetBool("Running", true);
        if (OnGround)
            WalkSource.Play();
        if (Speed <= 0)
        {
            Speed = SpeedConst;
        }
    }
    public void Left()
    {
        anim.SetBool("Running", true);
        if (OnGround)
            WalkSource.Play();
        if (Speed >= 0)
        {
            Speed = -SpeedConst;
        }
    }
    public void ButtonUp()
    {
        anim.SetBool("Running", true);
        WalkSource.Stop();
        Speed = 0;
    }
    public void Jump()
    { 
        if (OnGround)
        {   
            anim.SetTrigger("Jump");
            PlaySound(2);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
    }
}
