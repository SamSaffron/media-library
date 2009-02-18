using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    public enum MetadataFragmentQualityEnum
    {
        OnlyMatch,
        FirstMatch, 
        LikelyMatch
    }

    public class MetadataFragment
    {
        MetadataFragmentQualityEnum Quality {
            get {
                throw new NotImplementedException();
            }
        }
        DateTime FetchDate {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
