using Notes.DAL.Interfaces;
using Notes.Entities;
using Notes.Entities.DB;


namespace Notes.DAL.SQL
{
    public class SqlDAO : INotesDAO
    {
        private readonly NotesDbContext _context;

        public SqlDAO(NotesDbContext context)
        {
            _context = context;
        }

        public Note AddNote(string text, string imagePath)
        {
            var note = new Note(text, imagePath);
            _context.Notes.Add(note);
            _context.SaveChanges();

            return note;
        }

        public void EditNote(Guid id, string newText)
        {
            var note = _context.Notes.Find(id);
            
            if (note is null)
            {
                throw new FileNotFoundException(message: ExceptionMessages.NOTE_NOT_FOUND);
            }

            note.Edit(newText);
            _context.SaveChanges();
        }

        public Note GetNote(Guid id)
        {
            var note = _context.Notes.Find(id);

            if (note is null)
            {
                throw new FileNotFoundException(message: ExceptionMessages.NOTE_NOT_FOUND);
            }

            return note;
        }

        public IEnumerable<Note> GetNotes(bool orderedById)
        {
            var notes = _context.Notes.AsEnumerable();

            if (orderedById)
            {
                notes = notes.OrderBy(x => x.ID);
            }

            return notes.ToList();
        }

        public void RemoveNote(Guid id)
        {
            var note = _context.Notes.Find(id);

            if (note is null)
            {
                throw new FileNotFoundException(message: ExceptionMessages.NOTE_NOT_FOUND);
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();
        }
    }
}