using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApplication7
{
    public class Utility : IUtility
    {
        public string FileSourcePath
        {
            set;get;
        }

        public IList<PropertyMember> ProcessPropertyType(IList<PropertyMemberSource> propertyMemeberList)
        {
            var result = new List<PropertyMember>();
            foreach(var item in propertyMemeberList)
            {
                var name = item.PropertyName;
                var typeName = item.TypeName;
                var type = ConvertTypeFromName(typeName.ToLower());
                result.Add(new PropertyMember { PropertyName= name, TypeName = type});
            }
            return result;
        }

        public IList<PropertyMemberSource> ReadPropertiesFromSource()
        {
            var result = new List<PropertyMemberSource>();
            using (var reader = new StreamReader(FileSourcePath))
            {
                var line = string.Empty;
                while((line = reader.ReadLine()) != null)
                {
                    var arr = line.Split(',');
                    if (arr.Length < 2) throw new Exception("row data using , as split");
                    result.Add(new PropertyMemberSource { PropertyName = arr[0], TypeName = arr[1]});
                }
            }
            return result;
        }

        private Type ConvertTypeFromName(string name)
        {
            switch (name)
            {
                case "string":
                    return typeof(string);
                case "int":
                    return typeof(int);
                case "decimal":
                    return typeof(decimal);
            }

            throw new NotSupportedException();
        }
    }

    public interface IUtility
    {
        string FileSourcePath { get; set; }
        IList<PropertyMemberSource> ReadPropertiesFromSource();
        IList<PropertyMember> ProcessPropertyType(IList<PropertyMemberSource> propertyMemeberList);
    }
}
