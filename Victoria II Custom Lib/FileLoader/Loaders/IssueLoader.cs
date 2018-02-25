namespace Victoria_II_Custom_Lib.FileLoader.Loaders
{
    public class IssueLoader : GameFileLoader
    {
        public static IssueLoader Default { get; } = new IssueLoader();
        /// <summary>
        /// defines issue loader
        /// </summary>
        private IssueLoader() : base("/common/issues.txt")
        {

        }
    }
}
