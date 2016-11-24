using BrendanGrant.Helpers.FileAssociation;
using System;

namespace FileTypeAssociation
{
    public class FileTypeAssociation
    {
        
        private readonly FileAssociationInfo _fileAssociationInfo;
        private readonly ProgramAssociationInfo _programAssociationInfo;
        private const string ProgramId = "Finance";
       
        public FileTypeAssociation(string fileExt)
        {
            _fileAssociationInfo = new FileAssociationInfo(fileExt);
            _programAssociationInfo = new ProgramAssociationInfo(ProgramId);

        }

        public bool Exists
        {
            get
            {
                return _fileAssociationInfo.Exists && _programAssociationInfo.Exists;
            }
        }
        
        
        public void Create(string exeName, string exePath)
        {
            if (!_fileAssociationInfo.Exists)
            {
                _fileAssociationInfo.Create(ProgramId);
                _fileAssociationInfo.ContentType = "application/x-qif";
                _fileAssociationInfo.OpenWithList = new string[] { exeName };
            }

            ProgramAssociationInfo programInfo = new ProgramAssociationInfo(ProgramId);
            
            if(!programInfo.Exists)
            {
                programInfo.Create("Quicken Interexchange Format", new ProgramVerb("Open", $"\"{exePath}\" \"%1\""));
            }
        }
    }
}
