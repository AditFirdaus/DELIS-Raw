using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteAnalyzerModule : MonoBehaviour
{
    public bool autoplay = false;
    public NoteAnalyzer[] noteAnalyzers;

    // References
    public AudioSource audioSource;

    // Properties
    public Note[] notes;
    public void Init(AudioSource _audioSource, Note[] _notes, bool _autoplay = true)
    {
        this.audioSource = _audioSource;
        this.notes = _notes;
        this.autoplay = _autoplay;

        if (autoplay) Activate();
    }

    public void Activate()
    {
        foreach (NoteAnalyzer analyzer in noteAnalyzers)
        {
            analyzer.Init(this, audioSource, notes);
        }
    }
}

[System.Serializable]
public class NoteAnalyzer
{
    public bool autoplay = false;
    public bool analyzing = true;

    // Properties
    public MonoBehaviour mono;
    public AudioSource audioSource;
    public float offset;
    public Note[] notes;

    // Comunicataor
    public int noteIndex = 0;

    public UnityEvent<int> spawnCallback = new UnityEvent<int>();
    public UnityEvent endCallback = new UnityEvent();

    public UnityAction<int> spawnAction;
    public UnityAction endAction;

    public Coroutine analyzerCoroutine;

    public NoteAnalyzer(MonoBehaviour _monoBehaviour, AudioSource _audioSource, Note[] _notes, bool _autoplay = true, float _offset = 0)
    {
        Init(_monoBehaviour, _audioSource, _notes, _autoplay, _offset);
    }

    public void Init(MonoBehaviour _monoBehaviour, AudioSource _audioSource, Note[] _notes, bool _autoplay = true, float _offset = 0)
    {
        this.mono = _monoBehaviour;
        this.audioSource = _audioSource;
        this.notes = _notes;
        if (_offset != 0) this.offset = _offset;
        this.autoplay = _autoplay;

        if (this.autoplay) this.Activate();
    }

    public void Activate()
    {
        analyzing = true;

        if (analyzerCoroutine != null) mono.StopCoroutine(analyzerCoroutine);
        analyzerCoroutine = mono.StartCoroutine(Analyze());
    }

    public IEnumerator Analyze()
    {
        if (notes == null) yield return null;

        for (int i = 0; i < notes.Length; i++)
        {
            while (notes[i].globalTime > audioSource.time + offset) yield return null;

            if (analyzing)
            {
                noteIndex = i;

                spawnCallback.Invoke(noteIndex);
                spawnAction?.Invoke(noteIndex);
            }
        }
        endCallback.Invoke();
        endAction?.Invoke();
    }

    public Note GetNote(int index = -1)
    {
        if (index < 0) index = noteIndex;

        return notes[Mathf.Clamp(index, 0, notes.Length - 1)];
    }

}