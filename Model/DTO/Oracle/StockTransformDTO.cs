using Model.Entities.Oracle;

namespace Model.DTO.Oracle
{
    public class StockTransformDTO
    {
        public PP_TriggerData triggerData { get; set; }

        public Stock motherCodeStock { get; set; }

        public Stock childCodeStock { get; set; }

        public SKUTransform skuTF { get; set; }

        public bool isMotherCode { get; set; }
    }
}
