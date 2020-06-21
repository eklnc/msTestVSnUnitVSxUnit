using System.Collections.Generic;

namespace msTestVSnUnitVSxUnit
{
    public class MathOperations : IMathOperations
    {
        private readonly ISqlRepository _sqlRepository;

        public MathOperations(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public int? Sum(int? v1, int? v2)
        {
            if (!v1.HasValue || !v2.HasValue)
            {
                return null;
            }

            if (v1.Value >= int.MaxValue || v2.Value >= int.MaxValue)
            {
                return null;
            }

            var result = v1 + v2;

            var dbResult = _sqlRepository.Create();
            if (dbResult == 0)
            {
                return null;
            }

            return result;
        }

        public int? Mul(int? v1, int? v2)
        {
            if (!v1.HasValue || !v2.HasValue)
            {
                return null;
            }

            if (v1.Value >= int.MaxValue || v2.Value >= int.MaxValue)
            {
                return null;
            }

            var result = v1 * v2;

            List<string> dataFromDb = _sqlRepository.Read();
            if (dataFromDb == null || dataFromDb.Count == 0)
            {
                return null;
            }

            return result;
        }

        public int? Sub(int? v1, int? v2)
        {
            if (!v1.HasValue || !v2.HasValue)
            {
                return null;
            }

            if (v1.Value >= int.MaxValue || v2.Value >= int.MaxValue)
            {
                return null;
            }

            if (v1 < v2)
            {
                return null;
            }

            var result = v1 - v2;

            var dbResult = _sqlRepository.Update();
            if (dbResult == 0)
            {
                return null;
            }

            return result;
        }

        public int? Div(int? v1, int? v2)
        {
            if (!v1.HasValue || !v2.HasValue)
            {
                return null;
            }

            if (v1.Value >= int.MaxValue || v2.Value >= int.MaxValue)
            {
                return null;
            }

            if (v1 < v2)
            {
                return null;
            }

            if (v2 == 0)
            {
                return null;
            }

            if (v1 % v2 != 0)
            {
                return null;
            }

            var result = v1 / v2;

            var dbResult = _sqlRepository.Delete();
            if (dbResult == 0)
            {
                return null;
            }

            return result;
        }
    }
}
