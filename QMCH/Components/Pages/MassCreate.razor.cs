namespace QMCH.Components.Pages
{
    // no code-behind needed - logic moved to .razor file
    public partial class MassCreate { }

    public class MassRow
    {
        public int Index { get; set; }
        public QMCH.Models.ClientType Model { get; set; } = new QMCH.Models.ClientType();
    }
}
