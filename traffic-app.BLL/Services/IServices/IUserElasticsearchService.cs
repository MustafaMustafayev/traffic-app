using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IUserElasticsearchService
    {
        public void AddDataToIndices(UserElasticsearchDTO userToElasticsearchAddDTO);
        public void UpdateIndicesData(UserElasticsearchDTO userToElasticsearchAddDTO);
        public void DeleteIndicesData(int userId);
        public void CreateIndex();
        public List<UserToElasticsearchListDTO> Search(string condition);
    }
}
