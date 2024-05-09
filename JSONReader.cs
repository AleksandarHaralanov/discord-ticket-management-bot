using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace OMNI.Config
{
    internal class JSONReader
    {
        public string Token { get; set; }

        public async Task ReadJSON(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string json = await streamReader.ReadToEndAsync();
                JSONStructure data = JsonConvert.DeserializeObject<JSONStructure>(json);

                this.Token = data.Token;
            }
        }
    }

    internal sealed class JSONStructure
    {
        public string Token { get; set; }
    }
}
