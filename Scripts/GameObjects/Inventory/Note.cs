using System;
using UnityEngine;

public class Note : UsableItem
{
    private string _title;
    private string _text;

    public string Title => _title;
    public string Text => _text;

    private NoteWindow _noteWindow;

    public Note(AbstractScriptableItem item, NoteWindow noteWindow, string title, string text) : base(item)
    {
        _noteWindow = noteWindow;
        _title = title;
        _text = text;
    }

    public override void Use()
    {
        NoteWindow.OnOpenWindow.Invoke();
        _noteWindow.OpenNote(this);
    }
}
