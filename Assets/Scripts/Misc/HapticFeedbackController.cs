using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class HapticConfiguration
{
    [Range(0f, 1f)]
    public float hapticIntensity = 0.1f;
    public float hapticDuration = 1f;
    public XRBaseController referencedBaseController;
}

public class HapticFeedbackController : MonoBehaviour
{
    public HapticConfiguration[] hapticConfigurations;

    public void CallForHapticFeedback()
    {
        foreach (var hapticConfiguration in hapticConfigurations)
        { 
            OnHapticCall(hapticConfiguration.referencedBaseController, hapticConfiguration.hapticIntensity, hapticConfiguration.hapticDuration);
        }
    }

    private void OnHapticCall(XRBaseController _controller, float _intensity, float _duration)
    {
        if(_duration > 0f)
            _controller.SendHapticImpulse(_intensity, _duration);
    }
    
}
