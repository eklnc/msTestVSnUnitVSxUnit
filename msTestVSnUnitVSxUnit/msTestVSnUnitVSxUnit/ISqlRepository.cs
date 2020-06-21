using System.Collections.Generic;

namespace msTestVSnUnitVSxUnit
{
    public interface ISqlRepository
    {
        int Create();
        List<string> Read();
        int Update();
        int Delete();
    }
}
