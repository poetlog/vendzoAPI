namespace vendzoAPI.Interfaces
{
    public interface IPromotionRepository
    {
        ICollection<Promotion> GetPromotions();
        ICollection<Promotion> GetValidPromotions();
        Promotion GetById(string id);
        Promotion GetByCode(string code);
        bool CheckIsValid(string code);
        bool PromotionExists(string id);
        bool Add(Promotion promotion);
        bool Update(Promotion promotion);
        bool Delete(Promotion promotion);
        bool Save();


    }
}
