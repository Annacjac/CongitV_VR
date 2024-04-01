using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SADText : MonoBehaviour
{

    public TextMeshProUGUI text;
    public Button nextButton;
    public Button prevButton;
    int page = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(page == 0){
            text.text = "More than 31M adults in the U.S. suffer from social anxiety disorder, characterized by " + 
            "persistent fear of social situations in anticipation of being judged negatively by others. " + 
            "Social anxiety disorder can negatively affect all areas of life, including academic " +
            "performance, work performance, and social relationships. Social anxiety disorder is the " +
            "most prevalent anxiety disorder affecting 12.1% of the adult population, making it quite " +
            "common, and typically begins in childhood or adolescence.\n\n" +
            "On the following pages is a list of coping skills for social anxiety disorder (box breathing, " +
            "breathing exercises, relaxation exercises, self-talk/challenge thinking, self-" +
            "affirmations) with an explanation of each and an example of how do practice " +
            "each.";

            prevButton.gameObject.SetActive(false);
        }
        else if(page == 1){
            text.text = "Box Breathing: Box breathing, also known as square breathing, is a specialized " +
            "breathing technique that promotes relaxation and mindfulness. Its structured approach " +
            "helps individuals manage social anxiety by providing a simple and rhythmic breathing "+
            "pattern. The technique involves inhaling, holding the breath, exhaling, and holding "+
            "again, each for an equal count, creating a square-shaped breath cycle. The benefits "+
            "include increased focus, reduced stress, and a sense of control.";
            
            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(true);
        }
        else if(page == 2){

            text.text = "How to Practice Box Breathing:\n" +
            "1. Find a Quiet Space: Choose a quiet environment where you can comfortably sit or stand.\n"+
            "2. Sit or Stand Comfortably: Adopt a comfortable posture with your back straight and shoulders relaxed.\n"+
            "3. Inhale (Count of Four): Inhale deeply through your nose for a count of four, filling your lungs completely.\n"+
            "4. Hold (Count of Four): Hold your breath for a count of four, maintaining a sense offullness.\n"+
            "5. Exhale (Count of Four): Exhale slowly and completely through your mouth for a count of four, releasing tension.\n"+
            "6. Hold (Count of Four): Pause and hold the breath again for a count of four before beginning the next cycle.\n"+
            "7. Repeat the Cycle: Continue the box breathing pattern for several cycles, focusing on the rhythmic flow of breath.";
        }
        else if(page == 3){
            text.text = "Breathing Exercises: Breathing exercises are a powerful tool for managing social "+
            "anxiety. By focusing on controlled, deep breaths, individuals can activate the body's "+
            "relaxation response, calming the nervous system. To practice, find a quiet space, inhale"+
            "deeply through the nose for a count of four, and exhale slowly through the mouth for "+
            "another six counts. Extend the exhale longer, if able. Repeat this process several times "+
            "to alleviate tension and promote a sense of calm.\n\n"+
            "Relaxation Exercises: Engaging in relaxation exercises helps combat social anxiety by"+
            "reducing muscle tension and promoting overall relaxation. Progressive muscle "+
            "relaxation is an effective technique; start by tensing and then gradually releasing each "+
            "muscle group in the body. Begin with the toes and work your way up to the head, paying "+
            "very good attention to any sensations and observing them. This practice encourages a "+
            "state of physical and mental ease, making social interactions more manageable.";
        }
        else if(page == 4){
            text.text = "Positive Self-Talk: Positive self-talk involves challenging and replacing negative "+
            "thoughts with more constructive and supportive ones. This coping skill helps reshape "+
            "the mindset, fostering self-confidence and reducing social anxiety. Identify negative "+
            "thoughts, counter them with positive affirmations, and remind yourself of past "+
            "successes. Start by paying attention to your thoughts. This may be a challenge at first "+
            "because thoughts can be so fleeting. The more you pay attention the better you get at "+
            "recognizing your thoughts. After identifying the thoughts, take time to reflect on their "+
            "validity and counter them with more a positive perspective. For instance, if the thought "+
            "\"I'll embarrass myself\" arises, counter it with \"I have handled similar situations well "+
            "before, and I can do it again.\" Or, counter \"no one thinks I am interesting\" with \"I bring "+
            "unique perspectives and qualities to every interaction, and I have interesting "+
            "experiences to share.\"";

            nextButton.gameObject.SetActive(true);
            prevButton.gameObject.SetActive(true);
        }
        else if(page == 5){
            text.text = "Challenge Thinking: Challenging thinking patterns is crucial in addressing social "+
            "anxiety. This involves questioning irrational beliefs and assumptions that contribute to "+
            "anxious thoughts. Identify and challenge these thoughts by asking yourself if they are "+
            "based on evidence or distorted perceptions. For example, if the thought \"Everyone is "+
            "judging me\" arises, ask yourself for evidence supporting this belief and consider more "+
            "realistic alternatives such as \"Not everyone is focused on me; people are likely more "+
            "concerned with their own thoughts and experiences\" or \"I can't accurately know what "+
            "everyone is thinking, and assuming negative judgments may be an exaggeration of the situation.\"\n\n"+
            "Self-Affirmations: Self-affirmations involve acknowledging and reinforcing positive "+
            "qualities and values about oneself. This practice enhances self-esteem and reduces "+
            "social anxiety by fostering a more positive self-image. Create a list of affirmations that "+
            "resonate with you, such as \"I am worthy of connection and acceptance\", \"I embrace my "+
            "uniqueness and value,\" or \"I have the strength to navigate social situations with ease.\" "+
            "Repeat these affirmations daily – each morning upon waking, before going to sleep, and "+
            "especially before and during social situations, to cultivate a more positive and empowering mindset.";

            nextButton.gameObject.SetActive(false);
        }

    }

    public void NextPage(){
        if(page < 5){
            page++;
        }
    }

    public void PrevPage(){
        if(page > 0){
            page--;
        }
    }


}
