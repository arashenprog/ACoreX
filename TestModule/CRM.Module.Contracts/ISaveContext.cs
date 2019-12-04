using CRM.Module.Contracts.Models;

namespace CRM.Contracts.Module
{
    public interface ISaveContext
    {
        void Save(SaveModel saveModel);
    }
}