using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoteMapCreator : MonoBehaviour
{
    public bool mouse = false;
    public bool touch = false;
    public int currentIndex = 0;
    public static LevelData levelData;
    public LevelData _levelData;
    public int notePackIndex = 0;



    public string notePackIndexString = "0";

    [Header("Reference")]
    public AudioSource audioSource;
    public RectTransform creationArea;
    public TrailRenderer trailRenderer;
    public TMP_Text indexDisplayer;

    public Image progressBar;

    [Header("Data")]
    public NoteMap noteMap;
    public List<NoteData> noteDatas;

    private void Start()
    {
        currentIndex = 0;
        if (!levelData) levelData = _levelData;

        levelData.LoadData(true);
        audioSource.clip = levelData.musicClip;
        audioSource.Play();
    }

    public static void Create(LevelData level)
    {
        NoteMapCreator.levelData = level;
        SceneManager.LoadScene("NoteMapCreator");
    }

    void Update()
    {
        progressBar.fillAmount = (audioSource.time / audioSource.clip.length);

        MouseInput();
        TouchInput();
    }
    public void MouseInput()
    {
        if (!mouse) return;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            Add(Input.mousePosition);
        }
    }
    public void TouchInput()
    {
        if (!touch) return;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Add(touch.position);
            }
        }
    }
    [ContextMenu("Insert NotePack")]
    public void InsertNotePack()
    {
        noteMap.notePacks[notePackIndex].datas = noteDatas.ToArray();
    }

    [ContextMenu("Save NoteMap")]
    public void SaveNoteMap()
    {
        SLS.SaveJson<NoteMap>(
            noteMap,
            Application.dataPath,
            $"/Resources/Game/Level/NoteMap/{levelData.packName}",
            $"{levelData.levelName}"
        );
    }

    public void Add(Vector2 screenPos)
    {
        if (creationArea.rect.Contains(creationArea.InverseTransformPoint(screenPos), true))
        {
            NoteData noteData = new NoteData(audioSource.time, Camera.main.ScreenToViewportPoint(screenPos));
            noteDatas.Add(noteData);
            trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(screenPos) + Vector3.forward * 1;

            UpdateIndex();
        }

    }

    public void Back()
    {
        LevelSelector.Enter(LevelSelector.levelPack);
    }

    public void BackAudio()
    {
        audioSource.time = Mathf.Clamp(audioSource.time - 1, 0, audioSource.clip.length);
    }

    public void DeleteNote()
    {
        if (noteDatas.Count > 0) noteDatas.RemoveAt(noteDatas.Count - 1);

        UpdateIndex();
    }

    public void GoToLastNote()
    {
        if (noteDatas.Count > 0)
        {
            NoteData noteData = noteDatas[noteDatas.Count - 1];

            audioSource.Pause();
            audioSource.time = noteData.time;
            audioSource.UnPause();
        }
    }

    public void DeleteAfterNote()
    {
        NoteData[] datas = noteDatas.ToArray();
        foreach (NoteData data in datas)
        {
            if (data.time > audioSource.time)
            {
                noteDatas.Remove(data);
            }
        }

        UpdateIndex();
    }

    public void UpdateIndex()
    {
        currentIndex = (noteDatas.Count - 1);

        indexDisplayer.text = "";

        if (currentIndex >= 0)
        {
            indexDisplayer.text = currentIndex.ToString();
        }
    }

    public void ResetCreator()
    {
        Create(levelData);
    }

}