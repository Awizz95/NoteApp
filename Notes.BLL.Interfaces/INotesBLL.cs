using Epam.Auth.Models;
using Notes.Entities;

namespace Epam.Notes.BLL.Interfaces
{
    public interface INotesBLL
    {
        Note AddNote(string text, User user, string imagePath);

        void RemoveNote(Guid id, User user);

        void EditNote(Guid id, string newText);

        Note GetNote(Guid id);

        IEnumerable<Note> GetNotes(bool orderedById);
    }
}