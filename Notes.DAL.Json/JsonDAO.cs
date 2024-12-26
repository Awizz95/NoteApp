using Newtonsoft.Json;
using Notes.DAL.Interfaces;
using Notes.Entities;

namespace Notes.DAL.Json
{
    public class JsonDAO : INotesDAO
    {
        public Note AddNote(string text, string imagePath)
        {
            var note = new Note(text, imagePath);

            string json = JsonConvert.SerializeObject(note);

            File.WriteAllText(GetFileFullNameById(note.ID), json);

            return note;
        }

        public void RemoveNote(Guid id)
        {
            if (File.Exists(GetFileFullNameById(id)))
                File.Delete(GetFileFullNameById(id));

            else throw new FileNotFoundException(
                string.Format("File with name {0} at path {1} isn't created!", id + ".json", FilesFolderPath));
        }

        public void EditNote(Guid id, string newText)
        {
            if (!File.Exists(GetFileFullNameById(id)))
                throw new FileNotFoundException(
                    string.Format("File with name {0} at path {1} isn't created!",
                        id, FilesFolderPath));

            Note note = JsonConvert.DeserializeObject<Note>(File.ReadAllText(GetFileFullNameById(id)));

            note.Edit(newText);

            File.WriteAllText(GetFileFullNameById(note.ID), JsonConvert.SerializeObject(note));
        }

        private string GetFileFullNameById(Guid id) => FilesFolderPath + id + ".json";

        public Note GetNote(Guid id)
        {
            if (!File.Exists(GetFileFullNameById(id)))
                throw new FileNotFoundException(
                    string.Format("File with name {0} at path {1} isn't created!",
                        id, FilesFolderPath));

            var textContent = File.ReadAllText(GetFileFullNameById(id));

            return JsonConvert.DeserializeObject<Note>(textContent);
        }

        public IEnumerable<Note> GetNotes(bool orderedById)
        {
            var files = Directory.GetFiles(FilesFolderPath);

            if (files.Length == 0)
            {
                return new List<Note>();
            }

            var notes = new List<Note>();

            foreach (var file in files)
            {
                var textContent = File.ReadAllText(file);
                var note = JsonConvert.DeserializeObject<Note>(textContent);
                notes.Add(note);
            }

            if (orderedById)
            {
                notes = notes.OrderBy(note => note.ID).ToList();
            }

            return notes;
        }

        private static DirectoryInfo GetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

        private string FilesFolderPath => Path.Combine(GetSolutionDirectoryInfo().FullName, @"Files\");
    }
}