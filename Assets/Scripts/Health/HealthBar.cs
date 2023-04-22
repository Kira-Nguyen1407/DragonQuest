using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private Health objectHealth;
    
    [SerializeField] private Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        // objectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        currentHealthBar.fillAmount = objectHealth.startingHealth/objectHealth.totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = objectHealth.currentHealth/objectHealth.totalHealth;
    }
}
