using Epam.Notes.BLL.Interfaces;
using Epam.Notes.PL.Console;
using Notes.Dependencies;

namespace Notes.PL.Console
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SerilogHelper.InitializeLogger();

            INotesBLL bll = DependencyResolver.Instance.NotesBLL;
        }
    }
}