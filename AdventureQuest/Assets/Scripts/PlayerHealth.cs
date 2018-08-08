using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float p_StartingHealth = 100f;
    public Slider p_Slider;
    public Image p_FillImage;
    public Color p_FullHealthColor = Color.red;
    public Color p_ZeroHealthColor = Color.white;

    private float p_CurrrentHealth;
    private bool p_Dead;


    // Use this for initialization
    private void OnEnable()
    {
        p_CurrrentHealth = p_StartingHealth;
        p_Dead = false;

        SetHealthUI();
    }

    private void Update()
    {
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        //amount of danage taken by enemies, will vary on enemy type

        p_CurrrentHealth -= amount;

        if (p_CurrrentHealth <= 0f && !p_Dead)
        {
            OnDeath();
        }
    }

    // Update is called once per frame
    private void SetHealthUI()
    {
        p_Slider.value = p_CurrrentHealth;
        p_FillImage.color = Color.Lerp(p_ZeroHealthColor, p_FullHealthColor, p_CurrrentHealth / p_StartingHealth);
    }

    private void OnDeath()
    {
        p_Dead = true;
        gameObject.SetActive(true);
        SceneManager.LoadScene("Menu");
    }
}
