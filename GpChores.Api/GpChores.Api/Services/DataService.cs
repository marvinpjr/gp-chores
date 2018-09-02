using GpJobs.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GpJobs.Api.Services
{
    public class DataService<T>: IDataService<T> where T: IDataItem
    {
        private IHostingEnvironment _hostingEnvironment;
        private string _dataFilePath;
        private List<T> _dataItems;

        public DataService(IHostingEnvironment hostingEnvironment)
        {
            var fileName = $"{typeof(T).Name}Data.txt";

            _hostingEnvironment = hostingEnvironment;
            _dataFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "DataFiles", fileName);

            _dataItems = getDataItems().Result;
        }

        public async Task<string> Create(T dataItem)
        {
            dataItem.Id = Guid.NewGuid().ToString();
            _dataItems.Add(dataItem);

            await updateDataFile();
            return dataItem.Id;
        }

        public async Task<bool> Delete(string id)
        {
            _dataItems.RemoveAll(c => c.Id == id);
            await updateDataFile();
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return _dataItems;
        }

        public async Task<T> GetById(string dataItemId)
        {
            return _dataItems.SingleOrDefault<T>(di => di.Id == dataItemId);
        }

        public async Task<List<T>> GetMatching(Func<T, bool> query)
        {
            return _dataItems.Where(query).ToList<T>();
        }

        public async Task<T> Update(string id, T dataItem)
        {
            var dataItemToUpdate = _dataItems.SingleOrDefault(di => di.Id == id);
            if (dataItemToUpdate != null)
            {
                var props = typeof(T).GetProperties(BindingFlags.Public);
                foreach (var prop in props)
                {
                    var newValue = prop.GetValue(dataItem);
                    prop.SetValue(dataItemToUpdate, newValue);
                }
            }

            await updateDataFile();
            return dataItemToUpdate;
        }

        private async Task updateDataFile()
        {
            var dataJson = JsonConvert.SerializeObject(_dataItems);
            await File.WriteAllTextAsync(_dataFilePath, dataJson);
        }

        private async Task<List<T>> getDataItems()
        {
            if (!File.Exists(_dataFilePath)) return new List<T>();

            var dataItemsJson = await File.ReadAllTextAsync(_dataFilePath);
            var deserializedData = JsonConvert.DeserializeObject<List<T>>(dataItemsJson);

            return deserializedData ?? new List<T>();
        }
    }

    public interface IDataService<T>
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetMatching(Func<T, bool> query);
        Task<T> GetById(string dataItemId);
        Task<string> Create(T dataItem);
        Task<T> Update(string id, T dataItem);
        Task<bool> Delete(string id);        
    }
}
