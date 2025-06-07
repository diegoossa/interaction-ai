// 6/5/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Emotion Objects")]
    public GameObject mouthEmo;
    public GameObject mouthSpeech;

    [Header("Managers")]
    public Rob10ColorManager robotColorManager;
    public EmotionChanger emotionChanger;

    [Header("Animation Settings")]
    public int playCount = 1;

    private string animationName = "YourAnimationName";
    private bool battleIsActive = false;
    private int emoIndex = 0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleEmotionInput();
        HandleAnimations();
    }

    private void HandleEmotionInput()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            emoIndex = (emoIndex + 1) % 10;
            SetEmotion(emoIndex);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            emoIndex = (emoIndex - 1 + 10) % 10;
            SetEmotion(emoIndex);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetEmotion();
        }
    }

    private void HandleAnimations()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) TriggerEmotionAnimation("Angry", 2); // Sad
        if (Input.GetKeyDown(KeyCode.Alpha2)) TriggerEmotionAnimation("Cry", 8); // Cry
        if (Input.GetKeyDown(KeyCode.Alpha3)) TriggerEmotionAnimation("Thumb", 9); // Love
        if (Input.GetKeyDown(KeyCode.Alpha4)) TriggerEmotionAnimation("Win", 1); // Happy
        if (Input.GetKeyDown(KeyCode.Alpha5)) TriggerEmotionAnimation("DontKnow", 3); // Distrust
        if (Input.GetKeyDown(KeyCode.Alpha6)) TriggerEmotionAnimation("Hello", 0); // Neutral
        if (Input.GetKeyDown(KeyCode.Alpha7)) TriggerEmotionAnimation("Laught", 1); // Happy
        if (Input.GetKeyDown(KeyCode.Alpha8)) TriggerEmotionAnimation("LookingFor", 4); // Wonder

        if (Input.GetKeyDown(KeyCode.Alpha9)) StartDanceAnimation("Dance0");
        if (Input.GetKeyDown(KeyCode.Alpha0)) StartDanceAnimation("Dance1");

        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetBool("Talk", true);
            ToggleObjectActiveState();
            SetEmotion(0); // Neutral
        }
    }

    private void TriggerEmotionAnimation(string animation, int emotion)
    {
        anim.SetBool(animation, true);
        SetEmotion(emotion);
    }

    private void StartDanceAnimation(string danceName)
    {
        animationName = danceName;
        robotColorManager.isRainbowCycles = true;
        SetEmotion(1); // Happy
        StartCoroutine(PlayAnimationMultipleTimes());
    }

    public void SetEmotion(int emoNumber)
    {
        if (battleIsActive) emoNumber = 7; // Evil

        robotColorManager.ChangeBodyColor(emoNumber);
        emotionChanger.SetEmotionEyes(emoNumber);
        emotionChanger.SetEmotionMouth(emoNumber);
    }

    public void ResetEmotion()
    {
        SetEmotion(0); // Neutral
        anim.SetBool("reset", true);
    }

    public void Speech3End()
    {
        ToggleObjectActiveState();
    }

    private IEnumerator PlayAnimationMultipleTimes()
    {
        for (int i = 0; i < playCount; i++)
        {
            anim.SetBool(animationName, true);
            yield return new WaitForSeconds(playCount);
        }

        anim.SetBool(animationName, false);
        robotColorManager.isRainbowCycles = false;
        anim.SetBool("reset", true);
        ResetEmotion();
    }

    private void ToggleObjectActiveState()
    {
        if (mouthEmo != null && mouthSpeech != null)
        {
            mouthEmo.SetActive(!mouthEmo.activeSelf);
            mouthSpeech.SetActive(!mouthSpeech.activeSelf);
        }
        else
        {
            Debug.LogError("Mouth objects are not assigned!");
        }
    }
    
    public void StartSpeaking(float seconds)
    {
        StartCoroutine(SpeakCoroutine(seconds));
    }
    
    private IEnumerator SpeakCoroutine(float seconds)
    {
        if (mouthEmo != null && mouthSpeech != null)
        {
            anim.SetBool("Talk", true);
            ToggleObjectActiveState();
            SetEmotion(0); // Neutral
            
            yield return new WaitForSeconds(seconds);
            
            ResetEmotion();
            
        }
        else
        {
            Debug.LogError("Mouth objects are not assigned!");
        }
    }
}