using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnduranceBarManager : MonoBehaviour
{
    public Slider enduranceSlider;

    public Image EnduranceBarFill;

    public Color BaseColor = new Color(0, 131/255, 15/255);
    public Color RegenColor = new Color(0, 131/255, 15/255, 128 / 255);

    private Coroutine _blinkCoroutine = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //EnduranceBarFill.color.a += Mathf.Cos()
    }

    public void SetEndurance(float amount)
    {
        enduranceSlider.value = amount;
    }

    public void SetRegenState(bool regen)
    {
        if (regen)
        {
            EnduranceBarFill.color = RegenColor;
            _blinkCoroutine = StartCoroutine(BlinkCoRoutine(15f, 0.7f, 1f));
        }
        else
        {
            EnduranceBarFill.color = BaseColor;
            if (null != _blinkCoroutine)
            {
                StopCoroutine(_blinkCoroutine);
                _blinkCoroutine = null;
            }
        }
        
    }

    private IEnumerator BlinkCoRoutine(float blinkPerSec, float min = 0, float max = 1)
    {
        float i = 0;
        while (true)
        {
            Color color = EnduranceBarFill.color;
            float alpha = min + ((Mathf.Cos(i) + 1) / 2) * (max - min);
            EnduranceBarFill.color = new Color(color.r, color.g, color.b, alpha);
            i += blinkPerSec * Time.deltaTime;
            yield return null;
        }
    }
}
