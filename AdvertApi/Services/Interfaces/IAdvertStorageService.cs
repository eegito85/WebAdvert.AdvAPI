using AdvertApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Services.Interfaces
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel advertModel);
        Task Confirm(ConfirmAdvertModel confirmModel);
    }
}
