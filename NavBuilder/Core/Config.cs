#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion Usings

namespace NavBuilder.Core
{
    public class Config
    {
        #region Members

        /// <summary>
        /// The object that stores the configuration
        /// </summary>
        private Dictionary<string, string> _config;

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        private string _path;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Config"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public Config(string file)
        {
            Load(file);
        }

        #endregion  

        #region Methods

        /// <summary>
        /// Existses the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (_config == null)
                return false;

            return _config.ContainsKey(key) && _config[key] != null && _config[key] != string.Empty;
        }

        /// <summary>
        /// Sets the specified key inside the config.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>

        public void Set<T>(string key, T value)
        {
            _config = _config ?? new Dictionary<string, string>();

            string setValue = value == null ? string.Empty : value.ToString();
            if (!_config.ContainsKey(key))
                _config.Add(key, setValue);
            else
                _config[key] = setValue;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        public T Get<T>(string key, string defaultValue = "")
        {
            if (_config == null) return (T)Convert.ChangeType(false, typeof(T));

            if (!_config.ContainsKey(key))
                Set(key, defaultValue);

            return (T)Convert.ChangeType(_config[key], typeof(T));
        }

        /// <summary>
        /// Loads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        private void Load(string file)
        {
            _path = file;
            if (!File.Exists(_path))
                File.Create(_path).Dispose();

            _config = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(_path))
            {
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) continue;

                var key = line.Split('{')[0];
                var value = line.Split('{')[1].Split('}')[0];

                if (!_config.ContainsKey(key))
                    _config.Add(key, value);
            }
        }

        /// <summary>
        /// Saves the specified file.
        /// </summary>
        public void Save()
        {
            if (_config == null || string.IsNullOrWhiteSpace(_path)) return;

            if (!File.Exists(_path))
                File.Create(_path).Dispose();

            var serializedConfig = new string[_config.Count];
            var index = 0;

            foreach (var element in _config)
            {
                serializedConfig[index] = element.Key + "{" + element.Value + "}";
                index++;
            }

            File.WriteAllLines(_path, serializedConfig);
        }

        /// <summary>
        /// Sets the array.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        /// <param name="delimiter">The delimiter.</param>
        public void SetArray<T>(string key, IList<T> values, string delimiter = ",")
        {
            if (values == null) return;

            Set(key, string.Join(delimiter, values));
        }

        /// <summary>
        /// Gets the array.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public T[] GetArray<T>(string key, char delimiter = ',')
        {
            var data = Get<string>(key).Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return data.Length == 0 ? new T[] { } : data?.Select(p => (T)Convert.ChangeType(p, typeof(T))).ToArray();
        }

        #endregion Methods
    }
}