using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Providers
{
    class TvDbProvider : IMetadataProvider
    {

        public void Fetch(Item item, bool forceRefresh)
        {
            throw new NotImplementedException(); 
        }

        public Dictionary<string, int> FetchOptions()
        {
            throw new NotImplementedException();
        }

        public void Fetch(Item item, int metadataId)
        {
            throw new NotImplementedException();  
        }

        #region IMetadataProvider Members

        public string ProviderName {
            get { throw new NotImplementedException(); }
        }

        public bool RequiresInternet {
            get { throw new NotImplementedException(); }
        }

        public Dictionary<string, int> FetchCandidateIds(Item item) {
            throw new NotImplementedException();
        }

        MetadataFragment IMetadataProvider.Fetch(Item item, int metadataId) {
            throw new NotImplementedException();
        }

        public MetadataFragment Fetch(Item item) {
            throw new NotImplementedException();
        }

        public bool NeedsRefresh(Item item) {
            throw new NotImplementedException();
        }

        public IMetadataProviderConfiguration BuildDefaultConfiguration() {
            throw new NotImplementedException();
        }

        public void UpdateConfiguration(IMetadataProviderConfiguration configuration) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
