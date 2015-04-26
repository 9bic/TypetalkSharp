using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TypetalkSharp
{
    public class UpdateTopic : IFormParameter
    {
        public string Name { get; set; }
        public int? TeamId { get; set; }

        public UpdateTopic(string name) {
            this.Name = name;
        }
        public FormUrlEncodedContent ToFormUrlEncodedContent() {
            var dict = new Dictionary<string, string>();
            if(this.Name != null) {
                dict.Add("name", this.Name);
            }
            if(this.TeamId != null) {
                dict.Add("teamId", this.TeamId.Value.ToString());
            }
            return new FormUrlEncodedContent(dict);
        }
    }
}
