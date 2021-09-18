using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;
using VaultApplicationCleanArchitecture.Application.Interfaces;

namespace vaultapplication_cleanarch_tests
{
    class ObjectRepositoryStub : IObjectRepository
    {
        private readonly Dictionary<ObjVer, Dictionary<string, object>> _objectProps = new Dictionary<ObjVer, Dictionary<string, object>>();

        public ObjectRepositoryStub()
        {
        }

        public void AddObjectStub(ObjVer objVer, string initTitle, DateTime initLastModifiedDate)
        {
            _objectProps[objVer] = new Dictionary<string, object>()
            {
                { "NameOrTitle", initTitle },
                { "LastModified", initLastModifiedDate }
            };
        }

        public string GetObjectTitle(ObjVer objVer)
        {
            return (string)(_objectProps[objVer]["NameOrTitle"]);
        }

        public DateTime GetObjectLastModified(ObjVer objVer)
        {
            return (DateTime)(_objectProps[objVer]["LastModified"]);
        }


        public void UpdateObjectTitle(ObjVer objVer, string updatedTitle, int lastModifiedByUserId)
        {
            _objectProps[objVer]["NameOrTitle"] = updatedTitle;
        }
    }
}
