using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFilesAPI;
using VaultApplicationCleanArchitecture.Application.Interfaces;

namespace VaultApplicationCleanArchitecture.Application
{
    public class AddModificationDateToTitleUseCase
    {
        private readonly IObjectRepository _objectRepository;

        public AddModificationDateToTitleUseCase(IObjectRepository objectRepository)
        {
            _objectRepository  = objectRepository;
        }


        public void Execute(ObjVer objVer, int userID)
        {
            var objectTitle = _objectRepository.GetObjectTitle(objVer);
            var datePostFix = objectTitle.Length > 11 ? objectTitle.Substring(objectTitle.Length-10) : "";

            var objectModified = _objectRepository.GetObjectLastModified(objVer);

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
