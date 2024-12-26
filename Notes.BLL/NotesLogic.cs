using Epam.Auth.Models;
using Epam.Notes.BLL.Interfaces;
using Notes.DAL.Interfaces;
using Notes.Entities;

namespace Epam.Notes.BLL
{
    public class NotesLogic : INotesBLL
    {
        private readonly INotesDAO _noteDAO;

        public NotesLogic(INotesDAO noteDAO)
        {
            _noteDAO = noteDAO;
        }

        public Note AddNote(string text, User user , string imagePath)
        {

            var note = _noteDAO.AddNote(text, imagePath);
            user.Notes.Add(note.ID);

            return note;
        }

        public void RemoveNote(Guid id, User user)
        {
            _noteDAO.RemoveNote(id);
            user.Notes.Remove(id);
        }

        public void EditNote(Guid id, string newText) => _noteDAO.EditNote(id, newText);

        public Note GetNote(Guid id) => _noteDAO.GetNote(id);

        public IEnumerable<Note> GetNotes(bool orderedById) => _noteDAO.GetNotes(orderedById);
    }
}
