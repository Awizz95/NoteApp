using Epam.Notes.BLL;
using Epam.Notes.BLL.Interfaces;
using Notes.DAL.Interfaces;
using Notes.DAL.Json;

namespace Notes.Dependencies
{
    public class DependencyResolver
    {
        #region SINGLETON

        private static DependencyResolver _instance;

        public static DependencyResolver Instance => _instance ??= new DependencyResolver();

        private DependencyResolver()
        {
        }

        #endregion

        private INotesDAO _notesDAO;
        public INotesDAO NotesDAO => _notesDAO ??= new JsonDAO();

        private INotesBLL _notesBLL;
        public INotesBLL NotesBLL => _notesBLL ??= new NotesLogic(NotesDAO);
    }
}