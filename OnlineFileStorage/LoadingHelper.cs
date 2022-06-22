using System;
using System.Collections.Generic;

namespace OnlineFileStorage.Utilities
{
    public enum Status
    {
        Init,
        Progress,
        Success,
        Error
    }

    public class LoadingData
    {
        private long _loadedBytes;
        private readonly long _fileSizeBytes;
        public Status Status;
        public float Progress => GetPercentage();

        public void UpdateLoadedBytes(long bytes)
        {
            _loadedBytes = bytes;
        }

        private float GetPercentage()
        {
            return (_loadedBytes / _fileSizeBytes) * 100;
        }

        public LoadingData(long fileSizeBytes)
        {
            _fileSizeBytes = fileSizeBytes;
            Status = Status.Init;
        }
    }

    
    public static class LoadingHelper
    {
        private static readonly object _locker = new object();
        private static readonly Dictionary<Guid, LoadingData> _loadData = new Dictionary<Guid, LoadingData>();

        public static void NewLoadInit(Guid loadId, long fileSizeBytes)
        {
            lock (_locker)
            {
                if (_loadData.ContainsKey(loadId))
                {
                    throw new Exception("is loadId already exists");
                }

                _loadData.Add(loadId, new LoadingData(fileSizeBytes));
            }
        }

        public static bool Exists(Guid loadId)
        {
            bool res;
            lock (_locker)
            {
                res = _loadData.ContainsKey(loadId);
            }

            return res;
        }

        public static void UpdateProgress(Guid loadId, long bytes)
        {
            lock (_locker)
            {
                if (!_loadData.ContainsKey(loadId))
                {
                    throw new KeyNotFoundException("loadId not found");
                }

                _loadData[loadId].UpdateLoadedBytes(bytes);
            }
        }

        public static void UpdateStatus(Guid loadId, Status status)
        {
            lock (_locker)
            {
                if (!_loadData.ContainsKey(loadId))
                {
                    throw new KeyNotFoundException("loadId not found");
                }

                _loadData[loadId].Status = status;
            }
        }

        public static LoadingData GetStatus(Guid loadId)
        {
            lock (_locker)
            {
                if (_loadData.ContainsKey(loadId)) return _loadData[loadId];
            }

            throw new KeyNotFoundException("loadId not found");
        }
    }
}
