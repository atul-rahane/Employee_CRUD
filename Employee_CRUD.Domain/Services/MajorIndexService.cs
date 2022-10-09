namespace Employee_CRUD.Domain.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly EmployeeCrudHttpClient _client;

        public MajorIndexService(EmployeeCrudHttpClient client)
        {
            _client = client;
        }

        /*public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            string uri = "majors-indexes/" + GetUriSuffix(indexType);

            MajorIndex majorIndex = await _client.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;

            return majorIndex;
        }*/

        /*private string GetUriSuffix(MajorIndexType indexType)
        {
            switch(indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    throw new Exception("MajorIndexType does not have a suffix defined.");
            }
        }*/
    }
}