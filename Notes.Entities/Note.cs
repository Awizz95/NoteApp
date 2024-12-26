using System.ComponentModel.DataAnnotations;

namespace Notes.Entities
{
    public class Note
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }

        public Note(Guid Id, DateTime creationDate, string text, string image)
        {
            ID = Id;
            CreationDate = creationDate;
            Text = text;
            Image = image;
        }

        public Note(DateTime creationDate, string text, string image)
        {
            ID = Guid.NewGuid();
            CreationDate = creationDate;
            Text = text;
            Image = image;
        }

        public Note(string text, string image)
        {
            ID = Guid.NewGuid();
            CreationDate = DateTime.Now;
            Text = text;
            Image = image;
        }

        public Note()
        {
        }

        public void Edit(string newText)
        {
            Text = newText;
        }
    }
}