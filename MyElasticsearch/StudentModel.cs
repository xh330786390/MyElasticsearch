using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace MyElasticsearch
{
    [ElasticsearchType(Name = "student", IdProperty = "id")]
    public class StudentModel
    {
        [Nest.String(Index = FieldIndexOption.NotAnalyzed)]
        public string Id { get; set; }

        [Nest.String(Analyzer = "standard")]
        public string Name { get; set; }

        [Nest.String(Analyzer = "standard")]
        public string Description { get; set; }

        [Nest.Date(Format="yyyy-MM-dd")]
        public DateTime DateTime { get; set; }
    }
}


