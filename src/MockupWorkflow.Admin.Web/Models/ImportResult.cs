namespace MockupWorkflow.Admin.Web.Models
{
    public class ImportResult
    {
        public int Received { get; set; }
        public int Inserted { get; set; }
        public int AlreadyPresent { get; set; }
    }
}
