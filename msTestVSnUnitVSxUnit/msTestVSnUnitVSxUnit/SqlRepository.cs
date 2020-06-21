using System.Collections.Generic;

namespace msTestVSnUnitVSxUnit
{
    // crud operations
    public class SqlRepository : ISqlRepository
    {
        public int Create()
        {
            // insert codes
            return 1;
        }

        public List<string> Read()
        {
            // get data from db codes
            return new List<string> { "1", "2", "3" };
        }

        public int Update()
        {
            // update codes
            return 1;
        }

        public int Delete()
        {
            // delete codes
            return 1;
        }
    }
}
