﻿using System.Collections.Generic;
using System.IO;
using CloudOps;
using Newtonsoft.Json;
using AWSRetriver.Controls;

namespace Retriever
{
    namespace Model
    {
        public class Profile : List<ProfileRecord>
        {
            private string path;

            public static string FileFilter
            {
                get
                {
                    return "Profile (profile.*.js)|profile.*.js";
                }
            }

            public static string Extension
            {
                get
                {
                    return ".js";
                }
            }

            public string Name
            {
                get
                {
                    //the file name has two extensions... (.profile.js).
                    return System.IO.Path.GetFileNameWithoutExtension(
                        System.IO.Path.GetFileNameWithoutExtension(this.Path));
                }
            }

            public string Path { get => path;}

            public Profile()
            {

            }

            public static Profile AllServices()
            {
                Profile p = new Profile()
                {
                    path = System.IO.Path.Combine("everything", Profile.Extension)
                };
                string regions = RegionsString.All().Text();
                foreach (Operation op in OperationFactory.All())
                {
                    p.Add(new ProfileRecord(op.ServiceName, op.Name, regions, true, Configuration.Instance.PageSize));
                }

                return p;
            }

            public IEnumerable<string> Services()
            {
                HashSet<string> items = new HashSet<string>();
                foreach (ProfileRecord profileRecord in this)
                {
                    items.Add(profileRecord.ServiceName);
                }
                return items;
            }

            public static Operation FindOpeartion(ProfileRecord p)
            {
                foreach (Operation op in OperationFactory.All())
                {
                    if (op.ServiceName == p.ServiceName)
                    {
                        if (op.Name == p.Name)
                        {
                            return op;
                        }
                    }
                }
                return null;
            }

            public ProfileRecord Find(string service, string operation)
            {
                foreach (ProfileRecord pr in this)
                {
                    if (pr.ServiceName == service)
                    {
                        if (pr.Name == operation)
                        {
                            return pr;
                        }
                    }
                }
                return null;
            }

            public void Set(ProfileRecord p)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Name == p.Name)
                    {
                        if (this[i].ServiceName == p.ServiceName)
                        {
                            this[i] = p;
                            return;
                        }
                    }
                }
            }

            public bool RegionEnabled(string systemName)
            {
                foreach (ProfileRecord pr in this)
                {
                    if (pr.RegionEnabled(systemName))
                    {
                        return true;
                    }
                }
                return false;
            }

            public void EnableRegion(string region, bool enabled)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    this[i].EnableRegion(region, enabled);
                }
            }
            
            public void Save()
            {
                SaveAs(this.Path);
            }

            public void SaveAs(string newPath)
            {                                
                JsonSerializer serializer = new JsonSerializer();
                StreamWriter sw = new StreamWriter(newPath);
                try
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        writer.Formatting = Formatting.Indented;
                        serializer.Serialize(writer, this);
                    }
                }
                finally
                {
                    sw.Close();
                }
            }

            public static Profile Load(string path)
            {                
                JsonSerializer serializer = new JsonSerializer();
                StreamReader sr = new StreamReader(path);
                try
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        Profile p = serializer.Deserialize<Profile>(reader);
                        p.path = path;
                        return p;
                    }
                }
                finally
                {
                    sr.Close();
                }
            }
        }
    }
}