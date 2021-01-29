﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using traffic_app.DTO;

namespace traffic_app.BLL.Services.IServices
{
    public interface IOnTheWayDriverPostService
    {
        Task AddDriverPost(OnTheWayDriverPostToAddDTO onTheWayDriverPostToAddDTO, string filePath, int userId);
    }
}
