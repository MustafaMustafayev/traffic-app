using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using traffic_app.BLL.Services.IServices;
using traffic_app.DTO;

namespace traffic_app.BLL.Services
{
    public class UserElasticsearchService : IUserElasticsearchService
    {
        private readonly ElasticClient client = null;
        private readonly IConfiguration _configuration;
        private string indicesName = string.Empty;
        public UserElasticsearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            var uri = new Uri(_configuration["ElasticsearchSetting:URI"]);
            var settings = new ConnectionSettings(uri);
            client = new ElasticClient(settings);
            indicesName = _configuration["ElasticsearchSetting:IndicesNameForUser"];
            settings.DefaultIndex(indicesName);
        }

        public void AddDataToIndices(UserElasticsearchDTO userToElasticsearchAddDTO)
        {
            client.IndexAsync<UserElasticsearchDTO>(userToElasticsearchAddDTO, null);
        }

        public void CreateIndex()
        {
            client.Indices.Create(indicesName,
                    index => index.Map<UserElasticsearchDTO>(
                        x => x.AutoMap()));
        }

        public void DeleteIndicesData(int userId)
        {
            client.DeleteByQueryAsync<UserElasticsearchDTO>(q => q.Query(rq => rq.Term(f => f.Id, userId)));
        }

        public List<UserToElasticsearchListDTO> Search(string condition)
        {
            return client.Search<UserToElasticsearchListDTO>(s => s
                                .Query(q => q
                                .MultiMatch(mm => mm
                                .Query(condition)
                                .Fields(fl => fl.Field(p => p.CarNumber))
                                .Fuzziness(Fuzziness.EditDistance(8))))).Documents.ToList();
        }

        public void UpdateIndicesData(UserElasticsearchDTO userToElasticsearchAddDTO)
        {
            client.UpdateAsync<UserElasticsearchDTO>(userToElasticsearchAddDTO.Id, u => u.Index(indicesName)
                                .Doc(userToElasticsearchAddDTO).Refresh(Refresh.True));
        }
    }
}
