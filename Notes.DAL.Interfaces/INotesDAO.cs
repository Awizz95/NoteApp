using Notes.Entities;

namespace Notes.DAL.Interfaces
{
    public interface INotesDAO
    {
        Note AddNote(string text, string imagePath);

        void RemoveNote(Guid id);

        void EditNote(Guid id, string newText);

        Note GetNote(Guid id);

        IEnumerable<Note> GetNotes(bool orderedById);
    }
}