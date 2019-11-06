using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using System.Data;
using System.Diagnostics;
using Lucene.Net.Search;
using Remotion.Linq.Parsing.Structure;

namespace XC.Library.Luence
{
    public class LuenceHelper
    {
        static string path = @"D:\Sample";
        public void SearchEn()
        {
            var watch = Stopwatch.StartNew();

            //搜索
            IndexSearcher search = new IndexSearcher(path);

            //查询表达式
            QueryParser query = new QueryParser(string.Empty, new StandardAnalyzer());

            //query.parse：注入查询条件
            var hits = search.Search(query.Parse("Content:流行"));

            for (int i = 0; i < hits.Length(); i++)
            {
                Console.WriteLine("当前内容:{0}", hits.Doc(i).Get("Content").Substring(0, 20) + "...");
            }

            watch.Stop();

            Console.WriteLine("搜索耗费时间:{0}", watch.ElapsedMilliseconds);
        }

        static void CreateIndex()
        {
            //创建索引库目录
            var directory = FSDirectory.GetDirectory(path, true);
            //创建一个索引,采用StandardAnalyzer对句子进行分词
            IndexWriter indexWriter = new IndexWriter(directory, new IndexWriterConfig());

            var reader = DbHelperSQL.ExecuteReader("select * from News");

            while (reader.Read())
            {
                //域的集合：文档，类似于表的行
                Document doc = new Document();

                //要索引的字段
                doc.Add(new Field("ID", reader["ID"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field("Title", reader["Title"].ToString(), Field.Store.NO, Field.Index.ANALYZED));
                doc.Add(new Field("Content", reader["Content"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                indexWriter.AddDocument(doc);
            }

        }
    }
}
