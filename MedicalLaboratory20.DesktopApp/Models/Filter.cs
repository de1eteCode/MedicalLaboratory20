using SharedModels;

namespace MedicalLaboratory20.DesktopApp.Models
{
    internal class Filter
    {
        public Filter(string title, string property)
        {
            Title = title;
            Property = property;
        }

        public string Title {  get; private set; }
        public string Property { get; private set; }

        public bool IsEqual(LogingAuth lAuth, string search)
        {
            var obj = lAuth[Property];
            if (obj is null)
                return false;
            return obj.ToString().ToLower().Contains(search.ToLower());
        }
    }
}
