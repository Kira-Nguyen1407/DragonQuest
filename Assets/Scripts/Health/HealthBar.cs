using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private Health healthObject;
    
    [SerializeField] private Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        // objectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        currentHealthBar.fillAmount = healthObject.startingHealth/healthObject.totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = healthObject.currentHealth/healthObject.totalHealth;
    }
}
