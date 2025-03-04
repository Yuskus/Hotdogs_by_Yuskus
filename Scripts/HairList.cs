using System.Collections.Generic;
using UnityEngine;

public class HairList : MonoBehaviour
{
    public List<int> haircutsMan = new();
    public List<int> haircutsWoman = new();
    public Sprite[] hair = new Sprite[13];
    public Sprite[,,] faces = new Sprite[2,4,6]; //eyes, eyebrows color, emotion
    public List<int> randomIndex = new() { 0, 1, 2, 3, 4 };
    public AudioClip[] audioClipM, audioClipW;
    public readonly int[] hairColor = new int[13]
    {
        (int)Colors.black,
        (int)Colors.red,
        (int)Colors.brown,
        (int)Colors.black,
        (int)Colors.blond,
        (int)Colors.brown,
        (int)Colors.black,
        (int)Colors.brown,
        (int)Colors.red,
        (int)Colors.brown,
        (int)Colors.black,
        (int)Colors.blond,
        (int)Colors.brown
    };
    public enum Colors { black, blond, red, brown };
    private void Start()
    {
        hair = Resources.LoadAll<Sprite>("Sprites/People/Haircuts");
        for (int i = 0; i < 7; i++) { haircutsMan.Add(i); }
        for (int i = 7; i < 13; i++) { haircutsWoman.Add(i); }
        for (int i = 0; i < 6; i++)
        {
            faces[0,0,i] = Resources.Load<Sprite>("Sprites/People/Faces/blue eyes/01. black/"+i);
            faces[0,1,i] = Resources.Load<Sprite>("Sprites/People/Faces/blue eyes/02. blond/"+i);
            faces[0,2,i] = Resources.Load<Sprite>("Sprites/People/Faces/blue eyes/03. red/"+i);
            faces[0,3,i] = Resources.Load<Sprite>("Sprites/People/Faces/blue eyes/04. brown/"+i);
            faces[1,0,i] = Resources.Load<Sprite>("Sprites/People/Faces/brown eyes/01. black/"+i);
            faces[1,1,i] = Resources.Load<Sprite>("Sprites/People/Faces/brown eyes/02. blond/"+i);
            faces[1,2,i] = Resources.Load<Sprite>("Sprites/People/Faces/brown eyes/03. red/"+i);
            faces[1,3,i] = Resources.Load<Sprite>("Sprites/People/Faces/brown eyes/04. brown/"+i);
        }
        audioClipM = Resources.LoadAll<AudioClip>("Sounds/ClientsSounds/man");
        audioClipW = Resources.LoadAll<AudioClip>("Sounds/ClientsSounds/woman");
    }
}
