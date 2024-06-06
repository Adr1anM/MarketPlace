
using MarketPlace.Application.FileServices;


namespace MarketPlace.Application
{
    public class GetDataFromFile
    {
        private readonly IFileService _fileService;

        public GetDataFromFile(IFileService fileservice)
        {
            _fileService =  fileservice;
        }

        public async Task<bool> CompareTofileData(string template)
        {
            string path = @"C:\Users\andri\OneDrive\Desktop\TestFile.txt";
            string data = await _fileService.GetFile(path);
            
            if(data.Contains(template))
            {
                return true;
            }

            return false;
        }
    }
}
