using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch
{
    public class ESHelper
    {
        private static SniffingConnectionPool connectionPool;
        private static ElasticClient _esClient;

        public static ElasticClient GetESClient()
        {
            if (_esClient == null)
            {
                var esSettings = new ConnectionSettings(ConnectionPool);
                _esClient = new ElasticClient(esSettings);
            }
            return _esClient;
        }

        private static SniffingConnectionPool ConnectionPool
        {
            get
            {
                if (connectionPool == null)
                {
                    var uris = GetEsUrl();
                    connectionPool = new SniffingConnectionPool(uris);
                }

                return connectionPool;
            }
        }

        private static List<Uri> GetEsUrl()
        {
            var uris = new List<Uri>();
            string[] urls = null;

            urls = ConfigurationManager.AppSettings["ElasticsearchUrl"].Split(',');

            foreach (var url in urls)
            {
                uris.Add(new Uri(string.Format("http://{0}/", url)));
            }
            return uris;
        }

        /// <summary>
        /// 最大搜索窗口
        /// </summary>
        public static int SearchWindow = 10000;

        /// <summary>
        /// ik_max_word
        /// </summary>
        public const string IKMaxWord = "ik";

        /// <summary>
        /// ik_smart
        /// </summary>
        public const string IKSmart = "ik_smart";

        /// <summary>
        /// ik_syn
        /// </summary>
        public const string IKSyn = "ik_syn";

        /// <summary>
        /// synonym
        /// </summary>
        public const string Synonym = "synonym";


        /// <summary>
        /// 高亮开始
        /// </summary>
        public const string HighlightPre = "<font color=\"red\" name=\"微软雅黑\" size=\"10\">";

        /// <summary>
        /// 高亮结束
        /// </summary>
        public const string HighlightPost = "</font>";

        /// <summary>
        /// 海关名称
        /// </summary>
        public static string Type_Customs = "customs";
    }

    /// <summary>
    /// IK分析器配置类
    /// </summary>
    //public class IKSynAnalyzer : AnalyzerBase
    //{
    //    [JsonProperty(PropertyName = "tokenizer")]
    //    public string Tokenizer { get; set; }
    //}

    /// <summary>
    /// 同义词分词过滤器配置
    /// </summary>
    //public class SynTokenFilter : TokenFilterBase
    //{
    //    public SynTokenFilter(string type)
    //        : base(type) { }

    //    [JsonProperty(PropertyName = "synonyms_path")]
    //    public string SynonymsPath { get; set; }
    //}
}
