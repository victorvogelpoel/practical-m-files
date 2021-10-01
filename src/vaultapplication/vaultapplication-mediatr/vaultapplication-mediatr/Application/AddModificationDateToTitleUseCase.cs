using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MFilesAPI;
using VaultApplicationMediatr.Application.Interfaces;

namespace VaultApplicationMediatr.Application
{
    public class AddModiticationDateToTitle : IRequest<string>
    {
        public IVault Vault  { get; set;}
        public ObjVer ObjVer { get; set; }
        public int    UserID { get; set; }

    }



    public class AddModificationDateToTitleHandler : IRequestHandler<AddModiticationDateToTitle, string>
    {
        private readonly IObjectRepository _objectRepository;

        public AddModificationDateToTitleHandler(IObjectRepository objectRepository)
        {
            _objectRepository  = objectRepository;
        }


        public Task<string> Handle(AddModiticationDateToTitle request, CancellationToken cancellationToken)
        {
            var objectTitle     = _objectRepository.GetObjectTitle(request.ObjVer).TrimEnd();
            var datePostFix     = objectTitle.Length > 11 ? objectTitle.Substring(objectTitle.Length-10) : "";

            var objectModified  = _objectRepository.GetObjectLastModified(request.ObjVer);

            if (DateTime.TryParseExact(datePostFix, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date))
            {
                // The current title is postfixed with a date.
                if (date.Date != objectModified.Date)
                {
                    objectTitle = objectTitle.Substring(0, objectTitle.Length-11);
                    objectTitle += $" {objectModified:yyyy-MM-dd}";

                    _objectRepository.UpdateObjectTitle(request.ObjVer, objectTitle, request.UserID);
                }
            }
            else
            {
                // The current objectTitle does not contain a date postfix. Add it
                objectTitle += $" {objectModified:yyyy-MM-dd}";

                _objectRepository.UpdateObjectTitle(request.ObjVer, objectTitle, request.UserID);
            }

            return Task.FromResult(objectTitle);
        }




        /// <summary>
        /// Add or update the object's modification date to the object's title property.
        /// </summary>
        /// <param name="objVer">objVer of the object to update</param>
        /// <param name="userID">ID of M-Files user to set as LastModifiedBy property (otherwise it will show "(M-Files Server)")</param>
        public void UpdateObjectTitle(ObjVer objVer, int userID)
        {
            var objectTitle     = _objectRepository.GetObjectTitle(objVer).TrimEnd();
            var datePostFix     = objectTitle.Length > 11 ? objectTitle.Substring(objectTitle.Length-10) : "";

            var objectModified  = _objectRepository.GetObjectLastModified(objVer);

            if (DateTime.TryParseExact(datePostFix, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var date))
            {
                // The current title is postfixed with a date.
                if (date.Date != objectModified.Date)
                {
                    objectTitle = objectTitle.Substring(0, objectTitle.Length-11);
                    objectTitle += $" {objectModified:yyyy-MM-dd}";

                    _objectRepository.UpdateObjectTitle(objVer, objectTitle, userID);
                }
            }
            else
            {
                // The current objectTitle does not contain a date postfix. Add it
                objectTitle += $" {objectModified:yyyy-MM-dd}";

                _objectRepository.UpdateObjectTitle(objVer, objectTitle, userID);
            }
        }
    }
}
