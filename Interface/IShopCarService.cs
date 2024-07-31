namespace WebMVC2.Interface
{
    public interface IShopCarService
    {
        public int Get();
        public Task<int> Add(int Id, int Num);
        public void Delete(int Id);
    }
}
