using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    public interface IMetadataProvider
    {
        string ProviderName { get; }
        bool RequiresInternet { get; }

        /*
         * Item has access to Metadata which in turn has access to MetadataFragments, do we update Item as a side effect?
         * If so, should we call this UpdateMetadata
         */


        /*
         * Example :
         *   "Godzilla (2002)" , 100
         *   "Godzilla (1935)" , 101
         */
        Dictionary<string, int> FetchCandidateIds(Item item);
        MetadataFragment Fetch(Item item, int metadataId);
        MetadataFragment Fetch(Item item);
        bool NeedsRefresh (Item item); 


        IMetadataProviderConfiguration BuildDefaultConfiguration();
        void UpdateConfiguration (IMetadataProviderConfiguration configuration); 
        
    }
}
