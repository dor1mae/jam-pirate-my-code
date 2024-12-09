using UnityEngine;

public class NoteInteractor : ItemInteractor
{
    [SerializeField] private NoteWindow _window;
    [SerializeField] private string _title;
    [SerializeField] [TextArea(5, 10)]  private string _text;

    protected override AbstractItem CreateInstance()
    {
        return new Note(_itemForSpawn, _window, _title, _text);
    }
}
