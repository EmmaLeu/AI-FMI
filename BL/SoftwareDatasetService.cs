using BE;
using DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SoftwareDatasetService
    {
        private Repositories repository;

        public SoftwareDatasetService(Repositories repository)
        {
            this.repository = repository;
        }

        public List<SoftwareDataset> GetLatestItems(int howMany, bool type)
        {
            return repository.SoftwareDatasetRepo.GetLatestItems(howMany, type).ToList();
        }

        public List<SoftwareDataset> GetSoftwareDatasets(bool type)
        {
            return repository.SoftwareDatasetRepo.GetSoftwareDatasets(type);
        }

        public void AddSoftwareDataset(SoftwareDataset sd)
        {
            repository.SoftwareDatasetRepo.AddSoftwareDataset(sd);
        }
    }
}
