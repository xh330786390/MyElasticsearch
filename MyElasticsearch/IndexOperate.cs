using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch
{
    /// <summary>
    /// 索引操作
    /// </summary>
    public class IndexOperate
    {
        private ElasticClient client = null;

        /// <summary>
        /// 指标名称
        /// </summary>
        private static string indexName = "student";
        public static string IndexName
        {
            get { return indexName; }
            set { indexName = value; }
        }

        public IndexOperate()
        {
            client = ESHelper.GetESClient();
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="type"></param>
        public void Create(string indexName = null)
        {

            //var descriptor = new CreateIndexDescriptor(IndexName.ToLower())
            //   .Settings(s => s.NumberOfShards(5).NumberOfReplicas(1))
            //   .Mappings(ms => ms
            //       .Map<StudentModel>(m => m.AutoMap())
            //   );

            //client.CreateIndex(descriptor);

            //基本配置
            IIndexState indexState = new IndexState()
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = 1,//副本数
                    NumberOfShards = 5//分片数
                }
            };


            //自动创建Mapping
            //client.CreateIndex(indexName,
            //    s => s.InitializeUsing(indexState)
            //    .Mappings(ms => ms.Map<StudentModel>(m => m.AutoMap())));


            var descriptor = new CreateIndexDescriptor("myindex4")
                .Settings(s => s.NumberOfShards(5).NumberOfReplicas(1))
                .Mappings(ms => ms
                    .Map<StudentModel>(m => m.AutoMap()
                    .Properties(ps => ps
                        .String(s => s
                            .Name(e => e.Name)
                           .Analyzer("standard")
                        //.NotAnalyzed()

                            //.Fields(f => f
                        //    .String(ss => ss
                        //        .Name("name")
                        //        .Analyzer("stop")))
                        //.Fields(fs => fs
                        //    .Keyword(ss => ss
                        //        .Name("firstNameRaw")
                        //    )
                        //    .TokenCount(t => t
                        //        .Name("length")
                        //        .Analyzer("standard")
                        //    )
                        //)
                        )
                        //.Number(n => n
                        //    .Name(e => e.Id)
                        //    .Type(NumberType.Double)
                        //    .IgnoreMalformed(false)
                        //)
                        .Date(d => d
                            .Name(e => e.DateTime)
                            .Format("MM-dd-yy")
                        )
                    )
                    )
                );

            client.CreateIndex(descriptor);

            //手动指定
            client.CreateIndex(IndexName, index => index.Settings(s => s.NumberOfShards(5).NumberOfReplicas(1)));
            client.Map<StudentModel>(m => m.Properties(p => p.String(s => s.Name(n => n.Name).Fields(f => f.String(ss => ss.Name("Name"))))));

            //client.Map<StudentModel>(m => m.Properties(p => p.String(s => s.Name(n => n.Name).Fields(f => f.String(ss => ss.Name("Name").Index(FieldIndexOption.NotAnalyzed).NullValue("NULL"))) )));
            //.Name(n => n.Id).Fields(f => f.String(ss => ss.Name("id").Index(FieldIndexOption.NotAnalyzed))))));

            //.GeoPoint(gp => gp.Name(n => n.Name(n=).Location)// 坐标点类型
            //    .Fielddata(fd => fd
            //        .Format(GeoPointFielddataFormat.Compressed)//格式 array doc_values compressed disabled
            //        .Precision(new Distance(2, DistanceUnit.Meters)) //精确度
            //        ))
            //.String(s => s.Name(n => n.b_id))//string 类型
            //));
            ////在原有字段下新增字段（用于存储不同格式的数据，查询方法查看SearchBaseDemo）
            ////eg:在 vendorName 下添加无需分析器分析的值  temp
            //var result2 = client.Map<StudentModel>(
            //    m => m
            //        .Properties(p => p.String(s => s.Name(n => n.vendorName).Fields(fd => fd.String(ss => ss.Name("temp").Index(FieldIndexOption.NotAnalyzed))))));


        }
    }
}
